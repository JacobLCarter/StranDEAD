using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    /***************************************************************************
    Name: PlayGame
    Description: Loads a scene based on the index of the current scene.
    Input: None
    Output: None
    ***************************************************************************/
    public void PlayGame()
    {
        //loads the scene that comes directly after the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}