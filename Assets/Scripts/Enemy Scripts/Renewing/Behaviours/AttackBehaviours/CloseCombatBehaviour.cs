using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCombatBehaviour : IAttackBehaviour
{

    Coroutine attack;
    Vector2 position= Vector2.zero;
    bool isDuringAttack = false;
    public void Attack(EnemyController self, Rigidbody2D enemyRB, Transform target)
    {

        float distanceY = Mathf.Abs(target.transform.position.y - enemyRB.transform.position.y);
        bool isTooHigh = distanceY >= 1.25f;
        
        if (attack == null) attack = self.Run(PerformAttack(self,enemyRB,target));
        if(self.CurrentStance <= 0)
        {
            self.StopCo(attack);
            self.Combo.steps[self.AttackStep % 3].hitbox.enabled = false;
            self.UnlockEnemySprite();
            isDuringAttack = false;
            attack= null;
            self.ChangeState(new EnemyKnockbackState());

        }
        if (isTooHigh && !isDuringAttack)
        {
            self.StopCo(attack);
            self.Combo.steps[self.AttackStep % 3].hitbox.enabled = false;
            self.UnlockEnemySprite();
            attack = null;
            self.ChangeState(new EnemyMovState());
        }






    }


    IEnumerator PerformAttack(EnemyController self, Rigidbody2D enemyRB, Transform target)
    {
        
       
        while(Mathf.Abs(target.position.x - enemyRB.position.x) <= self.Combo.steps[self.AttackStep%3].range)
        {

            self.GetComponent<Animator>().Play(self.Combo.steps[self.AttackStep%3].animation);
            self.LockEnemySprite();
            self.SetPrePosition(self.transform.position);
            
            

            //enemyRB.WakeUp();
            //need to make first attack slower and more recognizable at the moment of attack1

            

            if(self.AttackStep % 3 == 0)
            {

                yield return new WaitForSeconds(0.1f);
                float distanceX = Mathf.Abs(target.position.x - enemyRB.transform.position.x);

                //enemyRB.velocity = new Vector2(enemyRB.transform.localScale.x * 10f, 0f);
                enemyRB.AddForce(new Vector2(enemyRB.transform.localScale.x * (2.5f/6f) * distanceX, 0f), ForceMode2D.Impulse);
                yield return new WaitForSeconds(0.1f);
                enemyRB.AddForce(new Vector2(enemyRB.transform.localScale.x * (20f/6f) * distanceX, 0f), ForceMode2D.Impulse);
                
                //Vector2 currentPos = new Vector2(enemyRB.transform.position.x, enemyRB.transform.position.y);
                //float targetX = target.position.x + (Mathf.Sign(enemyRB.transform.localScale.x) * -2f);





                //enemyRB.MovePosition(new Vector2(targetX, currentPos.y));
                yield return new WaitForSeconds(self.Combo.steps[self.AttackStep % 3].delayBeforeHit-0.2f);
                enemyRB.velocity = Vector2.zero;
                self.Combo.steps[self.AttackStep % 3].hitbox.enabled = true;
                isDuringAttack= true;
                enemyRB.WakeUp();
                //Vector2 currentPos = new Vector2(enemyRB.transform.position.x, enemyRB.transform.position.y);
                //enemyRB.MovePosition(currentPos + (Mathf.Sign(enemyRB.transform.localScale.x)*new Vector2(2,0))); Physics2D.SyncTransforms();



            }
            else if (self.AttackStep % 3 == 1)
            {



                yield return new WaitForSeconds(self.Combo.steps[self.AttackStep % 3].delayBeforeHit);
                self.UnlockEnemySprite();
                yield return new WaitForSeconds(0.01f);
                self.LockEnemySprite();

                yield return new WaitForSeconds(0.1f);
                
                self.Combo.steps[self.AttackStep % 3].hitbox.enabled = true;
                isDuringAttack = true;
                enemyRB.WakeUp();

               
                //enemyRB.MovePosition(currentPos + (Mathf.Sign(enemyRB.transform.localScale.x) * new Vector2(4, 0)));
                Vector2 currentPos = new Vector2(enemyRB.transform.position.x, enemyRB.transform.position.y);
                float targetX = target.position.x + (Mathf.Sign(enemyRB.transform.localScale.x) * 2f);
                
             

              
                
                enemyRB.MovePosition(new Vector2(targetX ,currentPos.y));

            }
            else
            {


                yield return new WaitForSeconds(0.1f);
                enemyRB.AddForce(new Vector2(enemyRB.transform.localScale.x* 2f,0f), ForceMode2D.Impulse);
                self.Combo.steps[self.AttackStep % 3].hitbox.enabled = true;
                isDuringAttack = true;
                enemyRB.WakeUp();
            }

            


            yield return new WaitForSeconds(self.Combo.steps[self.AttackStep % 3].postDelay);

            self.Combo.steps[self.AttackStep % 3].hitbox.enabled = false;
           

            self.IncreaseAttackStep();

            if (self.AttackStep %3 == 0 || self.AttackStep % 3 == 1)
            {
                yield return new WaitForSeconds(self.Combo.comboCooldown);
            }

            self.UnlockEnemySprite();
            isDuringAttack = false;
            yield return new WaitForSeconds(0.01f);

        }

        attack = null;
        self.ChangeState(new EnemyMovState());

    }

   

    
}
