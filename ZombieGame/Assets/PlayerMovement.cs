using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRB;
    public float forwardForce = 1000f;
    public float backForce = 700f;
    public float sideForce = 500f;
    public float sensitivity = 3f;

    // Update is called once per frame
    void FixedUpdate()
    {
        float yRotation = Input.GetAxis("Mouse X");
        Vector3 rotation = new Vector3 (0, yRotation, 0) * sensitivity;
        //playerRB.MoveRotation(transform.rotation * Quaternion.Euler(rotation));

        if (Input.GetKey("w"))
        {
            playerRB.AddForce(0,0,forwardForce * Time.deltaTime);
        }

        if (Input.GetKey("s"))
        {
            playerRB.AddForce(0,0,-backForce * Time.deltaTime);
        }

        if (Input.GetKey("a"))
        {
            playerRB.AddForce(-sideForce * Time.deltaTime,0,0);
        }

        if (Input.GetKey("d"))
        {
            playerRB.AddForce(sideForce * Time.deltaTime,0,0);
        }
    }
}
