using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd2 : MonoBehaviour
{
    //recognize player collision with deathzone on cliffs/water
    private void OnTriggerEnter()
    {
        //starts loading screen
        SceneManager.LoadScene(7);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }
}
