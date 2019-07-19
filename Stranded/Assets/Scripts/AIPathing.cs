using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPathing : MonoBehaviour
{
    public Transform player;
    public Transform enemyHead;
    private NavMeshAgent AI_navmesh;
    private Animator animator;
    public Transform[] stops;
    private int stop;
    private bool playerFound = false;
    private bool alreadyFound = true;
    private const float SightDistance = 10f;
    private const float AttackRange = 1.7f;
    private float stopTime = 4f;
    private const float stalkTime = 5f;
    private float currentStalk;
    private Vector3 attackOffset = new Vector3(.3f, 0,0);

    // Start is called before the first frame update
    void Start()
    {
        AI_navmesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentStalk = stalkTime;

        WalkPath();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (playerFound)
        {
            if (Vector3.Distance(transform.position, player.position) < AttackRange)
            {
                Attack();
                currentStalk = stalkTime;
            }
            else
            {
                Chase();
                
                currentStalk -= stalkTime;

                if (currentStalk <= 0f)
                {
                    stopChase();
                }
            }
        }
        else if (isPlayerInSight())// && rayCheck())
        {
            startChase();
        }
        else
        {
            if (!AI_navmesh.pathPending && AI_navmesh.remainingDistance < 0.2f)
            {
                if (stopTime <= 0)
                {
                    stopTime = 4f;
                    WalkPath();
                }
                else
                {
                    stopTime -= Time.deltaTime;
                }
            }
        }

        UpdateAnimator();
    }

    bool isPlayerInSight()
    {
        Vector3 target = player.position - transform.position;
        float angle = Vector3.Angle(target, transform.forward);

        if (angle < 80.0f)
        {
            Vector3 direction = player.position - enemyHead.position;
            Ray ray = new Ray(enemyHead.position, direction);
            RaycastHit hit;
            var rayColor = playerFound ? Color.red : Color.green;

            //Debug.DrawLine(enemyHead.position, direction * SightDistance, rayColor);
            
            if (Physics.Raycast(ray, out hit, SightDistance))
            {
                if (hit.collider.transform != null)
                {
                    Debug.DrawLine(enemyHead.position, hit.point, rayColor);
                    
                    if (hit.collider.transform == player)
                    {
                        return true;
                    }
                }
                else
                {
                    Debug.DrawLine(enemyHead.position, direction * SightDistance, rayColor);
                }
            }
        }
        
        return false;
    }

    private void WalkPath()
    {
        AI_navmesh.speed = .75f;

        if (stops.Length == 0)
        {
            return;
        }

        AI_navmesh.destination = stops[stop].position;

        //stop = (stop + 1) % stops.Length;
        stop = Random.Range(0, stops.Length - 1);
    }

    private void Chase()
    {
        AI_navmesh.speed = 1.5f;

        if (player != null)
        {
            Vector3 target = player.position;
            AI_navmesh.SetDestination(target);
        }
    }

    private void Attack()
    {
        //AI_navmesh.isStopped = true;
        //transform.LookAt(player.position + attackOffset);
        animator.SetTrigger("isAttacking");
        //AI_navmesh.isStopped = false;
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("Speed", AI_navmesh.velocity.magnitude);
    }

    private void stopChase()
    {
        if (!isPlayerInSight())
        {
            playerFound = false;
        }

        currentStalk = stalkTime;
    }

    private void startChase()
    {
        playerFound = true;

        Chase();
    }
}