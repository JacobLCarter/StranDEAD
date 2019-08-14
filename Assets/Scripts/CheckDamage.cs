using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDamage : MonoBehaviour
{
//    private void OnCollisionEnter(Collision other)
//    {
//        if (other.gameObject.CompareTag("Playertag"))
//        {
//            gameObject.GetComponent<AudioSource>().Play();
//            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(10, other);
//        }
//    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Playertag"))
        {
            gameObject.GetComponent<AudioSource>().Play();
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(10, other);
        }
    }
}
