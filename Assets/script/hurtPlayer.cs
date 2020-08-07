using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtPlayer : MonoBehaviour
{
    public int damageToGive = 1;
    public Rigidbody rb;
    public float pushBackForce = 10;

    private bool canhurtPlayer = true;

    //public AudioClip AudioClip; //Audio clip
    //public AudioSource SoundSource; // Game object waar het geluid vanaf moet komen

    // Start is called before the first frame update
    void Start()
    {
       // SoundSource.clip = AudioClip;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Timer()
    {
        canhurtPlayer = false;

        yield return new WaitForSeconds(1);

        canhurtPlayer = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && canhurtPlayer)
        {
            FindObjectOfType<playerHealth>().HurtPlayer(damageToGive);
            //SoundSource.Play();
            Debug.Log("Player triggered enemy");

            if (rb)
            {
                rb.AddForce(-transform.forward * pushBackForce, ForceMode.Impulse);
            }

            StartCoroutine(Timer());
        }
    }
}
