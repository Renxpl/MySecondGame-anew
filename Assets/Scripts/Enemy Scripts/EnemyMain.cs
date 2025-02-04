using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyMain : MonoBehaviour
{


    protected Vector2 firstPosition;
    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }
    protected virtual void Patrolling()
    {

    }


    protected abstract void AttackMode();

    protected virtual void TakingDamage()
    {

    }

}
