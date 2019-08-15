using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    public Image healthBar;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        health = 100f;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage, Collider collider)
    {
        health -= damage;
        healthBar.fillAmount = health / 100f;

        if (health <= 0f)
        {
            collider.gameObject.SetActive(false);
            Die();
        }
    }
    
    private void Die()
    {
        gameObject.GetComponent<Rigidbody>().freezeRotation = false;
        animator.SetTrigger("isDead");
    }
}
