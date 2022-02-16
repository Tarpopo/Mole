public class StateMachine<T>
{ 
    public State<T> CurrentState { get; private set; }
    public void Initialize(State<T> startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }
    public void ChangeState(State<T> newState)
    {
        CurrentState.Exit();

        CurrentState = newState;
        newState.Enter();
    }
}
