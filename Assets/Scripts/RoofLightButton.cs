using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofLightButton : MonoBehaviour
{
    private GameObject[] roofLights;
    private bool lightsOn = false;
    private int lightIdx = 0;
    private float timeElapsed = 0;
    public GameObject player;
    public GameObject levelEnd2;


    // Start is called before the first frame update
    void Start()
    {
        if (roofLights == null)
        {
            roofLights = GameObject.FindGameObjectsWithTag("RoofLight");
            foreach(GameObject roofLight in roofLights)
            {
                roofLight.SetActive(false);
            }
            lightIdx = roofLights.Length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // in mode where lights need to be turned on?
        if (lightsOn)
        {
            // all lights turned on?
            if (lightIdx == 0)
            {
                return;
            }
            
            // turn on row of lights every 500 ms
            timeElapsed += Time.deltaTime;
            if (timeElapsed < 0.5f)
            {
                // too early
                return;
            }
            // reset timer
            timeElapsed = 0;

            // turn on row  (3 lights per row)
            for (int i = 0; i < 3; i++)
            {
                if (lightIdx == 0)
                {
                    return;
                }
                // go in reverse order (from control room towards start of level)
                lightIdx--;
                roofLights[lightIdx].SetActive(true);
            }
        }
        else
        {
            // enter lights-on mode
            if (Input.GetKeyDown(KeyCode.E) && isObjectReachable())
            {
                player.GetComponent<Animator>().SetTrigger("isPressing");
                lightsOn = true;
                levelEnd2.SetActive(true);
            }
        }
    }
    
    public bool isObjectReachable()
    {
        Vector3 target = player.transform.position - transform.position;
        float angle = Vector3.Angle(target, transform.position);

        if (angle < 90.0f && Vector3.Distance(transform.position, player.transform.position) <= 0.5f)
        {
            return true;
        }
        
        return false;
    }
}