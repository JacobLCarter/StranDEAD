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
    public Transform sliders;
    public List<string> maleHairOptions = new List<string>();
    public List<string> femaleHairOptions = new List<string>();
    private int hairIndex;
    public string customChar;

    /***************************************************************************
    Name: OnEnable
    Description: This function is called when the object becomes enabled and active.
    Input: None
    Output: None
    ***************************************************************************/
    void OnEnable()
    {
        //checks if the character has been changed in any way and calls if so
        player.CharacterUpdated.AddListener(PlayerUpdated);

        //checks if any slider under the parent object has been changed
        foreach (Transform child in sliders.transform)
        {
            //calls OnSliderChanged if current value is different from last
            child.gameObject.GetComponent<Slider>().onValueChanged.AddListener(OnSliderChanged);
        }
    }

    /***************************************************************************
    Name: OnEnable
    Description: This function is called when the object becomes disabled.
    Input: None
    Output: None
    ***************************************************************************/
    void OnDisable()
    {
        player.CharacterUpdated.RemoveListener(PlayerUpdated);

        foreach (Transform child in sliders.transform)
        {
            child.gameObject.GetComponent<Slider>().onValueChanged.RemoveListener(OnSliderChanged);
        }
    }

    /***************************************************************************
    Name: PlayerUpdated
    Description: This function is called when the object becomes enabled and active.
    Input: UMAData for all of the players current physical data
    Output: None
    ***************************************************************************/
    void PlayerUpdated(UMAData data)
    {
        //grabs the player's current DNA information
        dna = player.GetDNA();

        //gets the current value of each slider and sets them accordingly in Unity
        foreach (Transform child in sliders.transform)
        {
            child.gameObject.GetComponent<Slider>().value = dna[child.gameObject.name].Get();
        }
    }

    /***************************************************************************
    Name: ChangeGender
    Description: Changes the current gender of the player.
    Input: Boolean representing if the female button is pressed
    Output: None
    ***************************************************************************/
    public void ChangeGender(bool female)
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

    /***************************************************************************
    Name: ChangeSkinColor
    Description: Changes the current skin color of the player.
    Input: Color representing the color to change to
    Output: None
    ***************************************************************************/
    public void ChangeSkinColor(Color color)
    {
        player.SetColor("Skin", color);
        //force a visual update of the character
        player.UpdateColors(true);
    }

    /***************************************************************************
    Name: ChangeHairColor
    Description: Changes the current hair color of the player.
    Input: Color representing the color to change to
    Output: None
    ***************************************************************************/
    public void ChangeHairColor(Color color)
    {
        player.SetColor("Hair", color);
        player.UpdateColors(true);
    }

    /***************************************************************************
    Name: ChangeEyeColor
    Description: Changes the current eye color of the player.
    Input: Color representing the color to change to
    Output: None
    ***************************************************************************/
    public void ChangeEyeColor(Color color)
    {
        player.SetColor("Eyes", color);
        player.UpdateColors(true);
    }

    /***************************************************************************
    Name: ChangeHair
    Description: Changes the current skin color of the player.
    Input: Boolean representing if the "next hair" button is pressed
    Output: None
    ***************************************************************************/
    public void ChangeHair(bool plus)
    {
        if (player.activeRace.name == "HumanMaleDCS")
        {
            //move through an list of pre-loaded hairstyles based on the index
            if (plus)
            {
                hairIndex++;
            }
            else
            {
                hairIndex--;
            }

            //lock the index value inside the values of the current array
            hairIndex = Mathf.Clamp(hairIndex, 0, maleHairOptions.Count - 1);

            if (maleHairOptions[hairIndex] == "None")
            {
                //empty the avatar's hair slot completely
                player.ClearSlot("Hair");
            }
            else
            {
                //set the avatar's hair slot to the current index value of the list
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

        //force a player update in Unity
        player.BuildCharacter();
    }

    /***************************************************************************
    Name: SaveChar
    Description: Saves all of the current attributes of the player to a text
    file. Allows for loading into other levels.
    Input: None
    Output: None
    ***************************************************************************/
    public void SaveChar()
    {
        //stores all current values in the player's UMA recipe
        customChar = player.GetCurrentRecipe();

        //write the recipe to a text file
        File.WriteAllText(Application.persistentDataPath + "/customChar.txt", customChar);
    }

    /***************************************************************************
    Name: LoadChar
    Description: Loads all of the current attributes of the player from a text
    file into the player avatar in Unity.
    Input: None
    Output: None
    ***************************************************************************/
    public void LoadChar()
    {
        //read the data from the saved text file into variable
        customChar = File.ReadAllText(Application.persistentDataPath + "/customChar.txt");
        //clear any current modifications that the player avatar has
        player.ClearSlots();
        //load the recipe into the player avatar
        player.LoadFromRecipeString(customChar);
    }
    
    /***************************************************************************
    Name: OnSliderChanged
    Description: Sets the value of a character attribute based on the value of
    the current active slider.
    Input: float representing the current value of the slider
    Output: None
    ***************************************************************************/
    public void OnSliderChanged(float val)
    {
        //gets the game object that is currently set to active
        var co = EventSystem.current.currentSelectedGameObject;

        //break out if there is no active object
        if (co == null)
        {
            return;
        }

        //set the game object to a type of slider
        Slider currentSlider = co.GetComponent(typeof(Slider)) as Slider;

        //set the character attribute based on the slider value and name
        if (currentSlider != null)
        {
            dna[co.name].Set(val);
        }

        //force a player update in Unity
        player.BuildCharacter();
    }
}