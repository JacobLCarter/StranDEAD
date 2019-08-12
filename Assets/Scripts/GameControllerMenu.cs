using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

//REFERENCES: https://www.youtube.com/watch?v=4X67kWGcYQw
//REFERENCES: https://docs.unity3d.com/ScriptReference/Cursor-lockState.html
public class GameControllerMenu : MonoBehaviour
{
    public GameObject inventory, player, camera;

    //Boolean to check if the cursor is locked or not locked
    bool CursorLocked;

    // Start is called before the first frame update
    void Start()
    {
        CursorLocked = (true);
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if cursor is not locked on I press in order to toggle inventory off and lock cursor
        if (Input.GetKeyDown(KeyCode.I) && !CursorLocked)
        {
            ToggleInventory();
            LockCursor();
        }

        //Checks if cursor is locke don I press in order to toggle inventory on and unlock cursor
        else if (Input.GetKeyDown(KeyCode.I) && CursorLocked)
        {
            ToggleInventory();
            UnlockCursor();
        }

    }

    //Toggles inventory on or off as well as set the cursor to be visible and locks and unlocks the cursor
    void ToggleInventory()
    {
        inventory.SetActive(!inventory.activeInHierarchy);
        Cursor.visible = inventory.activeInHierarchy;
        DisablePlayer(!inventory.activeInHierarchy);
    }

    //Locks cursor in its unlocked state
    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        CursorLocked = (true);
    }

    //Unlocks the cursor from it's locked state
    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        CursorLocked = (false);
    }

    
    //Disables the camerafollow script which freezes the character in place for the user to move their cursor around freely in the inventory
    void DisablePlayer(bool flag)
    {
        Debug.Log("Hello" + flag);
        player.GetComponent<CameraFollow>().enabled = flag;
        camera.GetComponent<CameraCollision>().enabled = flag;
    }  

}
