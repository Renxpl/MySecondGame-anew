using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementBehaviour 
{

    void Move(EnemyController self, Rigidbody2D enemyRB, Transform target);


}
