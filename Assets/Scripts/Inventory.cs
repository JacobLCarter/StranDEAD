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
    private List<IInventoryItem> mItems = new List<IInventoryItem>();

    public event EventHandler<InventoryEventArgs> ItemAdded;

    public event EventHandler<InventoryEventArgs> ItemRemoved;

    //Adds items depending if there are slots available and 
    public void AddItem(IInventoryItem item)
    {
        if (mItems.Count < SLOTS)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();

            if (collider.enabled)
            {
                //Checks to see if collider enabled worked properly since we are using triggers.
                Debug.Log("collider check");
                collider.enabled = false;

                mItems.Add(item);

                item.OnPickup();

                ItemAdded?.Invoke(this, new InventoryEventArgs(item));
            }
        }
    }

    public void RemoveItem(IInventoryItem item)
    {
        Debug.Log("This is the remove item");
        if (mItems.Contains(item))
        {
            mItems.Remove(item);

            item.OnDrop();

            Debug.Log("This is the ondrop");
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = true;
            }

            ItemRemoved?.Invoke(this, new InventoryEventArgs(item));
        }
    }
}
