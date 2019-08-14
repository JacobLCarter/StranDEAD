using UnityEngine;
using GeneralStateMachine;

public class DeathState : State<Enemy>
{
    private static DeathState _instance;

    private DeathState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static DeathState Instance
    {
        get
        {
            if (_instance == null)
            {
                new DeathState();
            }

            return _instance;
        }
    }
    
    public override void EnterState(Enemy enemy)
    {
        enemy.animator.SetTrigger("isDead");
        enemy.navmesh.velocity = Vector3.zero;
        enemy.GetComponent<Rigidbody>().freezeRotation = false;
        enemy.GetComponent<CapsuleCollider>().direction = 2;
    }

    public override void ExitState(Enemy enemy)
    {
        
    }

    public override void UpdateState(Enemy enemy)
    {
        
    }
}