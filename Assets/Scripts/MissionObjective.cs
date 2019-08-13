﻿//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissionObjective : MonoBehaviour
{
    public GameObject missionObject;
    public GameObject key;
    public GameObject zombie;
    public GameObject blood;
    public AudioSource audio;
    public Text missionCar;
    public Text missionDam;
    public Text missionDoor;
    public Text missionKey;

    // Start is called before the first frame update
    void Start()
    {
        missionCar.enabled = false;
        missionDam.enabled = false;
        missionDoor.enabled = false;
        missionKey.enabled = false;
        zombie.gameObject.SetActive(true);
        key.SetActive(false);
        blood.SetActive(false);

    }

    void OnTriggerEnter(Collider Player)
    {
        if (!missionCar.enabled && gameObject.tag == "Car")
        {
            missionCar.enabled = true;
            missionCar.color = Color.white;
        }
        else if (!missionDam.enabled && gameObject.tag == "Dam")
        {
            missionDam.enabled = true;
            missionDam.color = Color.white;
            missionCar.color = Color.red;
        }
        else if (!missionDoor.enabled && gameObject.tag == "Door")
        {
            key.SetActive(true);
            blood.SetActive(true);
            zombie.gameObject.transform.position = blood.transform.Find("trailEnd").gameObject.transform.position;
            audio.Play();
            //zombie.gameObject.SetActive(false);
            missionDoor.enabled = true;
            missionDoor.color = Color.white;
            missionDam.color = Color.red;
        }
        //else if (!missionKey.enabled && gameObject.tag == "Key")
        //{
        //    missionKey.enabled = true;
        //    missionKey.color = Color.white;
        //    missionDoor.color = Color.red;
        //}
    }
}
