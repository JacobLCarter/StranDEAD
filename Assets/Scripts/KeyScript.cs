using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REFERENCED: https://www.youtube.com/watch?v=Hj7AZkyojdo&list=PLboXykqtm8dynMisqs4_oKvAIZedixvtf&index=2
public class KeyScript : MonoBehaviour, IInventoryItem
{
    //Returns the key name of key_silver
    public string Name
    {
        get
        {
            return "key_silver";
        }
    }

    //Sets the sprite image to null before returning the image
    public Sprite _Image = null;

    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    //Sets item to not active on call of onpickup
    public void OnPickup()
    {
        gameObject.SetActive(false);
    }

    public void OnDrop()
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
}
