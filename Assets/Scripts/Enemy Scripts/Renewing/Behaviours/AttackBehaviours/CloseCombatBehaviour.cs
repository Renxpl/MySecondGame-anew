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
       
        while(Mathf.Abs(target.position.x - enemyRB.position.x) < self.Combo.steps[self.AttackStep%3].range)
        {
            self.GetComponent<Animator>().Play(self.Combo.steps[self.AttackStep%3].animation);

            yield return new WaitForSeconds(self.Combo.durations[self.AttackStep % 3]);



            self.IncreaseAttackStep();
            
            if(self.AttackStep %3 == 0)
            {
                yield return new WaitForSeconds(self.Combo.comboCooldown);
            }

        }


        self.ChangeState(new EnemyMovState());

    }
   

    
}
