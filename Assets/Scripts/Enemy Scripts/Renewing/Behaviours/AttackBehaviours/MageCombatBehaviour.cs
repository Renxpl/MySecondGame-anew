using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageCombatBehaviour : IAttackBehaviour
{
    Coroutine attack;
    public void Attack(EnemyController self, Rigidbody2D enemyRB, Transform target)
    {


        if (attack == null) attack = self.Run(PerformAttack(self, enemyRB, target));
        if (self.CurrentStance <= 0)
        {
            self.StopCo(attack);
            if(self.Combo.steps[1].hitbox!= null) self.Combo.steps[1].hitbox.enabled = false;
            self.UnlockEnemySprite();
            attack = null;
            self.ChangeState(new EnemyKnockbackState());

        }






    }
   


    IEnumerator PerformAttack(EnemyController self, Rigidbody2D enemyRB, Transform target)
    {

        while (Mathf.Abs(target.position.x - enemyRB.position.x) <= self.Combo.steps[0].range)
        {

            if(Mathf.Abs(target.position.x - enemyRB.position.x) <= self.Combo.steps[1].range)
            {
                while (self.AttackStep % 2 != 1)
                {
                    self.IncreaseAttackStep();
                }
                
            }
            else
            {
                while (self.AttackStep % 2 != 0)
                {
                    self.IncreaseAttackStep();
                }
            }

            self.GetComponent<Animator>().Play(self.Combo.steps[self.AttackStep % 2].animation);
            self.LockEnemySprite();
            yield return new WaitForSeconds(self.Combo.steps[self.AttackStep % 2].delayBeforeHit);

            if (self.AttackStep % 2 == 0)
            {
                Vector2 fbPosition = new Vector2(self.transform.position.x + (Mathf.Sign(self.transform.localScale.x)*1.65f), self.transform.position.y);
                var fb = GameObject.Instantiate(self.Combo.steps[self.AttackStep % 2].projectilePrefab, fbPosition, Quaternion.identity);
                fb.GetComponent<FireballScript>().LaunchFireball(new Vector2(self.transform.localScale.x, 0f));
                yield return new WaitForSeconds(self.Combo.steps[self.AttackStep % 2].postDelay);


            }
           

            else
            {
                self.Combo.steps[self.AttackStep % 2].hitbox.enabled = true;
                enemyRB.WakeUp();
                yield return new WaitForSeconds(0.1f);
                self.Combo.steps[self.AttackStep % 2].hitbox.enabled = false;


                yield return new WaitForSeconds(self.Combo.steps[self.AttackStep % 2].postDelay-0.1f);
                


            }

            

            if (self.AttackStep % 2 == 0 || self.AttackStep % 2 == 1)
            {
                self.GetComponent<Animator>().Play("Idle");
                yield return new WaitForSeconds(self.Combo.comboCooldown);
            }

            self.UnlockEnemySprite();
            yield return new WaitForSeconds(0.01f);

        }

        attack = null;
        self.ChangeState(new EnemyMovState());

    }
}
