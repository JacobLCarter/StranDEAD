using System.Runtime.InteropServices;
using UnityEngine;
using GeneralStateMachine;

public class GroundState : State<Item>
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
    
    public override void EnterState(Item item)
    {
        
    }

    public override void ExitState(Item item)
    {
        
    }

    public override void UpdateState(Item item)
    {
        if (isObjectReachable(item) && Input.GetKey(KeyCode.E))
        {
            item.stateMachine.switchState(HeldState.Instance);
        }
    }
    
    public bool isObjectReachable(Item item)
    {
        Vector3 target = item.player.position - item.transform.position;
        float angle = Vector3.Angle(target, item.transform.position);

        if (angle < 90.0f && Vector3.Distance(item.transform.position, item.player.position) <= pickupDistance)
        {
            Debug.Log("true");
            return true;
        }
        
        Debug.Log("false");
        return false;
    }
}
