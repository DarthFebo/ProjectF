using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copShoot : MonoBehaviour
{

	private Transform target;
	public float range = 15f;

	public string enemyTag = "Enemy";

	
	public float turnSpeed = 10f;

	public GameObject bulletPrefab;
	public float fireRate = 1f;
	private float fireCountdown = 0f;
	public Transform firePoint;
	public AudioSource bulletImpact;

	// Use this for initialization
	void Start()
	{
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}


	// Update is called once per frame
	void Update()
	{
		
			if (fireCountdown <= 0f)
			{
				Shoot();
				fireCountdown = 1f / fireRate;
			}

			fireCountdown -= Time.deltaTime;	
	}

	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
			
		}
		else
		{
			target = null;
		}

	}

	void Shoot()
	{
		Debug.Log("Shoot");
		bulletImpact.Play();
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		bullet bullet = bulletGO.GetComponent<bullet>();
		Debug.Log(bullet);

		//if (bullet != null)
			//bullet.Seek(target);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}

}
