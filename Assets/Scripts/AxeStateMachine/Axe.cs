using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralStateMachine;

public class Axe : InventoryItemMain
{
    public Animator animator;
    public Transform player;
    private Transform playerHand;
    public StateMachine<Axe> stateMachine { get; set; }
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        stateMachine = new StateMachine<Axe>(this);
        stateMachine.switchState(GroundState.Instance);
    }

    void Update()
    {

        //if (playerHand == null)
        //{
        //    playerHand = animator.GetBoneTransform(HumanBodyBones.RightMiddleDistal);
        //}
 

        stateMachine.Update();
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
               other.gameObject.GetComponent<Enemy>().takeDamage(20);
           }
       }
   }

    public override string Name
    {
        get
        {
            return "Fire Axe";
        }
    }

    public override void OnUse()
    {
        base.OnUse();
        this.stateMachine.switchState(HeldState.Instance);
    }
}
