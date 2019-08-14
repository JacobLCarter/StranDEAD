using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REFERENCED: https://www.youtube.com/watch?v=twjrs4u_5bc&list=PLboXykqtm8dynMisqs4_oKvAIZedixvtf&index=6
public class ItemClick : MonoBehaviour
{
    //Grabs gameobject of inventory
    public Inventory daInventory;

    //Checks and finds the item via image and component to then be used by the player.
    public void OnItemClicked()
    {
        ItemDrag dragHandler = gameObject.transform.Find("Image").GetComponent<ItemDrag>();

        TheInventoryItem item = dragHandler.Item;

        Debug.Log(item.Name);

        //Calls for the item to be used
        daInventory.UseItem(item);

        item.OnUse();
    }
}
