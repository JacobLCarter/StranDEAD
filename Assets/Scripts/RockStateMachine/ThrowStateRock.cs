using UnityEngine;
using GeneralStateMachine;

public class ThrowStateRock : State<Rock>
{
    private static ThrowStateRock _instance;

    private ThrowStateRock()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static ThrowStateRock Instance
    {
        get
        {
            if (_instance == null)
            {
                new ThrowStateRock();
            }

            return _instance;
        }
    }
    
    public override void EnterState(Rock rock)
    {
        rock.animator.SetTrigger("isThrowing");
        
        rock.stateMachine.switchState(GroundStateRock.Instance);
    }

    public override void ExitState(Rock rock)
    {
        
    }

    public override void UpdateState(Rock rock)
    {

    }
}