using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyStats Stats{get; private set;}
    public AttackCombo Combo { get; private set;}
    IMovementBehaviour chaseMov;

    public void Init(EnemyStats stats,IMovementBehaviour mov)
    {
        Stats = stats;
        chaseMov= mov;


    }

    void Update()
    {
        var player = GameObject.FindWithTag("Player").transform;
        chaseMov.Move(this,GetComponent<Rigidbody2D>(),player);

    }

}
