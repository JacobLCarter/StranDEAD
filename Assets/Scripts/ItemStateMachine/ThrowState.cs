using UnityEngine;
using GeneralStateMachine;

public class ThrowState : State<Item>
{
    private static ThrowState _instance;
    private const float throwForce = 200f;

    private ThrowState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static ThrowState Instance
    {
        get
        {
            if (_instance == null)
            {
                new ThrowState();
            }

            return _instance;
        }
    }
    
    public override void EnterState(Item item)
    {
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.transform.parent = null;
        item.GetComponent<Rigidbody>().AddForce(item.player.forward * throwForce);
        
        item.stateMachine.switchState(GroundState.Instance);
    }

    public override void ExitState(Item item)
    {
        
    }

    public override void UpdateState(Item item)
    {
        
    }
}