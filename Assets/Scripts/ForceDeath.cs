using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceDeath : MonoBehaviour
{
    public Transform player;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "attackWeapon")
        {
            if (other.relativeVelocity.magnitude > 1)
            {
                animator.SetBool("isDead", true);
                gameObject.GetComponent<CapsuleCollider>().direction = 2;
            }
        }
    }
}
