using UnityEngine;
using GeneralStateMachine;

public class DistractedState : State<Enemy>
{
    private static DistractedState _instance;

    private DistractedState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static DistractedState Instance
    {
        get
        {
            if (_instance == null)
            {
                new DistractedState();
            }

            return _instance;
        }
    }
    
    public override void EnterState(Enemy enemy)
    {
        enemy.navmesh.speed = 1.5f;
        enemy.navmesh.destination = enemy.getCurrentRock().transform.position;
    }

    public override void ExitState(Enemy enemy)
    {

    }

    public override void UpdateState(Enemy enemy)
    {
        
        if (enemy.isPlayerInSight())
        {
            if (Vector3.Distance(enemy.transform.position, enemy.player.position) < enemy.getAttackRange())
            {
                enemy.stateMachine.switchState(AttackState.Instance);
            }
            else
            {
                enemy.stateMachine.switchState(ChaseState.Instance);
            }
        }
        else if (enemy.navmesh.remainingDistance < 0.2f)
        {
            enemy.stateMachine.switchState(StalkState.Instance);
        }
    }
}