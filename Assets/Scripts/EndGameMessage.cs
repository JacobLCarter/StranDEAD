using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameMessage : MonoBehaviour
{
    public Text escapeMessage;
    [SerializeField] private readonly float delayBeforeDisplayingText = 2.5f;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        escapeMessage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > delayBeforeDisplayingText)
        {
            escapeMessage.enabled = true;
        }
    }
}
