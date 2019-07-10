using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRB;
    private float moveSpeed;
    private Animator Animator;
    private float ForwardAmount;
    private float TurnAmount;
    private float RunCycleLegOffset;
    private float k_Half;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
    }
    
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        moveSpeed = 4f;
        checkSprint();
    }

    public void MovePlayer(Vector3 direction)
    {
        playerRB.transform.position += direction * moveSpeed * Time.deltaTime;
        TurnAmount = Mathf.Atan2(direction.x, direction.z);
		ForwardAmount = direction.z;
        UpdateAnimator(direction);
    }

    public void RotatePlayer(Vector3 rotation)
    {
        playerRB.MoveRotation(transform.rotation * Quaternion.Euler(rotation));
    }

    private void checkSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed *= 1.3f;
        }
    }

    void UpdateAnimator(Vector3 move)
		{
			// update the animator parameters
			Animator.SetFloat("Forward", ForwardAmount, 0.1f, Time.deltaTime);
			Animator.SetFloat("Turn", TurnAmount, 0.1f, Time.deltaTime);

			// calculate which leg is behind, so as to leave that leg trailing in the jump animation
			// (This code is reliant on the specific run cycle offset in our animations,
			// and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
			float runCycle =
				Mathf.Repeat(
					Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + RunCycleLegOffset, 1);
			float jumpLeg = (runCycle < k_Half ? 1 : -1) * ForwardAmount;
		}
}
