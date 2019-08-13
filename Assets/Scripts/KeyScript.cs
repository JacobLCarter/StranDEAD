﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//REFERENCED: https://www.youtube.com/watch?v=Hj7AZkyojdo&list=PLboXykqtm8dynMisqs4_oKvAIZedixvtf&index=2
public class KeyScript : InventoryItemMain
{
    public GameObject levelEnd;
    public Text missionDoor;
    public Text missionKey;


    void Start()
    {
        gameObject.SetActive(true);
    }

    //Returns the key name of key_silver
    public override string Name
    {
        get
        {
            return "Key";
        }
    }

    public override void OnUse()
    {
        base.OnUse();
    }

    public override void OnPickup()
    {
        base.OnPickup();
        missionKey.enabled = true;
        missionKey.color = Color.white;
        missionDoor.color = Color.red;
        levelEnd.SetActive(true);
    }
}
