
using UnityEngine;

public class pickUp : MonoBehaviour
{
    public Transform theDest;
    public float speed;

    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        rigidbody.isKinematic = true;
        this.transform.position = theDest.position;
        this.transform.parent = theDest;

        foreach(var collider in GetComponents<Collider>())
        {
            if(!collider.isTrigger)
            {
                collider.enabled = false;
            }
        }

        //rigidbody.position = destination.position;
        //fixedJoint = gameObject.AddComponent<FixedJoint>();
        //fixedJoint.enableCollision = false;
        //fixedJoint.connectedBody = destination;
    }
    
    void OnMouseUp()
    {
        this.transform.parent = null;
        rigidbody.isKinematic = false;

        foreach (var collider in GetComponents<Collider>())
        {
            if (!collider.isTrigger)
            {
                collider.enabled = true;
            }
        }

        //Destroy(fixedJoint);
        rigidbody.AddForce(theDest.forward * speed, ForceMode.Impulse);
    }
}

