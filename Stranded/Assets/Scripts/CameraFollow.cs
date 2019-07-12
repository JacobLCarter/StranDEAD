using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class CameraFollow : MonoBehaviour
{
    //private Transform player;
    //private Vector3 offset = new Vector3(0f,5f,-6f);
    //public float sensitivity = 3f;
    private Transform cam;
    private Vector3 forwardCam;
    private Vector3 calculatedMove;
    private float sensitivity = 3f;
    private PlayerMovement movement;


    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        movement = GameObject.FindObjectOfType<PlayerMovement>();
        cam = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        forwardCam = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        calculatedMove = horizontal * cam.right + vertical * forwardCam;
        
        movement.MovePlayer(calculatedMove);

        float camRotation = Input.GetAxis("Mouse X");
        Vector3 rotation = new Vector3 (0, camRotation, 0) * sensitivity;

        movement.RotatePlayer(rotation);
    }
}