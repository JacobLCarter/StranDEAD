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

                if (ItemAdded != null)
                {
                    ItemAdded(this, new InventoryEventArgs(item));
                }
            }
        }
    }
}
