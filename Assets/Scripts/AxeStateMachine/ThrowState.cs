using UnityEngine;
using System.Collections;
 using GeneralStateMachine;
 
 public class ThrowState : State<Axe>
 {
     private static ThrowState _instance;

     private MonoBehaviour newObject;

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
     
     public override void EnterState(Axe axe)
     {
         axe.animator.SetTrigger("isThrowing");

         axe.stateMachine.switchState(GroundState.Instance);

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
    public override void ExitState(Axe axe)
     {
         
     }
 
     public override void UpdateState(Axe axe)
     {
         
     }
 }