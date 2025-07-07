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
                rndInt = 6;

                while (rndInt != (self.AttackStep % totalAC))
                {

                    self.IncreaseAttackStep();


                }



            }
            if(self.AttackStep % totalAC != 4 && self.AttackStep % totalAC != 6)
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
                bool isPlayerCaptured = false;
                self.GetComponent<Animator>().Play(self.Combo.steps[self.AttackStep % totalAC].animation);
                if (Mathf.Abs(target.position.y - self.transform.position.y) < 0.5f && self.transform.localScale.x * (target.position.x - self.transform.position.x) > 2.5f)
                {
                    self.Combo.steps[4].hitbox.enabled = true;
                    enemyRB.WakeUp();

                    yield return new WaitForSeconds(0.09f);

                }
                else if (Mathf.Abs(target.position.y - self.transform.position.y) < 0.5f && self.transform.localScale.x * (target.position.x - self.transform.position.x) < 2.5f && self.transform.localScale.x * (target.position.x - self.transform.position.x) > 0f)
                {
                    isPlayerCaptured = true;
                    self.Combo.steps[4].hitbox.enabled = true;
                    enemyRB.WakeUp();
                    self.Combo.steps[6].hitbox.gameObject.transform.Find("ChainTip").transform.position = target.position;
                    self.Combo.steps[6].hitbox.gameObject.transform.Find("ChainTip").gameObject.SetActive(true);

                    yield return new WaitForSeconds(0.09f);
                    self.Combo.steps[6].hitbox.gameObject.transform.Find("ChainTip").gameObject.SetActive(false);
                    self.GetComponent<Animator>().Play("Magic3_2");
                }
              

                if(Mathf.Abs(target.position.y - self.transform.position.y) < 0.5f && self.transform.localScale.x * (target.position.x-self.transform.position.x) > 5f && !isPlayerCaptured)
                {
                    self.Combo.steps[5].hitbox.enabled = true;
                    self.Combo.steps[4].hitbox.enabled = false;
                    enemyRB.WakeUp();

                    yield return new WaitForSeconds(0.09f);

                }
                else if (Mathf.Abs(target.position.y - self.transform.position.y) < 0.5f && self.transform.localScale.x * (target.position.x - self.transform.position.x) < 5f && self.transform.localScale.x * (target.position.x - self.transform.position.x) > 0f && !isPlayerCaptured)
                {
                    isPlayerCaptured = true;
                    self.Combo.steps[5].hitbox.enabled = true;
                    self.Combo.steps[4].hitbox.enabled = false;
                    enemyRB.WakeUp();
                    self.Combo.steps[6].hitbox.gameObject.transform.Find("ChainTip").transform.position = target.position;
                    self.Combo.steps[6].hitbox.gameObject.transform.Find("ChainTip").gameObject.SetActive(true);

                    yield return new WaitForSeconds(0.09f);
                    self.Combo.steps[6].hitbox.gameObject.transform.Find("ChainTip").gameObject.SetActive(false);
                    self.GetComponent<Animator>().Play("Magic3_2");
                }

                if (Mathf.Abs(target.position.y - self.transform.position.y) < 0.5f && self.transform.localScale.x * (target.position.x - self.transform.position.x) > 7.5f && !isPlayerCaptured)
                {
                   
                    self.Combo.steps[6].hitbox.enabled = true;
                    self.Combo.steps[5].hitbox.enabled = false;
                    enemyRB.WakeUp();
                    yield return new WaitForSeconds(0.3f);

                }

                else if (Mathf.Abs(target.position.y - self.transform.position.y) < 0.5f && self.transform.localScale.x * (target.position.x - self.transform.position.x) < 7.5f && self.transform.localScale.x * (target.position.x - self.transform.position.x) > 0f && !isPlayerCaptured)
                {
                    isPlayerCaptured = true;
                    self.Combo.steps[6].hitbox.enabled = true;
                    self.Combo.steps[5].hitbox.enabled = false;
                    enemyRB.WakeUp();
                    self.Combo.steps[6].hitbox.gameObject.transform.Find("ChainTip").transform.position = target.position;
                    self.Combo.steps[6].hitbox.gameObject.transform.Find("ChainTip").gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.3f);
                    self.Combo.steps[6].hitbox.gameObject.transform.Find("ChainTip").gameObject.SetActive(false);
                    self.GetComponent<Animator>().Play("Magic3_2");
                }





            }



            //self.Combo.steps[self.AttackStep % totalAC].hitbox.enabled = true;
            //enemyRB.WakeUp();




            yield return new WaitForSeconds(self.Combo.steps[self.AttackStep % totalAC].postDelay);
            if(self.Combo.steps[self.AttackStep % totalAC].hitbox != null) self.Combo.steps[self.AttackStep % totalAC].hitbox.enabled = false;
            if (self.AttackStep % totalAC == 6)
            {
                self.Combo.steps[4].hitbox.enabled = false;
                self.Combo.steps[5].hitbox.enabled = false;
                self.Combo.steps[6].hitbox.enabled = false;
            }


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
