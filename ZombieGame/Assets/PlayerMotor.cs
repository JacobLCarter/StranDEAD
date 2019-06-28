using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private Rigidbody playerRB;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    [SerializeField]
    private Camera cam;
    public Vector3 offset;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    public void setVelocity(Vector3 vel)
    {
        velocity = vel;
    }

    public void setRotation(Vector3 rot)
    {
        rotation = rot;
    }

    public void setCameraRotation(Vector3 rot)
    {
        cameraRotation = rot;
    }

    public void Move()
    {
        if (velocity != Vector3.zero)
        {
            playerRB.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
        }
    }

    public void Rotate()
    {
        playerRB.MoveRotation(transform.rotation * Quaternion.Euler(rotation));
        cam.transform.Rotate(-cameraRotation);
    }
}
