using UnityEngine;
using GeneralStateMachine;

public class AttackState : State<Enemy>
{
    private static AttackState _instance;

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
        enemy.navmesh.enabled = false;
    }

    public override void ExitState(Enemy enemy)
    {
        enemy.navmesh.enabled = true;
    }

    public override void UpdateState(Enemy enemy)
    {
        enemy.transform.LookAt(enemy.player.position + new Vector3(.3f,0,0));
        enemy.animator.SetTrigger("isAttacking");
    }
}