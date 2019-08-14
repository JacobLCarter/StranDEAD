using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEventHandler : MonoBehaviour
{
    public GameObject weapon;
    
    private void EnableDamage()
    {
        weapon.SetActive(true);
    }

    private void DisableDamage()
    {
        weapon.SetActive(false);
    }
}