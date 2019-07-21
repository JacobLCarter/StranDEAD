using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REFERENCED: https://www.youtube.com/watch?v=CNNeD9oT4DY
public class Hint1 : MonoBehaviour
{
    //Set game object
    public GameObject firstHint;

    // Start is called before the first frame update
    void Start()
    {
        //Sets the hint object box to false
        firstHint.SetActive(false);
    }

    //Ontrigger function that sets the dialogue box as active then calls upon the inEnumerator which will display the hint for 8 seconds
    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Playertag")
        {
            firstHint.SetActive(true);
            StartCoroutine(tempHint());
        }
    }

    IEnumerator tempHint()
    {
        yield return new WaitForSeconds(8);

        //Deactivates the dialogue box and the box collider used for triggering the hint.
        firstHint.SetActive(false);
        gameObject.SetActive(false);
    }
}
