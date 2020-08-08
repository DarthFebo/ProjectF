using System;
using System.Collections.Generic;
using UnityEngine;

public class radioScript : MonoBehaviour
{

    bool inTrigger = false;
    private bool _isPlaying = false;
    bool isPlaying
    {
        get => _isPlaying;
        set
        {
            _isPlaying = value;

#if !UNITY_EDITOR
            foreach(var spider in spiders.Keys)
            {
                spider.Sleep(value);
            }
#endif
        }
    }
    
    public AudioSource audio;
    public float killForce = 2f;

    private int triggerIndex = 0;

    private Dictionary<Spider, int> spiders = new Dictionary<Spider, int>();

    void Update()
    {
        // check if in the trigger zone
        if (triggerIndex >= 2)
        {
            // check if key has been pressed
            if (Input.GetKeyDown(KeyCode.E))
            {
                // check if music is already playing
                if (isPlaying)
                {
                    // turn music OFF
                    audio.Stop();
                    isPlaying = false;
                }
                else // not playing
                {
                    // turn music ON
                    audio.Play();
                    isPlaying = true;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            triggerIndex++;
        }

        var spider = other.GetComponent<Spider>();
        if (spider)
        {
            if(spiders.ContainsKey(spider))
            {
                spiders[spider]++;
            }
            else
            {
                spiders.Add(spider, 1);
#if UNITY_EDITOR
                spider.Sleep(true);
#else
                spider.Sleep(isPlaying);
#endif
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerIndex--;
        }

        var spider = other.GetComponent<Spider>();
        if (spider)
        {
            spiders[spider]--;

            if(spiders[spider] <= 0)
            {
                spider.Sleep(false);
                spiders.Remove(spider);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var spider = collision.collider.GetComponent<Spider>();
        if(spider && collision.impulse.magnitude >= killForce)
        {
            spider.Die();
            spiders.Remove(spider);
        }
    }
}