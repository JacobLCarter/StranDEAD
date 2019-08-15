using System.Runtime.InteropServices;
using UnityEngine;
using GeneralStateMachine;

public class GroundStateRock : State<Rock>
{
    private static GroundStateRock _instance;
    private const float pickupDistance = 0.5f;

    private GroundStateRock()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static GroundStateRock Instance
    {
        get
        {
            if (_instance == null)
            {
                new GroundStateRock();
            }

            return _instance;
        }
    }
    
    public override void EnterState(Rock rock)
    {
        
    }

    public override void ExitState(Rock rock)
    {
        //rock.HUD.PickupTextOff();
    }

    public override void UpdateState(Rock rock)
    {
        if (isObjectReachable(rock) && Input.GetKey(KeyCode.E))
        {
            rock.stateMachine.switchState(HeldStateRock.Instance);
        }
    }
    
    public bool isObjectReachable(Rock rock)
    {
        Vector3 target = rock.player.position - rock.transform.position;
        float angle = Vector3.Angle(target, rock.transform.position);

        if (angle < 90.0f && Vector3.Distance(rock.transform.position, rock.player.position) <= pickupDistance)
        {
            //rock.HUD.PickupTextOn("");
            return true;
        }
        
        //rock.HUD.PickupTextOff();
        return false;
    }
}