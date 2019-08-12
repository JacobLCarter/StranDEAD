using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCrane : MonoBehaviour
{
    public GameObject crane;
    public Transform player;
    private const float lerpTime = 5000f;
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
            print(hasMoved);
            StartCoroutine(moveCrane());
            hasMoved = !hasMoved;
            currentLerp = 0f;
            //crane.GetComponent<AudioSource>().Play();
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
        
        currentlyMoving = false;
    }
}