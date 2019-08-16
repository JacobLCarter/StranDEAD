//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissionObjectiveLvl2 : MonoBehaviour
{
    public GameObject missionObject;
    public GameObject Floor3;
    public Light craneRemoteLight;
    public Text missionMaze;
    public Text missionCrane;
    public Text missionCraneMove;
    public Text missionStairs;
    public Text missionFloor3;
    public Text missionControl;
    public Text missionControlEntrance;
    public Text missionPower;
    public Text missionOverride;
    public Text missionEscape;

    // Start is called before the first frame update
    void Start()
    {
        missionMaze.enabled = false;
        missionCrane.enabled = false;
        missionCraneMove.enabled = false;
        missionStairs.enabled = false;
        missionFloor3.enabled = false;
        missionControl.enabled = false;
        missionControlEntrance.enabled = false;
        missionPower.enabled = false;
        missionOverride.enabled = false;
        missionEscape.enabled = false;
        Floor3.SetActive(false);
        craneRemoteLight.enabled = false;
    }

    void OnTriggerEnter(Collider Player)
    {
        if (!missionMaze.enabled && gameObject.tag == "Maze")
        {
            missionMaze.enabled = true;
            missionMaze.color = Color.white;
        }
        else if (!missionCrane.enabled && gameObject.tag == "Crane")
        {
            craneRemoteLight.enabled = true;
            missionCrane.enabled = true;
            missionCrane.color = Color.white;
            missionMaze.color = Color.red;
        }
        else if (!missionCraneMove.enabled && gameObject.tag == "CraneMove")
        {
            missionCraneMove.enabled = true;
            missionCraneMove.color = Color.white;
            missionCrane.color = Color.red;
        }
        else if (!missionStairs.enabled && gameObject.tag == "Stairs")
        {
            missionStairs.enabled = true;
            missionStairs.color = Color.white;
            missionCraneMove.color = Color.red;
            
        }
        else if (!missionFloor3.enabled && gameObject.tag == "Floor3")
        {
            Floor3.SetActive(true);
            missionFloor3.enabled = true;
            missionFloor3.color = Color.white;
            missionStairs.color = Color.red;
        }
        else if (!missionControl.enabled && gameObject.tag == "Control")
        {
            missionControl.enabled = true;
            missionControl.color = Color.white;
            missionFloor3.color = Color.red;
        }
        else if (!missionControlEntrance.enabled && gameObject.tag == "ControlEntrance")
        {
            missionControlEntrance.enabled = true;
            missionControlEntrance.color = Color.white;
            missionControl.color = Color.red;
        }
        else if (!missionPower.enabled && gameObject.tag == "Power")
        {
            missionPower.enabled = true;
            missionPower.color = Color.white;
            missionControlEntrance.color = Color.red;
        }
        else if (!missionOverride.enabled && gameObject.tag == "Override")
        {
            missionOverride.enabled = true;
            missionOverride.color = Color.white;
            missionPower.color = Color.red;
        }
        else if (!missionEscape.enabled && gameObject.tag == "Escape")
        {
            missionEscape.enabled = true;
            missionEscape.color = Color.white;
            missionOverride.color = Color.red;
        }
    }
}

//key.SetActive(true);
//blood.SetActive(true);
//mutant.SetActive(true);
//mutant.GetComponent<NavMeshAgent>().SetDestination(new Vector3(-16.15351f, 44.069f, 25.528f));
//zombie.gameObject.transform.position = blood.transform.Find("trailEnd").gameObject.transform.position;
//mutant.GetComponent<AudioSource>().Play();
//missionDoor.enabled = true;
//missionDoor.color = Color.white;
//missionDam.color = Color.red;
