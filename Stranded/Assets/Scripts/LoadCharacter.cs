using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UMA;
using UMA.CharacterSystem;

public class LoadCharacter : MonoBehaviour
{
    public string customChar;
    public DynamicCharacterAvatar player;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        LoadChar();
    }
    public void LoadChar()
    {
        customChar = File.ReadAllText(Application.persistentDataPath + "/customChar.txt");
        player.ClearSlots();
        player.LoadFromRecipeString(customChar);
    }
}
