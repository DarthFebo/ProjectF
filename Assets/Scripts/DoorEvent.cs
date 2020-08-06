using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        animator.SetTrigger("Open");
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetTrigger("Close");
    }
}
