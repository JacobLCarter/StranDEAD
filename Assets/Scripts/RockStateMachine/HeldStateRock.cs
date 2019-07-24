using UnityEngine;
using GeneralStateMachine;
using UnityEngine.SocialPlatforms;

public class HeldStateRock : State<Rock>
{
    private static HeldStateRock _instance;
    private Vector3 newPos = new Vector3(-0.054f, -0.136f, -0.029f);
    private Vector3 newRot = new Vector3(201.038f, -207.332f, 162.065f);
    private Vector3 newScale = Vector3.one;

    private HeldStateRock()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static HeldStateRock Instance
    {
        get
        {
            if (_instance == null)
            {
                new HeldStateRock();
            }

            return _instance;
        }
    }
    
    public override void EnterState(Rock rock)
    {
        rock.GetComponent<Rigidbody>().isKinematic = true;
        rock.transform.SetParent(rock.getHand());
        positionObject(rock);
    }

    public override void ExitState(Rock rock)
    {
        
    }

    public override void UpdateState(Rock rock)
    {
        if (Input.GetMouseButtonDown(0))
        {
            rock.stateMachine.switchState(ThrowStateRock.Instance);
        }
    }
    
    private void positionObject(Rock rock)
    {
        rock.setPosition(newPos, newRot, newScale);
    }
}