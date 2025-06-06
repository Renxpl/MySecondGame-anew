using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockbackState : IStateEnemy
{
    public void Enter(EnemyController ctrl)
    {

        var kbBehaviour = new EnemyKbBehaviour(ctrl, ctrl.GetComponent<Rigidbody2D>(), ctrl.PlayerTransform);
        kbBehaviour.Knockbacking();
    }


    public void Update(EnemyController ctrl)
    {
        //ctrl.AttackBehaviour.Attack(ctrl, ctrl.GetComponent<Rigidbody2D>(), ctrl.PlayerTransform);
    




    }


    public void Exit(EnemyController ctrl)
    {
        ctrl.UnlockEnemySprite();
    }

}
