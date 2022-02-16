using System;
using System.Linq;
using Interfaces;
using UnityEngine;

public class Mine : MonoBehaviour,IDamagable
{
    [SerializeField] public float _startYPosition;
    [SerializeField] public float _endYPosition;
    [SerializeField] private float _explotionRadius;
    [SerializeField] private ParticleSystem _explotion;
    private FoodKit _foodKit;
    public Hole _occupiedHole;
    private StateMachine<Mine> _stateMachine;
    public State<Mine> _waitingState;
    public State<Mine> _moving;
    private Holes _holes;
    
    
    private void Start()
    {
        _holes = FindObjectOfType<Holes>();
        _occupiedHole = _holes.GetFreeHole();
        _foodKit = FindObjectOfType<FoodKit>();
        transform.position = new Vector3(_occupiedHole.GetHolePosition().x,_startYPosition,_occupiedHole.GetHolePosition().z);
        _stateMachine = new StateMachine<Mine>();
        _waitingState = new MineWaiting(this,_stateMachine);
        _moving = new MineMoving(this, _stateMachine);
        _stateMachine.Initialize(_waitingState);
    }

    private void Update()
    {
        _stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        _stateMachine.CurrentState.PhysicsUpdate();
    }

    public void ChangeHole()
    {
        _holes.TryFreeHole(_occupiedHole);
        _occupiedHole = _holes.GetFreeHole();
        //transform.SetParent(_occupiedHole);
        transform.position = new Vector3(_occupiedHole.GetHolePosition().x,_startYPosition,_occupiedHole.GetHolePosition().z);
    }

    public void TakeDamage()
    {
        _explotion.Play();
        Vibration.Vibrate(100);
        _explotion.transform.SetParent(null);
        _explotion.transform.position = transform.position;
        var damagables = _foodKit.GetFoodInDistance(transform.position, _explotionRadius);
        if (damagables == null) return;
        foreach (var food in damagables)
        {
            food.TakeDamage();
        }
        _stateMachine.ChangeState(_moving);
    }
}
