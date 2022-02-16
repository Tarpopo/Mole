using UnityEngine;

public class EnemyMoving : State<Enemy>
{
    private LocalPositionMover _localPositionMover;
    private LocalScaleChanger _scaleWhenReturn;
    private LocalScaleChanger _localScaleChanger;
    private LocalScaleChanger _holeScaleChanger;
    private bool _isMovingDown;
    public EnemyMoving(Enemy character, StateMachine<Enemy> stateMachine) : base(character, stateMachine)
    { 
        _localPositionMover = new LocalPositionMover();
        _localScaleChanger = new LocalScaleChanger();
        //_holeScaleChanger = new LocalScaleChanger();
        _scaleWhenReturn = new LocalScaleChanger();
    }

    public override void Enter()
    {
        var position = new Vector3(character.transform.position.x,
            _isMovingDown ? character._startYPosition : character._endYPosition, character.transform.position.z);
        _localPositionMover.StartMoving(character.transform, position,15,OnEndMoving);
        _localScaleChanger.StartScaling(character.transform, Vector3.one,7,true);
        _scaleWhenReturn.StartScaling(character.transform,Vector3.zero,20);
        //_holeScaleChanger.StartScaling(character._occupiedHole,Vector3.one*1.5f,7,true);
        character._occupiedHole.StartScale(!_isMovingDown);
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
            //_holeScaleChanger.UpdateScale();
        }
        else
        {
            _scaleWhenReturn.UpdateScale();
           // _holeScaleChanger.UpdateScale();
        }
        _localPositionMover.MovePosition();
    }

    public void OnEndMoving()
    {
        if (_isMovingDown)
        {
            character.IsOutside = true;
            character.Anima.Play(character.Search.name);
            //character.TryGetFood();
        }
        else
        {
            character.IsOutside = false;
            character.ChangeHole();
            character.transform.localScale = Vector3.one*0.6f;
            if (character._food)
            {
                character._food.DeleteFood();
                character._food = null;
            }

            //if (character._items.Length>0)
            //{
            //    foreach (var item in character._items)
            //    {
            //        item.SetActive(true);
            //    }
            //}
            //character._particleSystem.gameObject.SetActive(false);
            character.CurrentHealth = character._enemyHealth;
            character.Anima.Play(character.Wait.name);
            //character._hat?.SetActive(true);
            stateMachine.ChangeState(character._waitingState);
            character.transform.rotation = character.StartRotation;
        }
        
    }
}
