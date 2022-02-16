using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetFood : State<Enemy>
{
    private Transform _transform;
    private Transform _foodTransform;
    private Quaternion _targetRotation;
    public EnemyGetFood(Enemy character, StateMachine<Enemy> stateMachine) : base(character, stateMachine)
    {
        _transform = character.transform;
    }

    

    public override void Enter()
    {
        //character._food = character._foodKit.TryGetFood();
        if (character._food == null)
        {
            stateMachine.ChangeState(character._enemyMoving);
            return;
        }
        _foodTransform = character._food.GetTransform();
        character.Anima.Play(character.GrubFood.name);
        _targetRotation = Quaternion.LookRotation(_foodTransform.position - _transform.position);
    }

    public override void Exit()
    {
        
    }

    public override void PhysicsUpdate()
    {
        _transform.rotation = Quaternion.Slerp(_transform.rotation, _targetRotation, 0.2f);
        if (_transform.rotation == _targetRotation)
        {
            character.Anima.Play(character.GrubFood.name);
        }
    }
}
