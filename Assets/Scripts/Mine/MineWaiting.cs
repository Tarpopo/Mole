using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineWaiting : State<Mine>
{
    private Timer _timer;
    private float _time;
    public MineWaiting(Mine character, StateMachine<Mine> stateMachine) : base(character, stateMachine)
    {
        _timer = new Timer();
        _time = Random.Range(3, 5.5f);
    }
    public override void Enter()
    {
        _timer.StartTimer(_time,()=>
        {
            stateMachine.ChangeState(character._moving);
        });
    }

    public override void HandleInput()
    {

    }

    public override void LogicUpdate()
    {
        _timer.UpdateTimer();
    }
    
}
