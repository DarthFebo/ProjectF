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

            // Comment dit uit als je gek word van het geluid Plus regel 76.
            foreach(var spider in spiders.Keys)
            {
                spider.Sleep(value);
            }
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

                //spider.Sleep(true); // Haal deze comment weg als regel 76 een comment heeft.
                spider.Sleep(isPlaying);
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

        var enemy = collision.collider.GetComponent<enemyScript>();
        if(enemy && collision.impulse.magnitude >= killForce)
        {
            Destroy(enemy.gameObject);
        }
    }
}