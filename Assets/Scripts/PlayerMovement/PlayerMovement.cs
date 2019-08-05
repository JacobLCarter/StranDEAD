using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRB;
    private float moveSpeed;
    private const float jumpForce = 0.6f;
    private Animator animator;
    private float playerHeight = 0.45f;
    private float downAccel = 0.5f;
    public Inventory inventory;
    public HUDScript HUD;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        //get references to the necessary player components
        playerRB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        inventory.ItemUse += InventoryItemUse;
        inventory.ItemRemove += InventoryItemRemove;
    }

    private void InventoryItemRemove(object sender, InventoryEventArgs e)
    {
        TheInventoryItem item = e.Item;

        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);

        goItem.transform.parent = null;
    }

    private void InventoryItemUse(object sender, InventoryEventArgs e)
    {
        TheInventoryItem item = e.Item;

        GameObject goItem = (item as MonoBehaviour).gameObject;
        Debug.Log("What is this" + goItem.name);
        Destroy(goItem.GetComponentInChildren<Rigidbody>());
        goItem.SetActive(true);

        goItem.transform.parent = animator.GetBoneTransform(HumanBodyBones.RightHand);

    }

    void FixedUpdate()
    {
        moveSpeed = 1.7f;
        checkSprint();
        if (pickupItem != null && Input.GetKeyDown(KeyCode.E))
        {
            inventory.AddItem(pickupItem);
            pickupItem.OnPickup();
            HUD.PickupTextOff();
        }
    }

    /***************************************************************************
    Name: MovePlayer
    Description: Handles moving the player in the game space, with directions
    and input taken from the keyboard.
    Input: Vector3 for total movement, two floats for specific x and y movement.
    Output: None
    ***************************************************************************/
    public void MovePlayer(Vector3 direction, float horiz, float vert)
    {
        Jump(direction);

        //decrease movement speed if the player is crouching
        if (Input.GetKey(KeyCode.C))
        {
            moveSpeed *= 0.7f;
        }

        //move the player's position by adding a new vector based on
        //input to the current position
        //playerRB.velocity = direction * moveSpeed * Time.deltaTime;
        playerRB.transform.position += direction * moveSpeed * Time.deltaTime;

        //handle player movement animations
        UpdateAnimator(horiz, vert);
    }

    /***************************************************************************
    Name: checkSprint
    Description: Checks if the player is sprinting.
    Input: None
    Output: None
    ***************************************************************************/
    private void checkSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0)
        {
            moveSpeed *= 1.3f;
        }
    }

    /***************************************************************************
    Name: UpdateAnimator
    Description: Handles playing the various character movement animations,
    based upon a number of conditionals that are set in the animation controller.
    Input: Two floats for specific x and y movement.
    Output: None
    ***************************************************************************/
    void UpdateAnimator(float x, float y)
    {
        animator.SetFloat("Horizontal_f", x);
        animator.SetFloat("Vertical_f", y);

        //checks if the player is currently moving and presses the sprint key
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //toggle "isRunning" in the animation controller
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.C))
        {
            animator.SetBool("isCrouched", true);
        }
        else
        {
            animator.SetBool("isCrouched", false);
        }
    }

    private TheInventoryItem pickupItem = null;

    //Adding pickup deactivate items and place them into inventory
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            //This is to place item into the inventory made.
            TheInventoryItem item = other.gameObject.GetComponent<TheInventoryItem>();
            if (item != null)
            {
                pickupItem = item;
                //inventory.AddItem(item);
                //other.gameObject.SetActive(false);
                HUD.PickupTextOn("");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        TheInventoryItem item = other.GetComponent<TheInventoryItem>();
        if (item != null)
        {
            HUD.PickupTextOff();
            pickupItem = null;
        }

    }

    /*void Update()
    {
        if (pickupItem != null && Input.GetKeyDown(KeyCode.V))
        {
            inventory.AddItem(pickupItem);
            pickupItem.OnPickup();
        }
    }*/
    /***************************************************************************
    Name: isGrounded
    Description: Checks if the player is currently touching a ground surface.
    Input: None
    Output: None
    ***************************************************************************/
    bool isGrounded()
    {
        //raycast directly downwards from the player's height, if the ray hits
        //something the condition is true, else false
        return Physics.Raycast(transform.position, Vector3.down, playerHeight);
    }

    void Jump(Vector3 direction)
    {
        animator.SetBool("isGrounded", isGrounded());
        
        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            //add an impulsive jump force to the player's movement vector, in
            //the y direction
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("isJumping", true);
            //animator.SetBool("isGrounded", false);
        }
        //else if (!Input.GetKey(KeyCode.Space) && isGrounded())
        //{
            //animator.SetBool("isGrounded", true);
        //}
        else
        {
            animator.SetBool("isJumping", false);
            direction.y -= downAccel;
        }
    }
}
