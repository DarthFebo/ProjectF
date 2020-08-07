using System;
using System.Collections.Generic;
using UnityEngine;

public class radioScript : MonoBehaviour
{

    bool inTrigger = false;
    bool isPlaying = false;
    public AudioSource audio;

    void Update()
    {
        // check if in the trigger zone
        if (inTrigger)
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
        inTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        inTrigger = false;

    }
}