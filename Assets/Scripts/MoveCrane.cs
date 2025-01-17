﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCrane : MonoBehaviour
{
    public GameObject crane;
    public GameObject wall;
    public Transform player;
    private const float lerpTime = 8f;
    private float currentLerp = 0f;
    private bool hasMoved = true;
    private bool currentlyMoving = false;
    private Vector3 originalPosition;
    private Vector3 movePosition;
    private Vector3 velocity;
    private bool EnteredTrigger;

    private void Start()
    {
        originalPosition = crane.transform.position;
        movePosition = originalPosition + Vector3.forward * 8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (/*isObjectReachable()*/EnteredTrigger && Input.GetKeyDown(KeyCode.E))
        {
            player.GetComponent<Animator>().SetTrigger("isPressing");
            StartCoroutine(moveCrane());
            hasMoved = !hasMoved;
            currentLerp = 0f;
            GameObject.FindGameObjectWithTag("Playertag").GetComponent<CameraFollow>().enabled = false;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraCollision>().enabled = false;
            StartCoroutine(CoroutineTest());
        }
    }

    private IEnumerator CoroutineTest()
    {
        yield return new WaitForSeconds(2.4f);
        GameObject.FindGameObjectWithTag("Playertag").GetComponent<CameraFollow>().enabled = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraCollision>().enabled = true;
        wall.SetActive(false);

    }

    IEnumerator moveCrane()
    {
        if (currentlyMoving)
        {
            yield break;
        }

        currentlyMoving = true;

        crane.GetComponent<AudioSource>().Play();
        
        while (currentLerp < lerpTime)
        {
            currentLerp += Time.deltaTime;
            
            if (hasMoved)
            {
                crane.transform.position = Vector3.SmoothDamp(crane.transform.position, originalPosition, ref velocity, lerpTime);
            }
            else
            {
                crane.transform.position = Vector3.SmoothDamp(crane.transform.position, movePosition, ref velocity, lerpTime);
            }
            
            yield return null;
        }
        
        
        crane.GetComponent<AudioSource>().Stop();
        currentlyMoving = false;
    }

    //https://answers.unity.com/questions/1205237/how-can-i-check-if-an-object-has-entered-a-trigger.html
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Playertag")
        {
            EnteredTrigger = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        EnteredTrigger = false;
    }

    /*public bool isObjectReachable()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 1f)
        {
            return true;
        }
        
        return false;
    }*/
}