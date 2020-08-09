using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spider : MonoBehaviour
{
    private Transform player;
    private Animator animator;
    private enemyScript[] enemies;

    public float moveSpeed;
    public float sightRadius;
    public float attackRadius;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        enemies = GameObject.FindObjectsOfType<enemyScript>();
    }

    private void Update()
    {
        if(!animator.GetBool("sleeping"))
        {
            var target = GetTarget();

            if (target)
            {
                var targetDistance = Vector3.Distance(target.position, transform.position);

                transform.LookAt(target, Vector3.up);
                GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);
                animator.SetBool("walking", true);

                if (targetDistance <= attackRadius)
                {
                    animator.SetBool("attacking", true);
                }
                else
                {
                    animator.SetBool("attacking", false);
                }
            }
            else
            {
                animator.SetBool("walking", false);
                animator.SetBool("attacking", false);
            }
        }
    }

    public void Sleep(bool value)
    {
        animator.SetBool("sleeping", value);
        if(value)
        {
            animator.SetBool("walking", false);
            animator.SetBool("attacking", false);
        }
    }

    public void Die()
    {
        animator.SetTrigger("die");

        GetComponent<Rigidbody>().detectCollisions = false;
        Destroy(this);
    }

    public void Attack()
    {
        var target = GetTarget();

        if (target)
        {
            var targetDistance = Vector3.Distance(target.position, transform.position);
            if(targetDistance <= attackRadius)
            {
                if (target == player)
                {
                    Destroy(target.GetComponent<PlayerMovementController>());
                    Destroy(target.GetComponentInChildren<PlayerCameraController>());
                    StartCoroutine(Coroutine());

                    IEnumerator Coroutine()
                    {
                        yield return new WaitForSecondsRealtime(1f);

                        GameManager gameManager = FindObjectOfType<GameManager>();
                        gameManager.EndGame();
                    }
                }
                else
                {
                    Destroy(target.gameObject);
                }
            }
        }
    }

    private Transform GetTarget()
    {
        Transform target = null;
        var targetDistance = sightRadius;
        foreach (var enemy in enemies)
        {
            if (enemy)
            {
                var enemyDistance = Vector3.Distance(enemy.transform.position, transform.position);
                if (enemyDistance <= targetDistance)
                {
                    targetDistance = enemyDistance;
                    target = enemy.transform;
                }
            }
        }

        if (!target)
        {
            var playerDistance = Vector3.Distance(player.position, transform.position);
            if (playerDistance <= targetDistance)
            {
                target = player;
            }
        }

        return target;
    }
}
