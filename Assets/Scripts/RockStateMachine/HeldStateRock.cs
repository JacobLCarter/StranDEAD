using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using GeneralStateMachine;
using UnityEngine.SocialPlatforms;

public class HeldStateRock : State<Rock>
{
    private static HeldStateRock _instance;
    private Vector3 newPos = new Vector3(0.0254f, 0.017f, -0.024f);
    private Vector3 newRot = new Vector3(49.253f, 184.153f, -67.47601f);
    private Vector3 newScale = new Vector3(0.1f, 0.1f, 0.1f);

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
        rock.gameObject.tag = "HeldItem";
        rock.animator.SetTrigger("heldItem");
        rock.GetComponent<Rigidbody>().isKinematic = true;
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Item"), LayerMask.NameToLayer("Player"));
    }

    public override void ExitState(Rock rock)
    {
        rock.gameObject.tag = "Rock";
    }

    public override void UpdateState(Rock rock)
    {
        if (Input.GetMouseButtonDown(1))
        {
            rock.stateMachine.switchState(ThrowStateRock.Instance);
        }
    }
    
    private void positionObject(Rock rock)
    {
        rock.setPosition(newPos, newRot, newScale);
    }
}