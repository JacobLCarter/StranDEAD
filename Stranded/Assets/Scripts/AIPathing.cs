using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPathing : MonoBehaviour
{
    
    public Transform player;
    //public Rigidbody player;
    NavMeshAgent AI_navmesh;
    private Animator animator;
    public Transform[] stops;
    private int stop;
    private bool playerFound = false;
    private bool alreadyFound = true;

    // Start is called before the first frame update
    void Start()
    {
        AI_navmesh = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();

        WalkPath();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        print("playerFound: " + playerFound);
        print("alreadyFound: " + alreadyFound);

        if (playerFound)
        {
            Chase();

            if (alreadyFound)
            {
                Invoke("stopChase", 3.0f);
            }
            else if (!alreadyFound)
            {
                alreadyFound = true;
            }
        }
        else if (isPlayerInSight() && rayCheck())
        {
            playerFound = true;
            Chase();
        }
        else
        {
            if (!AI_navmesh.pathPending && AI_navmesh.remainingDistance < 0.5f)
            {
                WalkPath();
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
            return true;
        }
        else
        {
            return false;
        }
    }

    bool rayCheck()
    {
        Vector3 direction = player.position - transform.position;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.collider.transform == player)
            {
                return true;
            }
        }
        
        return false;
    }

    void WalkPath()
    {
        AI_navmesh.speed = .75f;

        if (stops.Length == 0)
        {
            return;
        }

        AI_navmesh.destination = stops[stop].position;

        stop = (stop + 1) % stops.Length;
    }

    public void Chase()
    {
        AI_navmesh.speed = 1.5f;

        if (player != null)
        {
            Vector3 target = player.position;
            AI_navmesh.SetDestination(target);
        }
    }

    void UpdateAnimator()
    {
        animator.SetFloat("Speed", AI_navmesh.velocity.magnitude);

        if (playerFound && !alreadyFound)
        {
            animator.SetTrigger("playerIsSpotted");
        }
    }

    private void stopChase()
    {
        if (!isPlayerInSight() || !rayCheck())
        {
            playerFound = false;
            alreadyFound = false;
        }
    }
}