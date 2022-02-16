public abstract class State<T>
{
    protected readonly T character;
    protected readonly StateMachine<T> stateMachine;
    protected State(T character, StateMachine<T> stateMachine)
    {
        this.character = character;
        this.stateMachine = stateMachine;
    }

    public virtual bool IsStatePlay()
    {
        return false;
    }

    public virtual void Enter()
    {
    }
    public virtual void HandleInput()
    {
    }
    public virtual void LogicUpdate()
    {
    }
    public virtual void PhysicsUpdate()
    {
    }
    public virtual void Exit()
    {
    }
}