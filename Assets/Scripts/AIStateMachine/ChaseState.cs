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
        enemy.navmesh.speed = 1.5f;
        enemy.currentStalk = enemy.stalkTime;
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
        if (enemy.player != null)
        {
            Vector3 target = enemy.player.position;
            enemy.navmesh.SetDestination(target);
        }
    }
}