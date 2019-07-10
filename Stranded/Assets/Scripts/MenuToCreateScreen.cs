using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuToCreateScreen : MonoBehaviour
{
    //click effect for "Create Character" text button
    public void OnMouseClick()
    {
        //redirects to CharacterCreation scene
        SceneManager.LoadScene(1);
    }
}
