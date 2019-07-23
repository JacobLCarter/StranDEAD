using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform player;
    public LayerMask collisionMask;
    private float speed = 3f;
    
   // private Vector3 normalOffset = new Vector3(0, 1.3f, -1.2f);

    private void Start()
    {
        //transform.position = player.position + normalOffset;
    }

    private void LateUpdate()
    { 
        CollisionCheck();
    }

    private void CollisionCheck()
    {
        RaycastHit hit = new RaycastHit();

        if (Physics.Linecast(player.position - rotation, transform.position, out hit, collisionMask))
        {
            transform.position = Vector3.Lerp(transform.position, player.position - transform.position,
                speed * Time.deltaTime);
        }
    }
}
