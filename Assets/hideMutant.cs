using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideMutant : MonoBehaviour
{
    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Enemy")
        {
            player.gameObject.SetActive(false);
        }
    }
}
