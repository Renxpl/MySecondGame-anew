using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderCombatBehav : IAttackBehaviour
{
    Coroutine attack;
    Vector2 position = Vector2.zero;

    public void Attack(EnemyController self, Rigidbody2D enemyRB, Transform target)
    {


        if (attack == null) attack = self.Run(PerformAttack(self, enemyRB, target));
        if (self.CurrentStance <= 0)
        {
            self.StopCo(attack);
            self.Combo.steps[self.AttackStep % self.Combo.steps.Length].hitbox.enabled = false;
            self.UnlockEnemySprite();
            attack = null;
            self.ChangeState(new EnemyKnockbackState());

        }






    }


    IEnumerator PerformAttack(EnemyController self, Rigidbody2D enemyRB, Transform target)
    {

        while (Mathf.Abs(target.position.x - enemyRB.position.x) <= self.Combo.steps[self.AttackStep % self.Combo.steps.Length].range)
        {
            int totalAC= self.Combo.steps.Length;
            self.GetComponent<Animator>().Play(self.Combo.steps[self.AttackStep % totalAC].animation);
            self.LockEnemySprite();
            self.SetPrePosition(self.transform.position);

            yield return new WaitForSeconds(self.Combo.steps[self.AttackStep % totalAC].delayBeforeHit);

            //enemyRB.WakeUp();
            //need to make first attack slower and more recognizable at the moment of attack1

            if(self.AttackStep % totalAC > 3)
            {
                int rndInt = Random.Range(4, 7);

                while(rndInt != (self.AttackStep % totalAC))
                {

                    self.IncreaseAttackStep();


                }



            }


            if (self.AttackStep % totalAC == 0)
            {
                Vector2 currentPos = new Vector2(enemyRB.transform.position.x, enemyRB.transform.position.y);
                enemyRB.MovePosition(currentPos + (Mathf.Sign(enemyRB.transform.localScale.x) * new Vector2(2, 0)));


            }
            else if (self.AttackStep % totalAC == 1)
            {
                

            }
            else if(self.AttackStep % totalAC == 2)
            {

                self.UnlockEnemySprite();
                yield return new WaitForSeconds(0.01f);
                self.LockEnemySprite();
                yield return new WaitForSeconds(0.1f);
                Vector2 currentPos = new Vector2(enemyRB.transform.position.x, enemyRB.transform.position.y);
                enemyRB.MovePosition(currentPos + (Mathf.Sign(enemyRB.transform.localScale.x) * new Vector2(4, 0)));
            }
            else if (self.AttackStep % totalAC == 3)
            {

                Vector2 currentPos = new Vector2(enemyRB.transform.position.x, enemyRB.transform.position.y);
                enemyRB.MovePosition(currentPos + (Mathf.Sign(enemyRB.transform.localScale.x) * new Vector2(2, 0)));
            }
            //Magic Attacks
            //exp

            else if (self.AttackStep % totalAC == 4)
            {

            }
            //falling Ground

            else if (self.AttackStep % totalAC == 5)
            {

            }
            //magic chain

            else
            {

            }



            self.Combo.steps[self.AttackStep % totalAC].hitbox.enabled = true;
            enemyRB.WakeUp();




            yield return new WaitForSeconds(self.Combo.steps[self.AttackStep % totalAC].postDelay);
            self.Combo.steps[self.AttackStep % totalAC].hitbox.enabled = false;




            if(self.AttackStep % totalAC <= 3)
            {
                self.IncreaseAttackStep();
            }
            else
            {
                while(self.AttackStep % totalAC != 0)
                {
                    self.IncreaseAttackStep();
                }
            }

            if (self.AttackStep % totalAC == 0 || self.AttackStep % totalAC == 1)
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
