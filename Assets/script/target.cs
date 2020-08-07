using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class target : MonoBehaviour
{
    public float startHealth = 50f;
    //public GameObject destroyEffect;
    public float currentHealt;


    private float health;

   // public Image healthbar;

   // public AudioClip AudioClip; //Audio clip
  //  public AudioSource SoundSource; // Game object waar het geluid vanaf moet komen


    private void Start()
    {
        //SoundSource.clip = AudioClip;
        health = startHealth;
        currentHealt = health;
        

    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        //healthbar.fillAmount = health / startHealth;
        currentHealt = health;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        //SoundSource.Play();
        Destroy(gameObject, 0.2f);
        //Instantiate(destroyEffect, transform.position, transform.rotation);

    }
}
