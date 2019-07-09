using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA;
using UMA.CharacterSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class CharacterCreator : MonoBehaviour
{
    public DynamicCharacterAvatar player;
    private Dictionary<string, DnaSetter> dna;
    public Slider height;
    public Slider weight;
    public Slider headSz;
    public Transform sliders;
    public List<string> maleHairOptions = new List<string>();
    public List<string> femaleHairOptions = new List<string>();
    private int hairIndex;

    public string customChar;

    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        player.CharacterUpdated.AddListener(PlayerUpdated);
        height.onValueChanged.AddListener(ChangeHeight);
        weight.onValueChanged.AddListener(ChangeWeight);
        headSz.onValueChanged.AddListener(ChangeHeadSize);
    }

    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        player.CharacterUpdated.RemoveListener(PlayerUpdated);
        height.onValueChanged.RemoveListener(ChangeHeight);
        weight.onValueChanged.RemoveListener(ChangeWeight);
        headSz.onValueChanged.RemoveListener(ChangeHeadSize);
        
    }

    void PlayerUpdated(UMAData data)
    {
        dna = player.GetDNA();
        height.value = dna["height"].Get();
        weight.value = dna["belly"].Get();
        headSz.value = dna["headSize"].Get();
        // foreach (Transform child in sliders.transform)
        // {
        //     child.gameObject.GetComponent<Slider>().value = dna[child.gameObject.name].Get();
        // }
    }

    public void changeGender(bool female)
    {
        if (female && player.activeRace.name != "HumanFemaleDCS")
        {
            player.ChangeRace("HumanFemaleDCS");
        }
        else if (!female && player.activeRace.name != "HumanMaleDCS")
        {
            player.ChangeRace("HumanMaleDCS");
        }
    }

    public void ChangeHeight(float val)
    {
        dna["height"].Set(val);
        player.BuildCharacter();
    }

    public void ChangeWeight(float val)
    {
        dna["belly"].Set(val);
        player.BuildCharacter();
    }

    public void ChangeHeadSize(float val)
    {
        dna["headSize"].Set(val);
        player.BuildCharacter();
    }

    public void ChangeSkinColor(Color color)
    {
        player.SetColor("Skin", color);
        player.UpdateColors(true);
    }

    public void ChangeHair(bool plus)
    {
        if (player.activeRace.name == "HumanMaleDCS")
        {
            if (plus)
            {
                hairIndex++;
            }
            else
            {
                hairIndex--;
            }

            hairIndex = Mathf.Clamp(hairIndex, 0, maleHairOptions.Count - 1);

            if (maleHairOptions[hairIndex] == "None")
            {
                player.ClearSlot("Hair");
            }
            else
            {
                player.SetSlot("Hair", maleHairOptions[hairIndex]);
            }
        }
        else
        {
            if (plus)
            {
                hairIndex++;
            }
            else
            {
                hairIndex--;
            }

            hairIndex = Mathf.Clamp(hairIndex, 0, femaleHairOptions.Count - 1);

            if (femaleHairOptions[hairIndex] == "None")
            {
                player.ClearSlot("Hair");
            }
            else
            {
                player.SetSlot("Hair", femaleHairOptions[hairIndex]);
            }
        }

        player.BuildCharacter();
    }

    public void ChangeHairColor(Color color)
    {
        player.SetColor("Hair", color);
        player.UpdateColors(true);
    }

    public void SaveChar()
    {
        customChar = player.GetCurrentRecipe();

        File.WriteAllText(Application.persistentDataPath + "/customChar.txt", customChar);
    }

    public void LoadChar()
    {
        customChar = File.ReadAllText(Application.persistentDataPath + "/customChar.txt");
        player.ClearSlots();
        player.LoadFromRecipeString(customChar);
    }
    
    // public void OnSliderChanged(float val)
    // {
    //     var currentObject = EventSystem.current.currentSelectedGameObject;

    //     if (currentObject == null)
    //     {
    //         return;
    //     }

    //     Slider currentSlider = currentObject.GetComponent(typeof(Slider)) as Slider;

    //     if (currentSlider != null)
    //     {
    //         dna[EventSystem.current.currentSelectedGameObject.name].Set(val);
    //     }

    //     player.BuildCharacter();
    // }
}