using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//REFERENCED: https://www.youtube.com/watch?v=Hj7AZkyojdo&list=PLboXykqtm8dynMisqs4_oKvAIZedixvtf
public class HUDScript : MonoBehaviour
{
    public Inventory Inventory;
    public GameObject PickupPanel;

    // Start is called before the first frame update
    void Start()
    {
        Inventory.ItemAdd += InventoryScriptItemAdd;
        Inventory.ItemRemove += InventoryItemRemove;
    }

    private void InventoryScriptItemAdd(object sender, InventoryEventArgs eventItem)
    {
        Transform inventoryPanel = transform.Find("Inventory");
        foreach(Transform slot in inventoryPanel.GetChild(0))
        {
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
            ItemDrag itemDrag = slot.GetChild(0).GetChild(0).GetComponent<ItemDrag>();

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

    private void InventoryItemRemove(object sender, InventoryEventArgs eventItem)
    {
        Transform inventoryPanel = transform.Find("Inventory");
        foreach(Transform slot in inventoryPanel.GetChild(0))
        {
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
            ItemDrag itemDrag = slot.GetChild(0).GetChild(0).GetComponent<ItemDrag>();

            if(itemDrag.Item.Equals(eventItem.Item))
            {
                image.enabled = false;
                image.sprite = null;
                itemDrag.Item = null;
                break;
            }
        }
    }

    public void PickupTextOn(string e)
    {
        PickupPanel.SetActive(true);
    }

    public void PickupTextOff()
    {
        PickupPanel.SetActive(false);
    }

}
