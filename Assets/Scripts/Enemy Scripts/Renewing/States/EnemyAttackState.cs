using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IStateEnemy
{
    public void Enter(EnemyController ctrl)
    {
        ctrl.ResetAttackStep();
        ctrl.AttackBehaviour.Attack(ctrl, ctrl.GetComponent<Rigidbody2D>(), ctrl.PlayerTransform);
       
    }


    public void Update(EnemyController ctrl)
    {
        //ctrl.AttackBehaviour.Attack(ctrl, ctrl.GetComponent<Rigidbody2D>(), ctrl.PlayerTransform);

        if(ctrl.CurrentStance <= 0)
        {
            ctrl.Combo.steps[ctrl.AttackStep % 3].hitbox.enabled = false;
            ctrl.UnlockEnemySprite();
            ctrl.ChangeState(new EnemyKnockbackState());
        }




    }


    public void Exit(EnemyController ctrl)
    {
        ctrl.ResetAttackStep();
    }

    


}
