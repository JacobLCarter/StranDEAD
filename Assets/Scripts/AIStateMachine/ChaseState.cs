using UnityEngine;
using GeneralStateMachine;

public class ChaseState : State<Enemy>
{
    private static ChaseState _instance;

    private ChaseState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static ChaseState Instance
    {
        get
        {
            if (_instance == null)
            {
                new ChaseState();
            }

            return _instance;
        }
    }
    
    public override void EnterState(Enemy enemy)
    {
        enemy.currentStalk = enemy.stalkTime;
        enemy.navmesh.speed = 1.5f;
        enemy.navmesh.velocity = Vector3.zero;
        enemy.animator.SetTrigger("isRoaring");
    }

    public override void ExitState(Enemy enemy)
    {

    }

    public override void UpdateState(Enemy enemy)
    {
        Chase(enemy);
        
        enemy.isPlayerInSight();
    }

    private void Chase(Enemy enemy)
    {
        if (enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Roaring"))
        {
            enemy.navmesh.velocity = Vector3.zero;
        }
        else
        {
            if (enemy.player != null)
            {
                Vector3 target = enemy.player.position;
                enemy.navmesh.SetDestination(target);
            }
        }
    }
}