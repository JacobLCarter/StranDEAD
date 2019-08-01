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
    public IInventoryItem Item { get; set; }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        mouse = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Mouse position starts outside of game for some reason so had to subtract mouse position from the original mouse position to get the startposition of the image in the inventory
        transform.position = Input.mousePosition + startPosition - mouse;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        if (transform.parent != startParent)
        {
            transform.position = startPosition;
        }
        transform.position = startPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
