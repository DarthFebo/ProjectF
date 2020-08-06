
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float smoothing;

    private GameObject player;
    private Vector2 smoothedVelocity;
    private Vector2 currentLookingPos;

    private void Start()
    {
        player = transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        RotateCamera();
        CheckForShooting();
    }

    private void RotateCamera()
    {
        Vector2 inputValues = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw ("Mouse Y"));

        inputValues = Vector2.Scale(inputValues, new Vector2(lookSensitivity * smoothing, lookSensitivity * smoothing));
        
        // wanneer waarde tussen 2 waardes van camera rustig kan uitglijden
        smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, inputValues.x, 1f / smoothing);
        smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, inputValues.y, 1f / smoothing);

        currentLookingPos += smoothedVelocity;
        // om te zorgen dat de camera draait met kijken speler niet
        transform.localRotation = Quaternion.AngleAxis(-currentLookingPos.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(currentLookingPos.x, player.transform.up);
    }
    private void CheckForShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit whatIhit;
            if (Physics.Raycast(transform.position, transform.forward, out whatIhit, Mathf.Infinity))
            {
                Debug.Log(whatIhit.collider.name);
            }
        }

    }       

}
