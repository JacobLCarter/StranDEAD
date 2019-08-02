using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REFERENCED: https://www.youtube.com/watch?v=fAk5ZRevKs8&list=PLboXykqtm8dynMisqs4_oKvAIZedixvtf&index=5
public class InventoryItemMain : MonoBehaviour, IInventoryItem
{
    public virtual string Name
    {
        get
        {
            return "baseItem";
        }
    }

    public Sprite _Image;

    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public virtual void OnUse()
    {

    }
    public virtual void OnDrop()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.Log("Not opening");
        if (Physics.Raycast(ray, out hit, 1000))
        {
            Debug.Log("Raycast worked");
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
        }
    }

    public virtual void OnPickup()
    {
        gameObject.SetActive(false);
    }

}
