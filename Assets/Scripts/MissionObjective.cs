using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissionObjective : MonoBehaviour
{
    public GameObject missionObject;    
       
    // Start is called before the first frame update
    void Start()
    {
        missionObject.SetActive(false);
    }

    void OnTriggerEnter(Collider Player)
    {
        if (Player.gameObject.tag == "Playertag")
        {
            missionObject.SetActive(true);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
