using UnityEngine;
using GeneralStateMachine;

public class UseState : State<Item>
{
    private static UseState _instance;
    private const float throwForce = 200f;

    private UseState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static UseState Instance
    {
        get
        {
            if (_instance == null)
            {
                new UseState();
            }

            return _instance;
        }
    }
    
    public override void EnterState(Item item)
    {
        item.animator.SetTrigger("isUsing");
        item.stateMachine.switchState(HeldState.Instance);
    }

    public override void ExitState(Item item)
    {
        
    }

    public override void UpdateState(Item item)
    {
        
    }
}