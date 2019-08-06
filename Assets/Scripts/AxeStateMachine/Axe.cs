using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralStateMachine;

public class Axe : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public LayerMask collisionMask;
    private Transform playerHand;
    private Rigidbody objectRB;
    public StateMachine<Axe> stateMachine { get; set; }
    private AudioSource audioSource;

    private void Start()
    {
        objectRB = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        stateMachine = new StateMachine<Axe>(this);
        stateMachine.switchState(GroundState.Instance);
    }

    void Update()
    {
        if (playerHand == null)
        {
            playerHand = animator.GetBoneTransform(HumanBodyBones.RightMiddleDistal);
        }

        stateMachine.Update();
    }

    public void setPosition(Vector3 pos, Vector3 rot, Vector3 scale)
   {
       objectRB.transform.localPosition = pos;
       objectRB.transform.localEulerAngles = rot;
       objectRB.transform.localScale = scale;
   }

   public Transform getHand()
   {
       return playerHand;
   }
   
   private void OnCollisionEnter(Collision other)
   {
       if (other.gameObject.tag == "Enemy")
       {
           if (other.relativeVelocity.magnitude > 3)
           {
               audioSource.Play();
           }
       }
   }
}
