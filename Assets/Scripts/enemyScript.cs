using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemyScript : MonoBehaviour
{
    private Transform player;
    private float dis;

    public float moveSpeed;
    public float enemyRadius;
    public Gun gun;
    public float shootingRange = 5;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gun = GetComponent<Gun>();
    }

    void Update()
    {
        dis = Vector3.Distance(player.position, transform.position);

        if(dis <= enemyRadius)
        {
            transform.LookAt(player);
            //GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed, ForceMode.);
            GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) < shootingRange)
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


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyRadius);
    }
}


