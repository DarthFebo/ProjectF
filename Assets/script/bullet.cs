using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public AudioSource bulletImpact;

    private void OnCollisionEnter(Collision coll)
    {
        
        playerHealth health = coll.gameObject.GetComponent<playerHealth>();
        if(health)
        {
            bulletImpact.Play();
            health.HurtPlayer(1);
           

        }

        Destroy(gameObject);
    }
}
