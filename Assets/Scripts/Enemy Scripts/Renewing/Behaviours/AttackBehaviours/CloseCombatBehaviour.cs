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
            self.LockEnemySprite();
            yield return new WaitForSeconds(self.Combo.steps[self.AttackStep % 3].delayBeforeHit);

            
            self.Combo.steps[self.AttackStep % 3].hitbox.enabled= true;
            enemyRB.WakeUp();




            yield return new WaitForSeconds(self.Combo.steps[self.AttackStep % 3].postDelay);
            self.Combo.steps[self.AttackStep % 3].hitbox.enabled = false;

            self.IncreaseAttackStep();
            
            if(self.AttackStep %3 == 0 || self.AttackStep % 3 == 1)
            {
                yield return new WaitForSeconds(self.Combo.comboCooldown);
            }

            self.UnlockEnemySprite();
            yield return new WaitForSeconds(0.01f);

        }


        self.ChangeState(new EnemyMovState());

    }
   

    
}
