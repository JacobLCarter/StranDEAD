using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{

    public static int deathScreen = 5;
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
