using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovState : IStateEnemy
{
    public void Enter(EnemyController ctrl)
    {

    }


    public void Update(EnemyController ctrl)
    {
        ctrl.ChaseMov.Move(ctrl, ctrl.GetComponent<Rigidbody2D>(), ctrl.PlayerTransform);

        if (ctrl.CurrentStance <= 0)
        {
            ctrl.ChangeState(new EnemyKnockbackState());
        }

    }


    public void Exit(EnemyController ctrl)
    {

    }
}
