using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyMain : MonoBehaviour
{
    [SerializeField] GameObject getDmgRb;

    protected Vector2 firstPosition;
    protected virtual void Start()
    {
        GameEvents.gameEvents.onGettingDmg += TakingDamage;
    }

    protected virtual void Update()
    {
        
    }
    protected virtual void Patrolling()
    {

    }


    protected abstract void AttackMode();

    protected virtual void TakingDamage(GameObject receiver, GameObject sender, Collider2D otherCollider, int attakVer)
    {
        if(receiver == gameObject)
        {
            Debug.Log("GettingDmg");


        }
    }

}
