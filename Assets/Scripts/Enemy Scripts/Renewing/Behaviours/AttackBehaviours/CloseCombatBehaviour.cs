using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCombatBehaviour : IAttackBehaviour
{

    Coroutine attack;
    
    public void Attack(EnemyController self, Rigidbody2D enemyRB, Transform target)
    {


        if (attack == null) attack = self.Run(PerformAttack(self,enemyRB,target));
        if(self.CurrentStance <= 0)
        {
            self.StopCo(attack);
            self.Combo.steps[self.AttackStep % 3].hitbox.enabled = false;
            self.UnlockEnemySprite();
            self.ChangeState(new EnemyKnockbackState());

        }


        



    }


    IEnumerator PerformAttack(EnemyController self, Rigidbody2D enemyRB, Transform target)
    {
       
        while(Mathf.Abs(target.position.x - enemyRB.position.x) <= self.Combo.steps[self.AttackStep%3].range)
        {

            self.GetComponent<Animator>().Play(self.Combo.steps[self.AttackStep%3].animation);
            self.LockEnemySprite();
            yield return new WaitForSeconds(self.Combo.steps[self.AttackStep % 3].delayBeforeHit);

            //enemyRB.WakeUp();
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

        attack = null;
        self.ChangeState(new EnemyMovState());

    }
   

    
}
