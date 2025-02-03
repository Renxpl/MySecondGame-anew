using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyMain : MonoBehaviour
{


    Vector2 firstPosition;
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    protected virtual void Patrolling()
    {

    }


    protected abstract void AttackMode();


}
