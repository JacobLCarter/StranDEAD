using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRB;
    private float moveSpeed;
    private const float jumpForce = 1.1f;
    private Animator animator;
    private bool isGrounded = true;
    private const float runCycleLegOffset = 0.3f;
    private const float k_Half = 0.5f;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
        moveSpeed = 2f;
        checkSprint();
    }

    public void MovePlayer(Vector3 direction)
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            direction.y = jumpForce;
        }

        playerRB.transform.position += direction * moveSpeed * Time.deltaTime;

        UpdateAnimator(direction.z);
    }

    public void RotatePlayer(Vector3 rotation)
    {
        playerRB.MoveRotation(playerRB.rotation * Quaternion.Euler(rotation));
    }

    private void checkSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed *= 1.2f;
        }
    }

    void UpdateAnimator(float forward)
    {
        float runCycle = Mathf.Repeat(animator.GetCurrentAnimatorStateInfo(0).normalizedTime + runCycleLegOffset, 1);
		float jumpLeg = (runCycle < k_Half ? 1 : -1) * forward;

        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.Play("HumanoidRun");
            }
            else
            {
                animator.Play("WalkFWD");
            }
        }
        else if (Input.GetKey (KeyCode.A))
        {
            animator.Play ("StrafeLeft");
        }
        else if (Input.GetKey (KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.Play("HumanoidRun");
            }
            else
            {
                animator.Play("HumanoidWalk");
            }
        } 
        else if (Input.GetKey (KeyCode.D))
        {
            animator.Play ("StrafeLeft");
        }
        else
        {
            animator.Play ("HumanoidIdle");
        }

        if (!isGrounded)
        {
            animator.Play("Fall");
            // animator.Play("Fall");
            // animator.Play("Land");
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Dam")
        {
            isGrounded = true;
            animator.applyRootMotion = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.collider.tag == "Dam")
        {
            isGrounded = false;
            animator.applyRootMotion = false;
        }
    }
}