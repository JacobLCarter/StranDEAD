using Boo.Lang;
using UnityEngine;
using GeneralStateMachine;
using JetBrains.Annotations;
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
    private GameObject[] rocks;
    private GameObject currentRock;
    public int stop;
    private const float SightDistance = 5f;
    private const float HearingDistance = 7f;
    private const float AttackRange = 0.5f;
    private float currentStalk;
    private int health = 100;

    private void Start()
    {
        stateMachine = new StateMachine<Enemy>(this);
        stateMachine.switchState(PathState.Instance);
        rocks = GameObject.FindGameObjectsWithTag("Rock");
    }

    private void Update()
    {
        if (health <= 0)
        {
            stateMachine.switchState(DeathState.Instance);
        }
        
        UpdateAnimator();
        
        stateMachine.Update();
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

    public bool heardNoise()
    {
        if (currentRock == null)
        {
            rocks = GameObject.FindGameObjectsWithTag("Rock");
            
            foreach (var rock in rocks)
            {
                if (Vector3.Distance(transform.position, rock.transform.position) < HearingDistance && rock.GetComponent<AudioSource>().isPlaying)
                {
                    currentRock = rock;
                    print(gameObject.name + currentRock.name);
                    return true;
                }
            }
        }
        
        return false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("Speed", navmesh.velocity.magnitude);
    }

    public float getAttackRange()
    {
        return AttackRange;
    }

    public float getStalkTime()
    {
        return currentStalk;
    }

    public void setCurrentStalk(float stalk)
    {
        currentStalk = stalk;
    }

    public GameObject getCurrentRock()
    {
        return currentRock;
    }
}