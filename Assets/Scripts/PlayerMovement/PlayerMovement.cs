using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRB;
    private float moveSpeed;
    private const float jumpForce = 0.6f;
    private Animator animator;
    private bool onGround = true;
    private float playerHeight = 0.45f;
    private float downAccel = 0.5f;
    public Inventory inventory;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        //get references to the necessary player components
        playerRB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
        moveSpeed = 1.7f;
        checkSprint();
        onGround = isGrounded();
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
        
        /*if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            //add an impulsive jumpforce to the player's movement vector, in
            //the y direction
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }*/

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

        /*if (onGround && Input.GetButtonDown("Jump"))
        {
            //set the below 3 variables in the animation controller
            animator.SetFloat("velocityY", playerRB.velocity.y);
            animator.SetTrigger("isJumping");
            animator.SetBool("isGrounded", false);   
        }

        if (onGround)
        {
            animator.SetBool("isGrounded", true);
        }*/
    }

    //Adding pickup deactivate items and place them into inventory
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("key_silver"))
        {
            //This is to place item into the inventory made.
            IInventoryItem item = other.gameObject.GetComponent<IInventoryItem>();
            if (item != null)
            {
                //Testing to make sure object is called correctly.
                Debug.Log("Item is successfully picked up.");

                inventory.AddItem(item);
                other.gameObject.SetActive(false);
            }


        }
    }

    // void OnCollisionEnter(Collision other)
    // {
    //     if (other.collider.tag == "Dam")
    //     {
    //         isGrounded = true;
    //         animator.applyRootMotion = true;
    //     }
    // }

    // void OnCollisionExit(Collision other)
    // {
    //     if (other.collider.tag == "Dam")
    //     {
    //         isGrounded = false;
    //         animator.applyRootMotion = false;
    //     }
    // }

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
        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            //add an impulsive jump force to the player's movement vector, in
            //the y direction
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("isJumping");
            animator.SetBool("isGrounded", false);
        }
        else if (!Input.GetKey(KeyCode.Space) && isGrounded())
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isGrounded", true);
        }
        else
        {
            direction.y -= downAccel;
            animator.SetBool("isFalling", true);
        }
    }
}
