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
            attack= null;
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
            //need to make first attack slower and more recognizable at the moment of attack1
            if(self.AttackStep % 3 == 0)
            {
                Vector2 currentPos = new Vector2(enemyRB.transform.position.x, enemyRB.transform.position.y);
                enemyRB.MovePosition(currentPos + (Mathf.Sign(enemyRB.transform.localScale.x)*new Vector2(2,0)));


            }
            else if (self.AttackStep % 3 == 1)
            {
                self.UnlockEnemySprite();
                yield return new WaitForSeconds(0.01f);
                self.LockEnemySprite();
                yield return new WaitForSeconds(0.1f);
                Vector2 currentPos = new Vector2(enemyRB.transform.position.x, enemyRB.transform.position.y);
                enemyRB.MovePosition(currentPos + (Mathf.Sign(enemyRB.transform.localScale.x) * new Vector2(4, 0)));

            }
            else
            {

            }

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
