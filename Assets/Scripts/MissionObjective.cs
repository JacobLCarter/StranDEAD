﻿//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissionObjective : MonoBehaviour
{
    public GameObject missionObject;
    //public GameObject missionTriggerKey;
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
            // TODO: check if key in inventory
            missionDoor.enabled = true;
            missionDoor.color = Color.white;
            missionDam.color = Color.red;
        }
        else if (!missionKey.enabled && gameObject.tag == "Key")
        {
            missionKey.enabled = true;
            missionKey.color = Color.white;
            missionDoor.color = Color.red;
        }
    }
}
