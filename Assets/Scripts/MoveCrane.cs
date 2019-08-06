using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCrane : MonoBehaviour
{
    public GameObject crane;
    public Transform player;
    private const float useDistance = 3f;
    private const float moveSpeed = .01f;
    private const float lerpTime = 500f;
    private float currentLerp = 0f;
    private bool hasMoved = false;
    private bool currentlyMoving = false;
    private Vector3 originalPosition;
    private Vector3 movePosition;

    private void Start()
    {
        originalPosition = crane.transform.position;
        movePosition = originalPosition + Vector3.forward * 15f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(moveCrane());
        }
    }

    IEnumerator moveCrane()
    {
        if (currentlyMoving)
        {
            yield break;
        }

        currentlyMoving = true;

        while (currentLerp < lerpTime)
        {
            //crane.GetComponent<AudioSource>().Play();
            currentLerp += Time.deltaTime;
            
            if (hasMoved)
            {
                crane.transform.position = Vector3.Lerp(crane.transform.position, originalPosition, currentLerp / lerpTime);
            }
            else
            {
                crane.transform.position = Vector3.Lerp(crane.transform.position, movePosition, currentLerp / lerpTime);
            }

            yield return null;
        }

        hasMoved = !hasMoved;

        currentlyMoving = false;
    }
}