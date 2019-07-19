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
    public Transform[] stops;
    public int stop;
    private bool playerFound;
    public const float SightDistance = 10f;
    public const float AttackRange = 1.7f;
    public float currentStalk;
    public float stalkTime = 8f;

    private void Start()
    {
        stateMachine = new StateMachine<Enemy>(this);
        stateMachine.switchState(PathState.Instance);

        currentStalk = stalkTime;
    }

    private void Update()
    {
        if (playerFound)
        {
            if (Vector3.Distance(transform.position, player.position) < AttackRange && !isSameState(AttackState.Instance))
            {
                stateMachine.switchState(AttackState.Instance);
            }
            else if (!isSameState(ChaseState.Instance))
            {
                stateMachine.switchState(ChaseState.Instance);
            }
        }
        else if (currentStalk > 0)
        {
            if (!isSameState(StalkState.Instance))
            {
                stateMachine.switchState(StalkState.Instance);
            }
        }
        else
        {
            if (!isSameState(PathState.Instance))
            {
                stateMachine.switchState(PathState.Instance);
            }
        }

        UpdateAnimator();
        
        stateMachine.Update();
    }

    private bool isSameState(State<Enemy> nextState)
    {
        return stateMachine.currentState == nextState;
    }
    
    public void isPlayerInSight()
    {
        Vector3 target = player.position - transform.position;
        float angle = Vector3.Angle(target, transform.forward);

        if (angle < 80.0f)
        {
            Vector3 direction = player.position - enemyHead.position;
            Ray ray = new Ray(enemyHead.position, direction);
            RaycastHit hit;
            var rayColor = playerFound ? Color.red : Color.green;

            if (Physics.Raycast(ray, out hit, SightDistance))
            {
                if (hit.collider.transform != null)
                {
                    Debug.DrawLine(enemyHead.position, hit.point, rayColor);
                    
                    if (hit.collider.transform == player)
                    {
                        playerFound = true;
                        return;
                    }
                }
                else
                {
                    Debug.DrawLine(enemyHead.position, direction * SightDistance, rayColor);
                }
            }
        }
        
        playerFound = false;
    }
    
    private void UpdateAnimator()
    {
        if (stateMachine.currentState != AttackState.Instance)
        {
            animator.SetFloat("Speed", navmesh.velocity.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0.0f);
        }
    }
}
