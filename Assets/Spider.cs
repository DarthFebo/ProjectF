using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spider : MonoBehaviour
{
    private Transform player;
    private Animator animator;

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
    }

    private void Update()
    {
        if(!animator.GetBool("sleeping"))
        {
            var playerDistance = Vector3.Distance(player.position, transform.position);

            if (playerDistance <= sightRadius)
            {
                transform.LookAt(player);
                GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);

                animator.SetBool("walking", true);
            }
            else
            {
                animator.SetBool("walking", false);
            }

            if (playerDistance <= attackRadius)
            {
                animator.SetBool("attacking", true);
            }
            else
            {
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
        var playerDistance = Vector3.Distance(player.position, transform.position);
        if (playerDistance <= attackRadius)
        {
            Destroy(player.GetComponent<PlayerMovementController>());
            Destroy(player.GetComponentInChildren<PlayerCameraController>());
            StartCoroutine(Coroutine());

            IEnumerator Coroutine()
            {
                yield return new WaitForSecondsRealtime(1f);

                SceneManager.LoadScene("diedscene");
            }
        }
    }
}
