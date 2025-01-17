﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REFERENCED: https://www.youtube.com/watch?v=Hj7AZkyojdo&list=PLboXykqtm8dynMisqs4_oKvAIZedixvtf&index=2
public class FlashlightScript : InventoryItemMain
{
    //Returns the key name of flashlight
    public override string Name
    {
        get
        {
            return "Flashlight";
        }
    }

    //Function to override on use
    public override void OnUse()
    {
        base.OnUse();
    }
}
