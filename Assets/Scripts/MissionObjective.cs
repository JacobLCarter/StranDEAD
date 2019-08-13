//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class MissionObjective : MonoBehaviour
{
    public GameObject missionObject;
    public GameObject key;
    public GameObject zombie;
    public GameObject blood;
    public GameObject mutant;
    public Text missionCar;
    public Text missionDam;
    public Text missionDoor;
    public Text missionKey;

    // Start is called before the first frame update
    void Start()
    {
        missionCar.enabled = false;
        missionDam.enabled = false;
        missionDoor.enabled = false;
        missionKey.enabled = false;
        zombie.gameObject.SetActive(true);
        key.SetActive(false);
        blood.SetActive(false);
        mutant.SetActive(false);

    }

    void OnTriggerEnter(Collider Player)
    {
        if (!missionCar.enabled && gameObject.tag == "Car")
        {
            missionCar.enabled = true;
            missionCar.color = Color.white;
        }
        else if (!missionDam.enabled && gameObject.tag == "Dam")
        {
            missionDam.enabled = true;
            missionDam.color = Color.white;
            missionCar.color = Color.red;
        }
        else if (!missionDoor.enabled && gameObject.tag == "Door")
        {
            key.SetActive(true);
            blood.SetActive(true);
            mutant.SetActive(true);
            mutant.GetComponent<NavMeshAgent>().SetDestination(new Vector3(-16.15351f, 44.069f, 25.528f));
            zombie.gameObject.transform.position = blood.transform.Find("trailEnd").gameObject.transform.position;
            mutant.GetComponent<AudioSource>().Play();
            missionDoor.enabled = true;
            missionDoor.color = Color.white;
            missionDam.color = Color.red;
        }
        //else if (!missionKey.enabled && gameObject.tag == "Key")
        //{
        //    missionKey.enabled = true;
        //    missionKey.color = Color.white;
        //    missionDoor.color = Color.red;
        //}
    }
}
