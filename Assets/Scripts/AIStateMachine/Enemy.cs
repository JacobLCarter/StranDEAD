using UnityEngine;
using GeneralStateMachine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public StateMachine<Enemy> stateMachine { get; set; }
    public Transform player;
    public Transform enemyHead;
    public NavMeshAgent navmesh;
    public Animator animator;
    public Animator playerAnimator;
    public Transform[] stops;
    public int stop;
    public const float SightDistance = 6f;
    public const float HearingDistance = 10f;
    public float AttackRange = 1.7f;
    public float currentStalk;
    public float stalkTime = 10f;

    private void Start()
    {
        stateMachine = new StateMachine<Enemy>(this);
        stateMachine.switchState(PathState.Instance);
    }

    private void Update()
    {
        UpdateAnimator();
        
        stateMachine.Update();
    }

    private bool isSameState(State<Enemy> nextState)
    {
        return stateMachine.currentState == nextState;
    }
    
    public bool isPlayerInSight()
    {
        Vector3 target = player.position - transform.position;
        float angle = Vector3.Angle(target, transform.forward);

        if (angle < 80.0f)
        {
            Vector3 direction = player.position - enemyHead.position;
            Ray ray = new Ray(enemyHead.position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, SightDistance))
            {
                if (hit.collider.transform != null)
                {
                    if (hit.collider.transform == player)
                    {
                        return true;
                    }
                }
            }
        }
        
        return false;
    }

    public bool isPlayerAudible()
    {
        if (playerAnimator.GetBool("isRunning") || playerAnimator.GetBool("isJumping"))
        {
            if (Vector3.Distance(transform.position, player.position) < HearingDistance)
            {
                return true;
            }
        }

        return false;
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("Speed", navmesh.velocity.magnitude);
        
        /*if (stateMachine.currentState != AttackState.Instance)
        {
            animator.SetFloat("Speed", navmesh.velocity.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0.0f);
        }*/
    }
}