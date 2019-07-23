using UnityEngine;
using GeneralStateMachine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AttackState : State<Enemy>
{
    private static AttackState _instance;
    private Vector3 animationOffset = new Vector3(.3f,0,0);

    private AttackState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static AttackState Instance
    {
        get
        {
            if (_instance == null)
            {
                new AttackState();
            }

            return _instance;
        }
    }
    
    public override void EnterState(Enemy enemy)
    {
        enemy.navmesh.velocity = Vector3.zero;
    }

    public override void ExitState(Enemy enemy)
    {

    }

    public override void UpdateState(Enemy enemy)
    {
        if (enemy.isPlayerInSight())
        {
            enemy.transform.LookAt(enemy.player.position + animationOffset);
            enemy.navmesh.velocity = Vector3.zero;
            enemy.animator.SetTrigger("isAttacking");
        }
        else
        {
            enemy.stateMachine.switchState(StalkState.Instance);
        }
    }
}