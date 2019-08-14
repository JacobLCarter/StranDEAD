using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REFERENCED: https://www.youtube.com/watch?v=Hj7AZkyojdo&list=PLboXykqtm8dynMisqs4_oKvAIZedixvtf&index=2
//Interface that lets other structs or class use Name, Image, and Onpickup.
public interface TheInventoryItem
{
    string Name { get; }
    Sprite Image { get; }
    void OnPickup();
    void OnDrop();
    void OnUse();
}

//Sets the argument that is sent to the function InventoryEventArgs to set equal to the TheInventoryItem Item object.
public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(TheInventoryItem e)
    {
        Item = e;
    }

    public TheInventoryItem Item;

}
