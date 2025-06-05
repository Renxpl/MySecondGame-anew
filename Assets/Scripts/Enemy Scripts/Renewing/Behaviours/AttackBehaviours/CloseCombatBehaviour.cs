using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCombatBehaviour : IAttackBehaviour
{

    int attackStep;
    
    public void Attack(EnemyController self, Rigidbody2D enemyRB, Transform target)
    {
        attackStep = 0;
        self.Run(PerformAttack(self,enemyRB,target));
       


        



    }


    IEnumerator PerformAttack(EnemyController self, Rigidbody2D enemyRB, Transform target)
    {

        while(Mathf.Abs(target.position.x - enemyRB.position.x) < self.Combo.steps[attackStep%3].range)
        {
            self.GetComponent<Animator>().Play(self.Combo.steps[attackStep%3].animation);

            yield return new WaitForSeconds(self.Combo.durations[attackStep % 3]);
           
            attackStep++;


        }


        self.ChangeState(new EnemyMovState());

    }
   

    
}
