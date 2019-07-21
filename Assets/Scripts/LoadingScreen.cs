using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{

    public static int loadingScreen = 1;
    [SerializeField] private readonly float delayBeforeRestartingLevel = 3f;

    private float timeElapsed;

    // Update is called once per frame
    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > delayBeforeRestartingLevel)
        {
            //load level_1
            SceneManager.LoadScene(3);
        }
    }
}
