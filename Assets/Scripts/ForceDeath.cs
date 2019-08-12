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

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 0.5f)
        {
            animator.SetBool("isDead", true);
        }
    }
}
