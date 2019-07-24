using System;
using UnityEngine;
using System.Collections;

public class ThrowObject : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public LayerMask collisionMask;
    private Transform playerHand;
    private Rigidbody objectRB;
    private const float throwForce = 1000;
    private Vector3 newPos = new Vector3(-0.054f, -0.136f, -0.029f);
    private Vector3 newRot = new Vector3(201.038f, -207.332f, 162.065f);
    private Vector3 newScale = Vector3.one;
    bool hasPlayer = false;
    bool beingCarried = false;
    private bool touched = false;

    private void Start()
    {
        objectRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (playerHand == null)
        {
            playerHand = animator.GetBoneTransform(HumanBodyBones.RightMiddleIntermediate);
        }

        float dist = Vector3.Distance(gameObject.transform.position, player.position);
        
        if (dist <= 2.5f)
        {
            hasPlayer = true;
        }
        else
        {
            hasPlayer = false;
        }
        
        if (hasPlayer && Input.GetKey(KeyCode.E))
        {
            objectRB.isKinematic = true;
            transform.SetParent(playerHand);
            positionObject();
            beingCarried = true;
        }
        if (beingCarried)
        {
            if (touched)
            {
                objectRB.isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                objectRB.isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                objectRB.AddForce(player.forward * throwForce);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                objectRB.isKinematic = false;
                transform.parent = null;
                beingCarried = false;
            }
        }
    }

   void OnTriggerEnter()
    {
        if (beingCarried)
        {
            touched = true;
        }
    }

   private void positionObject()
   {
       objectRB.transform.localPosition = newPos;
       objectRB.transform.localEulerAngles = newRot;
       objectRB.transform.localScale = newScale;
   }
   
//   public bool isPlayerInSight()
//   {
//       Vector3 target = player.position - transform.position;
//       float angle = Vector3.Angle(target, transform.forward);
//
//       if (angle < 80.0f)
//       {
//           Vector3 direction = player.position - enemyHead.position;
//           Ray ray = new Ray(enemyHead.position, direction);
//           RaycastHit hit;
//
//           if (Physics.Raycast(ray, out hit, SightDistance))
//           {
//               if (hit.collider.transform != null)
//               {
//                   if (hit.collider.transform == player)
//                   {
//                       return true;
//                   }
//               }
//           }
//       }
//        
//       return false;
//   }
}