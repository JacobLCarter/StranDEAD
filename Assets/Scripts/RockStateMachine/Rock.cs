using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralStateMachine;

public class Rock : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public LayerMask collisionMask;
    protected Transform playerHand;
    private Rigidbody objectRB;
    public StateMachine<Rock> stateMachine { get; set; }
    
    
    bool hasPlayer = false;
    bool beingCarried = false;
    private bool touched = false;
    

    private void Start()
    {
        objectRB = GetComponent<Rigidbody>();
        stateMachine = new StateMachine<Rock>(this);
        stateMachine.switchState(GroundStateRock.Instance);
    }

    void Update()
    {
        if (playerHand == null)
        {
            playerHand = animator.GetBoneTransform(HumanBodyBones.RightMiddleIntermediate);
        }

        stateMachine.Update();
    }

    public void setPosition(Vector3 pos, Vector3 rot, Vector3 scale)
    {
        objectRB.transform.localPosition = pos;
        objectRB.transform.localEulerAngles = rot;
        //objectRB.transform.localScale = scale;
    }

    public Transform getHand()
    {
        return playerHand;
    }
}