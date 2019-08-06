using UnityEngine;
 using GeneralStateMachine;
 
 public class ThrowState : State<Axe>
 {
     private static ThrowState _instance;

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
     }
 
     public override void ExitState(Axe axe)
     {
         
     }
 
     public override void UpdateState(Axe axe)
     {
         
     }
 }