using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDamage : MonoBehaviour
{
    public LayerMask ignore;

    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<AudioSource>().Play();
        other.gameObject.GetComponent<Animator>().SetTrigger("isDamaged");
        
        if (other.gameObject.CompareTag("Playertag"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(15f, gameObject.GetComponent<Collider>());
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(25);
        }
    }
}