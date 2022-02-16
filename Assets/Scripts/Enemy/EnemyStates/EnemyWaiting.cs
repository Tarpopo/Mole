using UnityEngine;

public class EnemyWaiting : State<Enemy>
{
    private Timer _timer;
    private float _time;
    private bool _isUp;
    public EnemyWaiting(Enemy character, StateMachine<Enemy> stateMachine) : base(character, stateMachine)
    {
        _timer = new Timer();
        _time = Random.Range(1, 2.1f);
    }
    
    public override void Enter()
    {
        _timer.StartTimer(_time,()=>
        {
            stateMachine.ChangeState(character._enemyMoving);
            _isUp = !_isUp;

            character.RefreshItems();
        });
    }

    public override bool IsStatePlay()
    {
        return _isUp == false;
    }

    public override void HandleInput()
    {

    }

    public override void LogicUpdate()
    {
        _timer.UpdateTimer();
    }
    
    public override void PhysicsUpdate()
    {

    }

    public override void Exit()
    {

    }
}
