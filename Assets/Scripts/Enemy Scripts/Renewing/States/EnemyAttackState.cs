using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IStateEnemy
{
    public void Enter(EnemyController ctrl)
    {
        ctrl.ResetAttackStep();
        
       
    }


    public void Update(EnemyController ctrl)
    {
        ctrl.AttackBehaviour.Attack(ctrl, ctrl.GetComponent<Rigidbody2D>(), ctrl.PlayerTransform);
        //ctrl.AttackBehaviour.Attack(ctrl, ctrl.GetComponent<Rigidbody2D>(), ctrl.PlayerTransform);






    }


    public void Exit(EnemyController ctrl)
    {
        ctrl.ResetAttackStep();
    }

    


}
