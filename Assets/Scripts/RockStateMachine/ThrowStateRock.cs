using UnityEngine;
using System.Collections;
using GeneralStateMachine;

public class ThrowStateRock : State<Rock>
{
    private static ThrowStateRock _instance;

    private MonoBehaviour newObject;

    private ThrowStateRock()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static ThrowStateRock Instance
    {
        get
        {
            if (_instance == null)
            {
                new ThrowStateRock();
            }

            return _instance;
        }
    }
    
    public override void EnterState(Rock rock)
    {
        rock.animator.SetTrigger("isThrowing");
        rock.gameObject.tag = "Rock";
        
        rock.stateMachine.switchState(GroundStateRock.Instance);

        StartCoroutine();
    }

    //REFERENCED: https://forum.unity.com/threads/waitforseconds-without-monobehaviour.216081/
    //Pauses for a few seconds before re-enabling camera follow which controls user movement
    public void StartCoroutine()
    {
        newObject = GameObject.FindObjectOfType<MonoBehaviour>();
        newObject.StartCoroutine(CoroutineTest());
    }

    private IEnumerator CoroutineTest()
    {
        yield return new WaitForSeconds(3.067f);
        GameObject.FindGameObjectWithTag("Playertag").GetComponent<CameraFollow>().enabled = true;
    }

    public override void ExitState(Rock rock)
    {
        
    }

    public override void UpdateState(Rock rock)
    {

    }
}