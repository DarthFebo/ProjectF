
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
        rigidbody.detectCollisions = false;

        //  GetComponent<Rigidbody>().freezeRotation = true;
        this.transform.position = theDest.position;
        this.transform.parent = theDest;
    }
    
    void OnMouseUp()
    {
        this.transform.parent = null;
        rigidbody.isKinematic = false;
        rigidbody.detectCollisions = true;

        rigidbody.AddForce(theDest.forward * speed, ForceMode.Impulse);
    }
}

