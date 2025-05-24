using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseMovementBehaviour : IMovementBehaviour
{


    public void Move(EnemyController self, Rigidbody2D enemyRB, Transform target)
    {

        Vector2 dir = new Vector2(target.position.x - self.transform.position.x, 0).normalized;
        enemyRB.velocity = self.Stats.moveSpeed * dir;



    }



}
