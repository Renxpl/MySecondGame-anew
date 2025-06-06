using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    public EnemyStats Stats{get; private set;}
    public AttackCombo Combo { get; private set;}
    public IMovementBehaviour ChaseMov { get; private set; }
    public IAttackBehaviour AttackBehaviour { get; private set; }
    public Transform PlayerTransform { get; private set; }
    IStateEnemy currentState;

    public int AttackStep { get; private set; }

    public bool IsLockedEnemySprite { get; private set; }

    public float CurrentHealth { get; private set; }
    //method which allows child states to run couroutines
    public Coroutine Run(IEnumerator routine) => StartCoroutine(routine);


    public void Init(EnemyStats stats,IMovementBehaviour mov, AttackCombo combo, IAttackBehaviour attack, float HP)
    {
        Stats = stats;
        ChaseMov= mov;
        Combo = combo;
        AttackBehaviour = attack;
        AttackStep = 0;
        CurrentHealth = HP;
    }

    void Start()
    {
        PlayerTransform = GameObject.FindWithTag("Player").transform;
        ChangeState(new EnemyMovState());
        
    }


    void Update()
    {

        if (!IsLockedEnemySprite)
        {

            Vector2 dir = new Vector2(PlayerTransform.position.x - transform.position.x, 0).normalized;
            transform.localScale = new Vector2(dir.x, 1f);//Sprite Adjustment


        }

        currentState.Update(this);

    }

    public void ChangeState(IStateEnemy newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);


    }

    public void IncreaseAttackStep()
    {
        AttackStep++;


    }

    public void ResetAttackStep()
    {
        AttackStep= 0;

    }


    public void LockEnemySprite()
    {

        IsLockedEnemySprite = true;


    }
    public void UnlockEnemySprite()
    {

        IsLockedEnemySprite = false;


    }

    public void TakeDamage(float dmg)
    {



    }


}
