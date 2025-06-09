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
            self.Combo.steps[1].hitbox.enabled = false;
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



            self.GetComponent<Animator>().Play(self.Combo.steps[self.AttackStep % 2].animation);
            self.LockEnemySprite();
            yield return new WaitForSeconds(self.Combo.steps[self.AttackStep % 2].delayBeforeHit);

            if (self.AttackStep % 2 == 0)
            {

                var fb = GameObject.Instantiate(self.Combo.steps[self.AttackStep % 2].projectilePrefab,self.transform.position ,Quaternion.identity);
                fb.GetComponent<FireballScript>().LaunchFireball(new Vector2(self.transform.localScale.x, 0f));



            }
           

            else
            {
                self.Combo.steps[self.AttackStep % 2].hitbox.enabled = true;
                enemyRB.WakeUp();




                yield return new WaitForSeconds(self.Combo.steps[self.AttackStep % 2].postDelay);
                self.Combo.steps[self.AttackStep % 2].hitbox.enabled = false;


            }

            

            if (self.AttackStep % 2 == 0 || self.AttackStep % 2 == 1)
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
