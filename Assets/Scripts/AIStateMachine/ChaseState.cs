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
        enemy.navmesh.speed = 1f;
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
                Chase(enemy);
            }
        }
        else
        {
            enemy.stateMachine.switchState(StalkState.Instance);
        }
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