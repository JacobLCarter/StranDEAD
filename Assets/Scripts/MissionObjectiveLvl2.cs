//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissionObjectiveLvl2 : MonoBehaviour
{
    public GameObject missionObject;
    //public GameObject Floor3;
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
        //Floor3.SetActive(false);
        craneRemoteLight.enabled = false;
    }

    void OnTriggerEnter(Collider Player)
    {
        if (Player.tag == "Enemy")
        {
            return;
        }

        if (!missionMaze.enabled)
        {
            if (gameObject.tag == "Maze")
            {
                missionMaze.enabled = true;
                missionMaze.color = Color.white;
            }
            return;
        }
        if (!missionCrane.enabled)
        {
            if (gameObject.tag == "Crane")
            {
                craneRemoteLight.enabled = true;
                missionCrane.enabled = true;
                missionCrane.color = Color.white;
                missionMaze.color = Color.red;
            }
            return;
        }
        if (!missionCraneMove.enabled)
        {
            if (gameObject.tag == "CraneMove")
            {
                missionCraneMove.enabled = true;
                missionCraneMove.color = Color.white;
                missionCrane.color = Color.red;
            }
            return;
        }
        if (!missionStairs.enabled)
        {
            if (gameObject.tag == "Stairs")
            {
                missionStairs.enabled = true;
                missionStairs.color = Color.white;
                missionCraneMove.color = Color.red;
            }
            return;
        }
        if (!missionFloor3.enabled)
        {
            if (gameObject.tag == "Floor3")
            {
                //Floor3.SetActive(true);
                missionFloor3.enabled = true;
                missionFloor3.color = Color.white;
                missionStairs.color = Color.red;
            }
            return;
        }
        if (!missionControl.enabled)
        {
            if (gameObject.tag == "Control")
            {
                missionControl.enabled = true;
                missionControl.color = Color.white;
                missionFloor3.color = Color.red;
            }
            return;
        }
        if (!missionControlEntrance.enabled)
        {
            if (gameObject.tag == "ControlEntrance")
            {
                missionControlEntrance.enabled = true;
                missionControlEntrance.color = Color.white;
                missionControl.color = Color.red;
            }
            return;
        }
        if (!missionPower.enabled)
        {
            if (gameObject.tag == "Power")
            {
                missionPower.enabled = true;
                missionPower.color = Color.white;
                missionControlEntrance.color = Color.red;
            }
            return;
        }
        if (!missionOverride.enabled)
        {
            if (gameObject.tag == "Override")
            {
                missionOverride.enabled = true;
                missionOverride.color = Color.white;
                missionPower.color = Color.red;
            }
            return;
        }
        if (!missionEscape.enabled)
        {
            if (gameObject.tag == "Escape")
            {
                missionEscape.enabled = true;
                missionEscape.color = Color.white;
                missionOverride.color = Color.red;
            }
            return;
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
