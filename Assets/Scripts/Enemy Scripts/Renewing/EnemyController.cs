using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyStats Stats{get; private set;}
    public AttackCombo Combo { get; private set;}
    public IMovementBehaviour ChaseMov { get; private set; }
    public IAttackBehaviour AttackBehaviour { get; private set; }
    public Transform PlayerTransform { get; private set; }
    IStateEnemy currentState;

    //method which allows child states to run couroutines
    public Coroutine Run(IEnumerator routine) => StartCoroutine(routine);


    public void Init(EnemyStats stats,IMovementBehaviour mov, AttackCombo combo, IAttackBehaviour attack)
    {
        Stats = stats;
        ChaseMov= mov;
        Combo = combo;
        AttackBehaviour = attack;
    }

    void Start()
    {
        PlayerTransform = GameObject.FindWithTag("Player").transform;
        ChangeState(new EnemyMovState());
        
    }


    void Update()
    {
       
        currentState.Update(this);

    }

    public void ChangeState(IStateEnemy newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);


    }

}
