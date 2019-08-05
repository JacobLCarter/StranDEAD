using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REFERENCED: https://www.youtube.com/watch?v=twjrs4u_5bc&list=PLboXykqtm8dynMisqs4_oKvAIZedixvtf&index=6
public class ItemClick : MonoBehaviour
{
    public Inventory daInventory;
    public void OnItemClicked()
    {
        ItemDrag dragHandler = 
        gameObject.transform.Find("Image").GetComponent<ItemDrag>();

        TheInventoryItem item = dragHandler.Item;

        Debug.Log(item.Name);

        daInventory.UseItem(item);

        item.OnUse();
    }
}
