using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VolumeComponent;

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
            if (self.AttackStep % totalAC > 3)
            {
                int rndInt = UnityEngine.Random.Range(4, 7);

                // only for testing purposes
                rndInt = 5;

                while (rndInt != (self.AttackStep % totalAC))
                {

                    self.IncreaseAttackStep();


                }



            }
            if(self.AttackStep % totalAC != 4)
                self.GetComponent<Animator>().Play(self.Combo.steps[self.AttackStep % totalAC].animation);
            self.LockEnemySprite();
            self.SetPrePosition(self.transform.position);

            yield return new WaitForSeconds(self.Combo.steps[self.AttackStep % totalAC].delayBeforeHit);

            //enemyRB.WakeUp();
            //need to make first attack slower and more recognizable at the moment of attack1

            


            if (self.AttackStep % totalAC == 0)
            {
                self.Combo.steps[self.AttackStep % totalAC].hitbox.enabled = true;
                enemyRB.WakeUp();
                Vector2 currentPos = new Vector2(enemyRB.transform.position.x, enemyRB.transform.position.y);
                enemyRB.MovePosition(currentPos + (Mathf.Sign(enemyRB.transform.localScale.x) * new Vector2(4, 0)));


            }
            else if (self.AttackStep % totalAC == 1)
            {

                //there will be dash here first
                Vector2 currentPos = new Vector2(enemyRB.transform.position.x, enemyRB.transform.position.y);
                //enemyRB.MovePosition(currentPos + (Mathf.Sign(enemyRB.transform.localScale.x) * new Vector2(2, 0)));
                float targetX = target.position.x + (Mathf.Sign(enemyRB.transform.localScale.x) * -1f);





                enemyRB.MovePosition(new Vector2(targetX, currentPos.y));

                self.Combo.steps[self.AttackStep % totalAC].hitbox.enabled = true;
                enemyRB.WakeUp();

            }
            else if(self.AttackStep % totalAC == 2)
            {

                self.UnlockEnemySprite();
                yield return new WaitForSeconds(0.01f);
                self.LockEnemySprite();
                yield return new WaitForSeconds(0.1f);
                self.Combo.steps[self.AttackStep % totalAC].hitbox.enabled = true;
                enemyRB.WakeUp();
                Vector2 currentPos = new Vector2(enemyRB.transform.position.x, enemyRB.transform.position.y);
                //enemyRB.MovePosition(currentPos + (Mathf.Sign(enemyRB.transform.localScale.x) * new Vector2(2, 0)));
                float targetX = target.position.x + (Mathf.Sign(enemyRB.transform.localScale.x) * 2f);





                enemyRB.MovePosition(new Vector2(targetX, currentPos.y));
             
            }
            else if (self.AttackStep % totalAC == 3)
            {
                self.Combo.steps[self.AttackStep % totalAC].hitbox.enabled = true;
                enemyRB.WakeUp();
                Vector2 currentPos = new Vector2(enemyRB.transform.position.x, enemyRB.transform.position.y);
                //enemyRB.MovePosition(currentPos + (Mathf.Sign(enemyRB.transform.localScale.x) * new Vector2(2, 0)));
                enemyRB.MovePosition(currentPos + (Mathf.Sign(enemyRB.transform.localScale.x) * new Vector2(0.5f, 0)));

                Vector2 fbPosition = new Vector2(self.transform.position.x + (Mathf.Sign(self.transform.localScale.x) * 1.65f), self.transform.position.y);
                var fb = GameObject.Instantiate(self.Combo.steps[self.AttackStep % totalAC].projectilePrefab, fbPosition, Quaternion.identity);
                fb.GetComponent<A4PScript>().LaunchProjectile(new Vector2(self.transform.localScale.x, 0f));



            }
            //Magic Attacks
            //exp

            else if (self.AttackStep % totalAC == 4)
            {
                Vector2 expPosition = new Vector2(self.transform.position.x , self.transform.position.y);
                var exp = GameObject.Instantiate(self.Combo.steps[self.AttackStep % totalAC].projectilePrefab, expPosition, Quaternion.identity);
                exp.GetComponent<ExpCommander>().LaunchExp(new Vector2(self.transform.localScale.x, 1f));
                yield return new WaitForSeconds(self.Combo.steps[self.AttackStep % totalAC].postDelay);
                enemyRB.WakeUp();
            }
            //falling Ground

            else if (self.AttackStep % totalAC == 5)
            {
                //self.Combo.steps[self.AttackStep % totalAC].hitbox.enabled = true;
                Vector2 fbPosition = new Vector2(self.transform.position.x , self.transform.position.y + 2f);
                var fb = GameObject.Instantiate(self.Combo.steps[self.AttackStep % totalAC].projectilePrefab, fbPosition, Quaternion.identity);
                fb.GetComponent<FallingGround>().LaunchProjectile(new Vector2(self.transform.localScale.x, -0.3f));
                enemyRB.WakeUp();
            }
            //magic chain

            else
            {
                self.Combo.steps[self.AttackStep % totalAC].hitbox.enabled = true;
                enemyRB.WakeUp();
            }



            //self.Combo.steps[self.AttackStep % totalAC].hitbox.enabled = true;
            //enemyRB.WakeUp();




            yield return new WaitForSeconds(self.Combo.steps[self.AttackStep % totalAC].postDelay);
            if(self.Combo.steps[self.AttackStep % totalAC].hitbox != null) self.Combo.steps[self.AttackStep % totalAC].hitbox.enabled = false;


            if (self.AttackStep % totalAC == 3 || self.AttackStep % totalAC == 1)
            {
                yield return new WaitForSeconds(self.Combo.comboCooldown);
            }



            if (self.AttackStep % totalAC <= 3)
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
           

            

            self.UnlockEnemySprite();
            yield return new WaitForSeconds(0.01f);

        }

        attack = null;
        self.ChangeState(new EnemyMovState());

    }
}
