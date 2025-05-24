using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackBehaviour 
{

    void Attack(EnemyController self, Rigidbody2D enemyRB, Transform target);

}