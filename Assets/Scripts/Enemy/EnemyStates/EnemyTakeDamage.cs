using UnityEngine;

public class EnemyTakeDamage : State<Enemy>
{
    private Transform _enemyTransform;
    private readonly ColorChanger _ColorChanger;
    private Material _material;
    private Timer _timer;
    private bool _isFatalDamage;

    public EnemyTakeDamage(Enemy character, StateMachine<Enemy> stateMachine) : base(character, stateMachine)
    {
        _timer = new Timer();
        _enemyTransform = character.transform;
        _ColorChanger = new ColorChanger();
        _material = character.GetComponentInChildren<MeshRenderer>().material;
    }

    public override bool IsStatePlay()
    {
        return _isFatalDamage;
    }

    public override void Enter()
    {
        if (character.CurrentHealth<=0)
        {
            //character.CheckFood();
            _isFatalDamage = true;
            character.Anima.Play(character.DownDamage.name);
            _timer.StartTimer(0.4f, () =>
            {
                character.CurrentHealth = character._enemyHealth;
                stateMachine.ChangeState(character._enemyMoving);
                character._particleSystem.Stop();
                character._particleSystem.gameObject.SetActive(false);
                _isFatalDamage = false;
            });

            //_timer.StartTimer(0,null);
        }
        else
        {
            character.MoveWhenTakeDamage();
            Debug.Log("Stars");
            character.Anima.Play(character.Stars.name);
            _timer.StartTimer(1.5f, () =>
            {
                character._particleSystem.Stop();
                character._particleSystem.gameObject.SetActive(false);
                character.Anima.Play(character.Search.name);
                //character.CheckFood();

            });
            //_ColorChanger.StartChangeColor(_material, Color.red, 6, null, true);
        }
    }
    
    public override void PhysicsUpdate()
    {
        _ColorChanger.ChangeColorUpdate();
        _timer.UpdateTimer();
    }
    
}
