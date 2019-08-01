using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REFERENCED: https://www.youtube.com/watch?v=Hj7AZkyojdo&list=PLboXykqtm8dynMisqs4_oKvAIZedixvtf&index=2
public class KeyScript : InventoryItemMain
{
    //Returns the key name of key_silver
    public override string Name
    {
        get
        {
            return "key_silver";
        }
    }

    public override void OnUse()
    {
        base.OnUse();
    }
}
