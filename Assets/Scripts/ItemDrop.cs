using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//REFERENCED: https://www.youtube.com/watch?v=Pc8K_DVPgVM&list=PLboXykqtm8dynMisqs4_oKvAIZedixvtf&index=3
public class ItemDrop : MonoBehaviour, IDropHandler
{
    public Inventory inventory;
    //Drops the item
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform invPanel = transform as RectTransform;

        //Checks if the mouse pointer is within the inventory panel or not. If not then it will indicate that the item has been dropped
        if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {

            TheInventoryItem item = eventData.pointerDrag.gameObject.GetComponent<ItemDrag>().Item;
            if (item != null)
            {
                Debug.Log("It has been dropped " + item);
                inventory.RemoveItem(item);
                item.OnDrop();
            }
        }
    }

}
