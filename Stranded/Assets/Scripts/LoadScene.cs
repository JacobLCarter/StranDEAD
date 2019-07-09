using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    bool isLoaded;

    public void PlayGame()
    {
        if (!isLoaded)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            isLoaded = true;
        }
    }
}