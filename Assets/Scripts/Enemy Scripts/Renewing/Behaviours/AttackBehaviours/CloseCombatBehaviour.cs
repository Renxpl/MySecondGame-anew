using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCombatBehaviour : IAttackBehaviour
{
    
    public void Attack(EnemyController self, Rigidbody2D enemyRB, Transform target)
    {
        self.Run(PerformAttack(self,enemyRB,target));
       


        



    }


    IEnumerator PerformAttack(EnemyController self, Rigidbody2D enemyRB, Transform target)
    {

        
        self.GetComponent<Animator>().Play("Attack1");

        yield return new WaitForSeconds(0.75f);
        self.ChangeState(new EnemyMovState());


    }
   

    
}
