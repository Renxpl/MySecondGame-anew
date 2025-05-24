using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateEnemy 
{
    void Enter(EnemyController ctrl);

    void Update(EnemyController ctrl);

    void Exit(EnemyController ctrl);

}
