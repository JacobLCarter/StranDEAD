using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//REFERENCED: https://www.youtube.com/watch?v=Pc8K_DVPgVM&list=PLboXykqtm8dynMisqs4_oKvAIZedixvtf&index=3
//REFERENCED: https://www.youtube.com/watch?v=c47QYgsJrWc
public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform startParent;
    Vector3 startPosition;
    Vector3 newPosition;
    Vector3 mouse;
    Vector3 temp;

    public static GameObject itemBeingDragged;
    public TheInventoryItem Item { get; set; }

    //Sets variables to be used for later. Especially the position of everything at the moment of drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        mouse = Input.mousePosition;
    }

    //Follows the mouse on drag
    public void OnDrag(PointerEventData eventData)
    {
        //Mouse position starts outside of game for some reason so had to subtract mouse position from the original mouse position to get the startposition of the image in the inventory
        transform.position = Input.mousePosition + startPosition - mouse;
    }

    //After drag is done it will snap back to the original spot or dropped depending on placement on the screen. Some issues of not dropping occurs.
    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        if (transform.parent != startParent)
        {
            transform.position = startPosition;
        }
        transform.position = startPosition;
    }
}
