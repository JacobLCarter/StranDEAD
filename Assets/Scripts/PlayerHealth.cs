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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "zombieHand")
        {
            other.GetComponent<AudioSource>().Play();
            health -= 20;
            
            if (health <= 0)
            {
                other.gameObject.SetActive(false);
                StartCoroutine(Die());
            }
        }
    }

    private IEnumerator Die()
    {
        gameObject.GetComponent<Rigidbody>().freezeRotation = false;
        animator.SetTrigger("isDead");

        yield return null;
    }
}
