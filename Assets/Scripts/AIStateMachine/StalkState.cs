using UnityEngine;
using GeneralStateMachine;

public class StalkState : State<Enemy>
{
    private static StalkState _instance;
    private Vector3 target;

    private StalkState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static StalkState Instance
    {
        get
        {
            if (_instance == null)
            {
                new StalkState();
            }

            return _instance;
        }
    }
    
    public override void EnterState(Enemy enemy)
    {
        enemy.setCurrentStalk(10f);
        target = enemy.player.position;
        enemy.navmesh.SetDestination(target);
    }

    public override void ExitState(Enemy enemy)
    {
        
    }

    public override void UpdateState(Enemy enemy)
    {
        if (enemy.isPlayerInSight())
        {
            enemy.stateMachine.switchState(ChaseState.Instance);
        }
        else if (enemy.isPlayerAudible())
        {
            target = enemy.player.position;
            enemy.navmesh.SetDestination(target);
        }
        else if (enemy.heardNoise())
        {
            enemy.stateMachine.switchState(DistractedState.Instance);
        }
        else if (enemy.getStalkTime() < 0)
        {
            enemy.stateMachine.switchState(PathState.Instance);
        }
        else
        {
            float temp = enemy.getStalkTime();
            temp -= Time.deltaTime;
            
            enemy.setCurrentStalk(temp);
        }
    }
}