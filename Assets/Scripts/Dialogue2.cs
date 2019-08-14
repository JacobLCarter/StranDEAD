using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REFERENCED: https://www.youtube.com/watch?v=CNNeD9oT4DY
public class Dialogue2 : MonoBehaviour
{
    public GameObject secondDialogue;

    // Start is called before the first frame update
    void Start()
    {
        secondDialogue.SetActive(false);
    }

    //Checks if player collides with the object if so then the dialogue UI is activated
    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Playertag")
        {
            secondDialogue.SetActive(true);
            StartCoroutine(tempDialogue());
        }
    }

    //Deactivates the UI after a certain amount of time and the gameobject itself to disallow repeated collisions
    IEnumerator tempDialogue()
    {
        yield return new WaitForSeconds(5);

        secondDialogue.SetActive(false);
        gameObject.SetActive(false);
    }
}
