using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMoving : State<Mine>
{
    private LocalPositionMover _localPositionMover;
    private LocalScaleChanger _scaleWhenReturn;
    private LocalScaleChanger _localScaleChanger;
    private LocalScaleChanger _holeScaleChanger;
    private bool _isMovingDown;
    public MineMoving(Mine character, StateMachine<Mine> stateMachine) : base(character, stateMachine)
    {
        _localPositionMover = new LocalPositionMover();
        _localScaleChanger = new LocalScaleChanger();
        _holeScaleChanger = new LocalScaleChanger();
        _scaleWhenReturn = new LocalScaleChanger();
    }
    public override void Enter()
    {
        var position = new Vector3(character.transform.position.x,
            _isMovingDown ? character._startYPosition : character._endYPosition, character.transform.position.z);
        _localPositionMover.StartMoving(character.transform, position,15,OnEndMoving);
        _localScaleChanger.StartScaling(character.transform, Vector3.one*2f,7,true);
        _scaleWhenReturn.StartScaling(character.transform,Vector3.zero,20);
        character._occupiedHole.StartScale(!_isMovingDown);
        //_holeScaleChanger.StartScaling(character._occupiedHole,Vector3.one*1.5f,7,true);
        _isMovingDown = !_isMovingDown;
    }

    public override bool IsStatePlay()
    {
        return _localPositionMover.IsMoving;
    }

    public override void PhysicsUpdate()
    {
        if (_isMovingDown)
        {
            _localScaleChanger.UpdateScale();
            _holeScaleChanger.UpdateScale();
        }
        else
        {
            _scaleWhenReturn.UpdateScale();
            _holeScaleChanger.UpdateScale();
        }
        _localPositionMover.MovePosition();
    }

    private void OnEndMoving()
    {
        if(_isMovingDown==false)
        {
            character.ChangeHole();
            character.transform.localScale = Vector3.one;
        }
        stateMachine.ChangeState(character._waitingState);
        
    }
}
