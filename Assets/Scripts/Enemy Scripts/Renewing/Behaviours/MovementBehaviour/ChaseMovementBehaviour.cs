using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseMovementBehaviour : IMovementBehaviour
{
    //to be deleted later
    float range = 2.5f;

    public void Move(EnemyController self, Rigidbody2D enemyRB, Transform target)
    {
        if (self.CurrentStance <= 0)
        {
            self.ChangeState(new EnemyKnockbackState());
            return;

        }
        Vector2 dir = new Vector2(target.position.x - self.transform.position.x, 0).normalized;
        /*self.transform.localScale = new Vector2 (dir.x,1f);*///Sprite Adjustment
        //range will be next attack range which will be implemented later
        if(Mathf.Abs(target.position.x - self.transform.position.x) > self.Combo.steps[self.AttackStep % self.Combo.steps.Length].range)
        {
            float ambushY = 1.15f; 
            if (Mathf.Abs(target.position.y - self.transform.position.y) < 1.25f && (Mathf.Abs(target.position.x - self.transform.position.x) < 50f || self.transform.position.y < ambushY ))
            {
                enemyRB.velocity = self.Stats.moveSpeed * dir;
                self.GetComponent<Animator>().Play("Walking");
            }
            else
            {

                self.GetComponent<Animator>().Play("Idle");//have to be more flexible thing
            }

        }
        else
        {
            enemyRB.velocity = 0f * dir;
            if (Mathf.Abs(target.position.y - self.transform.position.y) < 1.25f)
            {
                self.ChangeState(new EnemyAttackState());//have to be in state not behaviour
            }
            else
            {
                
                self.GetComponent<Animator>().Play("Idle");//have to be more flexible thing
            }
            


        }
        



    }



}
