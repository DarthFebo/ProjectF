
using UnityEngine;

public class pickUp : MonoBehaviour{
   
    
    public Transform theDest;
    void OnMouseDown()
    {
        GetComponent<Rigidbody>().useGravity = false;
        //  GetComponent<Rigidbody>().freezeRotation = true;
        this.transform.position = theDest.position;
        this.transform.parent = GameObject.Find("destination").transform;
    }
    void OnMouseUp()
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = false;
    }
    
}

