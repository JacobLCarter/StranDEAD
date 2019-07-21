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

    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Playertag")
        {
            secondDialogue.SetActive(true);
            StartCoroutine(tempDialogue());
        }
    }

    IEnumerator tempDialogue()
    {
        yield return new WaitForSeconds(5);

        secondDialogue.SetActive(false);
        gameObject.SetActive(false);
    }
}
