using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;
    [SerializeField]
    private float sensitivity = 5;

    private PlayerMotor motor;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        Vector3 moveHorizontal = transform.right * xMovement;
        Vector3 moveVertical = transform.forward * zMovement;

        Vector3  velocity = (moveHorizontal + moveVertical).normalized * moveSpeed;

        motor.setVelocity(velocity);

        float yRotation = Input.GetAxis("Mouse X");

        Vector3 rotation = new Vector3 (0, yRotation, 0) * sensitivity;

        motor.setRotation(rotation);

        float xRotation = Input.GetAxis("Mouse Y");

        Vector3 cameraRotation = new Vector3 (xRotation, 0, 0) * sensitivity;

        motor.setCameraRotation(cameraRotation);
    }
}
