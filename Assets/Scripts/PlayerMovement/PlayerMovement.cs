using UnityEngine;
using GeneralStateMachine;

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
    private TheInventoryItem myCurrentItem = null;
    public Inventory inventory;
    public HUDScript HUD;

    public StateMachine<Axe> stateMachine;

    public GameObject zombie;

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

    //Removes object item and places it in a location active
    //REFERENCED: https://www.youtube.com/watch?v=Pc8K_DVPgVM&list=PLboXykqtm8dynMisqs4_oKvAIZedixvtf&index=3
    private void InventoryItemRemove(object sender, InventoryEventArgs e)
    {
        TheInventoryItem item = e.Item;

        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);

        goItem.transform.parent = null;
    }

    //Checks tag to either have the item go active to be used as a general pickup item. Instant touch which is made to instantly pickup the flashlight
    //in order to simulate carrying items over to the next level. If attackWeapon it will place the item in hand using inventory scripts then calls upon
    //the item script states in order to utilize the other movements
    //REFERENCED: https://www.youtube.com/watch?v=twjrs4u_5bc&list=PLboXykqtm8dynMisqs4_oKvAIZedixvtf&index=6
    private void InventoryItemUse(object sender, InventoryEventArgs e)
    {
        TheInventoryItem item = e.Item;

        GameObject goItem = (item as MonoBehaviour).gameObject;
        Debug.Log("What is this" + goItem.name);

        if (goItem.tag == "Pickup" || goItem.tag == "attackWeapon" || goItem.tag == "instantTouch")
        {
            if (goItem.activeSelf == false)
            {
                goItem.SetActive(true);
            }

            else
            {
                goItem.SetActive(false);
            }

        }

        if (goItem.tag == "attackWeapon")
        {
            goItem.transform.parent = animator.GetBoneTransform(HumanBodyBones.LeftMiddleDistal);
        }
        else
        {
            goItem.transform.parent = animator.GetBoneTransform(HumanBodyBones.RightHand);
        }
    }

    //Gets the keycode for the key E to pickup items with certain tags. Also turns off the pickup text after pickup is made
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
    //REFERENCED: https://www.youtube.com/watch?v=Hj7AZkyojdo&list=PLboXykqtm8dynMisqs4_oKvAIZedixvtf
    //REFERENCED: https://www.youtube.com/watch?v=90OiysC4j5Y&list=PLboXykqtm8dynMisqs4_oKvAIZedixvtf&index=11
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup") || other.gameObject.CompareTag("itemNotUsed") || other.gameObject.CompareTag("attackWeapon") || other.gameObject.CompareTag("instantTouch"))
        {
            //This is to place item into the inventory made.
            TheInventoryItem item = other.gameObject.GetComponent<TheInventoryItem>();
            if (item != null && !other.gameObject.CompareTag("instantTouch"))
            {
                pickupItem = item;
                HUD.PickupTextOn("");
            }

            if (other.gameObject.CompareTag("instantTouch"))
            {
                inventory.AddItem(item);
            }

        }
    }

    //When out of the trigger field the pickuptext UI will be turned off
    void OnTriggerExit(Collider other)
    {
        TheInventoryItem item = other.GetComponent<TheInventoryItem>();
        if (item != null)
        {
            HUD.PickupTextOff();
            pickupItem = null;
        }

    }

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
