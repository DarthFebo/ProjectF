using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    private Transform player;
    private Transform radio;

    public float moveSpeed;
    public float enemyRadius;
    public Gun gun;
    public float shootingRange = 5;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        radio = GameObject.FindGameObjectWithTag("Radio").transform;
        gun = GetComponent<Gun>();
    }

    void Update()
    {
        var playerDistance = Vector3.Distance(player.position, transform.position);
        var radioDistance = Vector3.Distance(radio.position, transform.position);

        if(playerDistance <= enemyRadius)
        {
            transform.LookAt(player);
            //GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed, ForceMode.);
            GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);

            if (playerDistance <= shootingRange)
            {
                //gun.enabled = true;
                if (!gun.IsInvoking("Shoot"))
                {
                    gun.InvokeRepeating("Shoot", 0f, gun._reloadTime);
                }
            }
            else
            {
                //gun.enabled = false;
                gun.CancelInvoke("Shoot");
            }
        }
        else if(radioDistance <= enemyRadius)
        {
            transform.LookAt(radio);
            GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}


