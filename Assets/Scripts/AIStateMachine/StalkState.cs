using UnityEngine;
using GeneralStateMachine;

public class StalkState : State<Enemy>
{
    private static StalkState _instance;

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

    }

    public override void ExitState(Enemy enemy)
    {

    }

    public override void UpdateState(Enemy enemy)
    {
        if (enemy.currentStalk < 0)
        {
            enemy.isPlayerInSight();
        }
        else
        {
            enemy.currentStalk -= Time.deltaTime;
        }
    }
}