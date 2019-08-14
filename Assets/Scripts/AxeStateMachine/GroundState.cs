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

    }
}
