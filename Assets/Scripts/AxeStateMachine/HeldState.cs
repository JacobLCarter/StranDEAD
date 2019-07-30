using UnityEngine;
using GeneralStateMachine;
using UnityEngine.SocialPlatforms;

public class HeldState : State<Axe>
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
    
    public override void EnterState(Axe axe)
    {
        axe.GetComponent<Rigidbody>().isKinematic = true;
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Item"), LayerMask.NameToLayer("Player"));
        axe.transform.SetParent(axe.getHand());
        positionObject(axe);
    }

    public override void ExitState(Axe axe)
    {
        
    }

    public override void UpdateState(Axe axe)
    {
        if (Input.GetMouseButtonDown(0))
        {
            axe.stateMachine.switchState(UseState.Instance);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            axe.stateMachine.switchState(ThrowState.Instance);
        }
    }
    
    private void positionObject(Axe axe)
    {
        axe.setPosition(newPos, newRot, newScale);
    }
}