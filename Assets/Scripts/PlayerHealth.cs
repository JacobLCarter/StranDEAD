using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage, Collider collider)
    {
        print(health);
        health -= damage;
        //animator.SetTrigger("isDamaged");
        
        if (health <= 0)
        {
            collider.gameObject.SetActive(false);
            StartCoroutine(Die());
        }
    }
    
    private IEnumerator Die()
    {
        gameObject.GetComponent<Rigidbody>().freezeRotation = false;
        animator.SetTrigger("isDead");

        yield return null;
    }
}
