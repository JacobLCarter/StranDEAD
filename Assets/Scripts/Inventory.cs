using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REFERENCES: https://www.youtube.com/watch?v=Hj7AZkyojdo&list=PLboXykqtm8dynMisqs4_oKvAIZedixvtf
public class Inventory : MonoBehaviour
{
    //Sets number of inventory slots to 16
    private const int SLOTS = 16;

    //Creates new object
    private List<TheInventoryItem> myItems = new List<TheInventoryItem>();

    public event EventHandler<InventoryEventArgs> ItemAdd;

    public event EventHandler<InventoryEventArgs> ItemRemove;

    public event EventHandler<InventoryEventArgs> ItemUse;

   
    //Adds items depending if there are slots available and 
    public void AddItem(TheInventoryItem item)
    {
        if (myItems.Count < SLOTS)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();

            if (collider.enabled)
            {
                //Checks to see if collider enabled worked properly since we are using triggers.
                Debug.Log("collider check");
                collider.enabled = false;

                myItems.Add(item);

                item.OnPickup();

                ItemAdd?.Invoke(this, new InventoryEventArgs(item));
            }
        }
    }

    internal void UseItem(TheInventoryItem item)
    {
        if (ItemUse != null)
        {
            ItemUse(this, new InventoryEventArgs(item));
        }
    }

    public void RemoveItem(TheInventoryItem item)
    {
        Debug.Log("This is the remove item");
        if (myItems.Contains(item))
        {
            myItems.Remove(item);

            item.OnDrop();

            Debug.Log("This is the ondrop");
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = true;
            }

            ItemRemove?.Invoke(this, new InventoryEventArgs(item));
        }
    }
}
