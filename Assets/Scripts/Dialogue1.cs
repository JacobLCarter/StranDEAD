using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REFERENCED: https://www.youtube.com/watch?v=CNNeD9oT4DY
public class Dialogue1 : MonoBehaviour
{
    public GameObject firstDialogue;

    // Start is called before the first frame update
    void Start()
    {
        //Sets dialogue panel to false intially
        firstDialogue.SetActive(false);

    }

    //Triggers when the player is started since the cube trigger is on the player at the start
    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Playertag")
        {
            //Sets the dialogue to being true and calls the Ienumerator
            firstDialogue.SetActive(true);
            StartCoroutine(tempDialogue());
        }

    }

    //Enumerator waits for 10 seconds after activating the dialogue then disables the dialogue and the cube so player won't retrigger it.
    IEnumerator tempDialogue()
    {
        yield return new WaitForSeconds (10);

        //Setting it to setactive false instead of destroy since destroy takes longer
        firstDialogue.SetActive(false);
        gameObject.SetActive(false);
    }
}
