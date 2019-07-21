namespace GeneralStateMachine
{
    public class StateMachine<Object>
    {
        public State<Object> currentState { get; private set; }
        public Object CurrentObject;

        public StateMachine(Object currentObject)
        {
            CurrentObject = currentObject;
            currentState = null;////////////////////////////////////////////////////////////////////////
        }

        public void switchState(State<Object> nextState)
        {
            currentState?.ExitState(CurrentObject);
            currentState = nextState;
            currentState.EnterState(CurrentObject);
        }

        public void Update()
        {
            currentState?.UpdateState(CurrentObject);
        }
    }

    public abstract class State<Object>
    {
        //executes only once upon entering a new state
        public abstract void EnterState(Object currentObject);
        //executes only once upon entering a new state
        public abstract void ExitState(Object currentObject);
        public abstract void UpdateState(Object currentObject);
    }
}