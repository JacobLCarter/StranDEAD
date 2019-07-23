using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform player;
    public LayerMask collisionMask;
    private float camRotationX;
    private float camRotationY;
    private Vector3 rotation;
    private Vector3 rotationVelocity;
    private const float rotationTime = 0.1f;
    private const float sensitivity = 3.0f;
    private const float moveSpeed = 3f;
    private const float returnSpeed = 5f;
    private const float collisionBump = 0.3f;
    private const float camTooClose = .4f;
    private const float rotationLock = 40f;
    private bool isTooClose;

    private void Start()
    {
        isTooClose = false;
    }

    private void FixedUpdate()
    {
        Collision(player.position - transform.forward * 1.2f + transform.up * 0.7f);

        camRotationX += Input.GetAxis("Mouse X") * sensitivity;
        
        if (isTooClose)
        {
            camRotationY = rotationLock;
        }
        else
        {
            camRotationY -= Input.GetAxis("Mouse Y") * sensitivity;
            camRotationY = Mathf.Clamp(camRotationY, -rotationLock, rotationLock);
        }
        
        rotation = Vector3.SmoothDamp(rotation, new Vector3(camRotationY, camRotationX),
            ref rotationVelocity, rotationTime);

        transform.eulerAngles = rotation;

        Vector3 angle = transform.eulerAngles;
        angle.x = 0;

        player.eulerAngles = angle;
    }

    private void Collision(Vector3 returnTo)
    {
        RaycastHit hit;
        
        if (Physics.Linecast(player.position, returnTo, out hit, collisionMask))
        {
            Vector3 normalData = hit.normal * collisionBump;
            Vector3 bumpPoint = hit.point + normalData;

            if (Vector3.Distance(Vector3.Lerp(transform.position, bumpPoint, moveSpeed * Time.deltaTime),
                    player.position) < camTooClose)
            {
                isTooClose = true;
            }
            else
            {
                isTooClose = false;
                transform.position = Vector3.Lerp(transform.position, bumpPoint, moveSpeed * Time.deltaTime);
            }

            return;
        }

        isTooClose = false;
        transform.position = Vector3.Lerp(transform.position, returnTo, returnSpeed * Time.deltaTime);
    }
}
