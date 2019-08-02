using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//REFERENCED: https://www.youtube.com/watch?v=Hj7AZkyojdo&list=PLboXykqtm8dynMisqs4_oKvAIZedixvtf
public class HUDScript : MonoBehaviour
{
    public Inventory Inventory;

    // Start is called before the first frame update
    void Start()
    {
        Inventory.ItemAdded += InventoryScript_ItemAdded;
        Inventory.ItemRemoved += Inventory_ItemRemoved;
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs eventItem)
    {
        Transform inventoryPanel = transform.Find("Inventory");
        foreach(Transform slot in inventoryPanel)
        {
            Image image = slot.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
            ItemDrag itemDrag = slot.GetChild(0).GetChild(0).GetChild(0).GetComponent<ItemDrag>();

            if(!image.enabled)
            {
                image.enabled = true;
                image.sprite = eventItem.Item.Image;

                //Store refernce;
                itemDrag.Item = eventItem.Item;

                break;
            }
        }
    }

    private void Inventory_ItemRemoved(object sender, InventoryEventArgs eventItem)
    {
        Transform inventoryPanel = transform.Find("Inventory");
        foreach(Transform slot in inventoryPanel)
        {
            Image image = slot.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
            ItemDrag itemDrag = slot.GetChild(0).GetChild(0).GetChild(0).GetComponent<ItemDrag>();

            if(itemDrag.Item.Equals(eventItem.Item))
            {
                image.enabled = false;
                image.sprite = null;
                itemDrag.Item = null;
                break;
            }
        }
           

    }

}
