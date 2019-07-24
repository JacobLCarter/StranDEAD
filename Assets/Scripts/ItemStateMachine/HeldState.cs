using UnityEngine;
using GeneralStateMachine;
using UnityEngine.SocialPlatforms;

public class HeldState : State<Item>
{
    private static HeldState _instance;
    private Vector3 newPos = new Vector3(-0.054f, -0.136f, -0.029f);
    private Vector3 newRot = new Vector3(201.038f, -207.332f, 162.065f);
    private Vector3 newScale = Vector3.one;

    private HeldState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static HeldState Instance
    {
        get
        {
            if (_instance == null)
            {
                new HeldState();
            }

            return _instance;
        }
    }
    
    public override void EnterState(Item item)
    {
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.transform.SetParent(item.getHand());
        positionObject(item);
    }

    public override void ExitState(Item item)
    {
        
    }

    public override void UpdateState(Item item)
    {
        if (Input.GetMouseButtonDown(0))
        {
            item.stateMachine.switchState(UseState.Instance);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            item.stateMachine.switchState(ThrowState.Instance);
        }
    }
    
    private void positionObject(Item item)
    {
        item.setPosition(newPos, newRot, newScale);
    }
}