using System.Runtime.InteropServices;
using UnityEngine;
using GeneralStateMachine;

public class GroundState : State<Axe>
{
    private static GroundState _instance;
    private const float pickupDistance = 0.5f;

    private GroundState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static GroundState Instance
    {
        get
        {
            if (_instance == null)
            {
                new GroundState();
            }

            return _instance;
        }
    }
    
    public override void EnterState(Axe axe)
    {
        
    }

    public override void ExitState(Axe axe)
    {
        
    }

    public override void UpdateState(Axe axe)
    {
        if (isObjectReachable(axe) && Input.GetKey(KeyCode.E))
        {
            axe.stateMachine.switchState(HeldState.Instance);
        }
    }
    
    public bool isObjectReachable(Axe axe)
    {
        Vector3 target = axe.player.position - axe.transform.position;
        float angle = Vector3.Angle(target, axe.transform.position);

        if (angle < 90.0f && Vector3.Distance(axe.transform.position, axe.player.position) <= pickupDistance)
        {
            return true;
        }
        
        return false;
    }
}
