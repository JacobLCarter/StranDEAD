using UnityEngine;
using GeneralStateMachine;

public class ThrowStateRock : State<Rock>
{
    private static ThrowStateRock _instance;
    private const float throwForce = 100f;

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
        rock.GetComponent<Rigidbody>().isKinematic = false;
        rock.transform.parent = null;
        rock.GetComponent<Rigidbody>().AddForce(rock.player.forward * throwForce + rock.player.up * throwForce);
        
        rock.stateMachine.switchState(GroundStateRock.Instance);
    }

    public override void ExitState(Rock rock)
    {
        
    }

    public override void UpdateState(Rock rock)
    {

    }
}