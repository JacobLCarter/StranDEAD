using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleAnimation : MonoBehaviour
{
    private Animator animator;

    private float timer;

    private const float maxTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= maxTime)
        {
            float rand = Random.Range(0f, 3f);
            animator.SetFloat("Blend", rand);
            timer = 0f;
        }
    }
}
