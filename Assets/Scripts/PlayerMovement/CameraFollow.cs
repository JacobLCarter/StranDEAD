using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class CameraFollow : MonoBehaviour
{
    private Transform cam;
    private Vector3 forwardCam;
    private Vector3 calculatedMove;
    private PlayerMovement movement;


    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        //references to necessary objects
        movement = GameObject.FindObjectOfType<PlayerMovement>();
        cam = Camera.main.transform;

        //hides the cursor when the game is being played
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //get player input on x and z axis
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //set a vector equal to the position that the camera is currently facing
        forwardCam = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        //calculate the player's input with the current conditions
        calculatedMove = horizontal * cam.right + vertical * forwardCam;
        
        //move the player; called in the PlayerMovement script
        movement.MovePlayer(calculatedMove, horizontal, vertical);
        
        //get the camera's rotation for the current frame
//        float camRotation = Input.GetAxis("Mouse X");
//        Vector3 rotation = new Vector3 (0, camRotation, 0) * sensitivity;
//
//        //rotate the player; called in the PlayerMovement script
//        movement.RotatePlayer(rotation);
    }
}