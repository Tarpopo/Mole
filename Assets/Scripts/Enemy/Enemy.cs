using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour,IDamagable,IHoleEntity
{
   [SerializeField] public float _startYPosition;
   [SerializeField] public float _endYPosition;
   [SerializeField] private float _grabDistance;
   [SerializeField] private Transform _foodTransform;
   public Quaternion StartRotation;
   public EnemyItem _hat, _glass;
   public int CurrentHealth;
   public int _enemyHealth=1;
   
   public AnimationClip GrubFood;
   public AnimationClip Search;
   public AnimationClip HandsUp;
   public AnimationClip Wait;
   public AnimationClip Stars;
   public AnimationClip DownDamage;

   public ParticleSystem _particleSystem;
   public Animator Anima;
   public Hole _occupiedHole;
   private Holes _holes;
   public FoodKit _foodKit;
   public Food _food;
   private StateMachine<Enemy> _stateMachine;
   public State<Enemy> _waitingState;
   public State<Enemy> _enemyMoving;
   public State<Enemy> _enemyTakeDamage;
   public State<Enemy> _enemyGetFood;

   public bool IsOutside { get; set;}
   //public bool IsOutSide;
   private void Start()
   {
      StartRotation = transform.rotation;
      CurrentHealth = _enemyHealth;
      Anima = GetComponent<Animator>();
      _stateMachine = new StateMachine<Enemy>();
      _waitingState = new EnemyWaiting(this,_stateMachine);
      _enemyMoving = new EnemyMoving(this,_stateMachine);
      _enemyTakeDamage = new EnemyTakeDamage(this, _stateMachine);
      _enemyGetFood = new EnemyGetFood(this, _stateMachine);
      _foodKit = FindObjectOfType<FoodKit>();
      _holes = FindObjectOfType<Holes>();
      _occupiedHole = _holes.GetFreeHole();
      //transform.SetParent(_occupiedHole);
      transform.position = new Vector3(_occupiedHole.GetHolePosition().x,_startYPosition,_occupiedHole.GetHolePosition().z);
      _stateMachine.Initialize(_waitingState);
      //StartCoroutine(MoveLocal(transform, _endPosition, 40,1, TryGetFood));
   }

   private void FixedUpdate()
   {
      _stateMachine.CurrentState.PhysicsUpdate();
   }

   private void Update()
   {
      _stateMachine.CurrentState.LogicUpdate();
   }

   public void TryGetFood()
   {
      var food=_foodKit.TryGetFood(transform.position, _grabDistance);
      if (food != null)
      {
         _food = food;
         _stateMachine.ChangeState(_enemyGetFood);
         //food.position = transform.position;
         //food.SetParent(transform);
         //food.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
      }
      else
      {
         Anima.Play(HandsUp.name);
      }
   }
   

   public void ChangeFoodPosition()
   {
      if (_food == null) return;
      var foodTransform = _food.GetTransform();
      if (foodTransform == null)
      {
         _stateMachine.ChangeState(_enemyMoving);
         Anima.Play(Wait.name);
         return;
      }
      foodTransform.position = _foodTransform.position;
      foodTransform.SetParent(_foodTransform);
      _stateMachine.ChangeState(_enemyMoving);
      Anima.Play(Wait.name);
   }

   public void StartMoving()
   {
        _stateMachine.ChangeState(_enemyMoving);
    }

   public void ChangeHole()
   {
      _holes.TryFreeHole(_occupiedHole);
      _occupiedHole = _holes.GetFreeHole();
      //transform.SetParent(_occupiedHole);
      transform.position = new Vector3(_occupiedHole.GetHolePosition().x,_startYPosition,_occupiedHole.GetHolePosition().z);
   }

   public void CheckFood()
   {
      if(_food!=null)
      {
         _food.GetTransform().SetParent(null);
         _food.ReturnFood();
         _food = null;
      }
      
   }

   public void MoveWhenTakeDamage()
   {
      StopAllCoroutines();
      StartCoroutine(MovePosition());
   }
   public IEnumerator MovePosition(int frames=4)
   {
      var position = transform.position;
      position.y = _startYPosition/2;
      var delta = (position - transform.position) / frames;
      for (int i = 0; i < frames; i++)
      {
         transform.position += delta;
         yield return new WaitForFixedUpdate();
      }

      position.y = _endYPosition/2;
      delta = (position - transform.position) / frames; 
      for (int i = 0; i < frames; i++)
      {
         transform.position += delta;
         yield return new WaitForFixedUpdate();
      }        
   }
   
   public void TakeDamage()
   {
      if (_stateMachine.CurrentState.IsStatePlay()) return;
      //Anima.Play(Wait.name);
      CheckFood();
      CurrentHealth--;
      Vibration.Vibrate(50);
      _particleSystem.gameObject.SetActive(true);
      _particleSystem.Play();

      if (CurrentHealth == 2 && _hat != null)
      {
            _hat.Drop();
      }
      else if (CurrentHealth == 1 && _glass != null)
      {
            _glass.Drop();
      }

      _stateMachine.ChangeState(_enemyTakeDamage);
   }

    public void RefreshItems()
    {
        _hat?.Refresh();
        _glass?.Refresh();
    }
}
