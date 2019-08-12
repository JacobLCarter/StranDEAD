using UnityEngine;
using GeneralStateMachine;

public class PathState : State<Enemy>
{
    private static PathState _instance;

    private PathState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static PathState Instance
    {
        get
        {
            if (_instance == null)
            {
                new PathState();
            }

            return _instance;
        }
    }
    
    public override void EnterState(Enemy enemy)
    {
        enemy.navmesh.speed = .375f;
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
            enemy.stateMachine.switchState(StalkState.Instance);
        }
        else if (enemy.heardNoise())
        {
            enemy.stateMachine.switchState(DistractedState.Instance);
        }
        else if (!enemy.navmesh.pathPending && enemy.navmesh.remainingDistance < 0.2f)
        {
            WalkPath(enemy);
        }
    }

    private void WalkPath(Enemy enemy)
    {
        if (enemy.stops.Length == 0)
        {
            return;
        }

        enemy.navmesh.destination = enemy.stops[enemy.stop].position;

        enemy.stop = (enemy.stop + 1) % enemy.stops.Length;
    }
}
