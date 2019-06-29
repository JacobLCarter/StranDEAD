using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRB;
    private float moveSpeed;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }
    
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        moveSpeed = 8f;
        checkSprint();
    }

    public void MovePlayer(Vector3 direction)
    {
        playerRB.transform.position += direction * moveSpeed * Time.deltaTime;
    }

    public void RotatePlayer(Vector3 rotation)
    {
        playerRB.MoveRotation(transform.rotation * Quaternion.Euler(rotation));
    }

    private void checkSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed *= 1.5f;
        }
    }
}
