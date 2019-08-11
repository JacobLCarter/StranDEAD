using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTransfer : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
