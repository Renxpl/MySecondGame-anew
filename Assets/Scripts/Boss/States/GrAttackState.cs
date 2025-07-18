using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrAttackState : IState
{

    float timePassed;
    Coroutine attackCo;
    BossTest bossMainScript = GameObject.Find("Boss").GetComponent<BossTest>();
    GameObject player = GameObject.Find("Player");
    public void Enter()
    {
        timePassed = 0f;
        BossTest.bossRb.velocity = Vector2.zero;
        BossTest.isSpriteLocked = true;
        
    }


    IEnumerator Attack1()
    {
        BossTest.ChangeAnimation(BossTest.idleAnim1);
        BossTest.ISAStarted = true;
        yield return new WaitForSeconds(3f);
        float distanceX = player.transform.position.x - bossMainScript.transform.position.x;
        if (distanceX != 0f) bossMainScript.transform.localScale = new Vector2(Mathf.Sign(distanceX), 1f);
        BossTest.bossRb.velocity = Vector2.zero;
        distanceX = player.transform.position.x - bossMainScript.transform.position.x;
        BossTest.ChangeAnimation(BossTest.ga1);
        float forceFactor = 2f * distanceX;
        Vector2 force = new Vector2(bossMainScript.transform.localScale.x * forceFactor , 0f);
        BossTest.bossRb.AddForce(force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.35f);
        BossTest.attackHitboxes[0].enabled = true;
        BossTest.bossRb.velocity = Vector2.zero;
        BossTest.ISAStarted = true;
        yield return new WaitForSeconds(0.32f);
        BossTest.attackHitboxes[0].enabled = false;
        BossTest.AttackStep++;
        BossTest.ISAStarted = false;
        attackCo = null;




       


    }
    IEnumerator Attack2()
    {
        float distanceX = player.transform.position.x - bossMainScript.transform.position.x;
        if (distanceX != 0f) bossMainScript.transform.localScale = new Vector2(Mathf.Sign(distanceX), 1f);
        BossTest.ChangeAnimation(BossTest.ga2);
        float forceFactor = 10f;
        Vector2 force = new Vector2(bossMainScript.transform.localScale.x * forceFactor, 0f);
        BossTest.bossRb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.35f);
        BossTest.attackHitboxes[1].enabled = true;
        BossTest.bossRb.AddForce(force, ForceMode2D.Impulse);

        BossTest.ISAStarted = true;
        yield return new WaitForSeconds(0.32f);
        BossTest.attackHitboxes[1].enabled = false;
        BossTest.AttackStep++;
        BossTest.ISAStarted = false;
        attackCo = null;







    }
    IEnumerator Attack3()
    {
        float distanceX = player.transform.position.x - bossMainScript.transform.position.x;
        if (distanceX != 0f) bossMainScript.transform.localScale = new Vector2(Mathf.Sign(distanceX), 1f);
        BossTest.ChangeAnimation(BossTest.ga3);
        float forceFactor = 10f;
        Vector2 force = new Vector2(bossMainScript.transform.localScale.x * forceFactor, 0f);
        BossTest.bossRb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.35f);
        BossTest.attackHitboxes[2].enabled = true;
        BossTest.bossRb.AddForce(force, ForceMode2D.Impulse);
      
        BossTest.ISAStarted = true;
        yield return new WaitForSeconds(0.32f);
        BossTest.attackHitboxes[2].enabled = false;
        BossTest.AttackStep++;
        BossTest.ISAStarted = false;
        attackCo = null;







    }
    IEnumerator Attack4()
    {
        float distanceX = player.transform.position.x - bossMainScript.transform.position.x;
        if (distanceX != 0f) bossMainScript.transform.localScale = new Vector2(Mathf.Sign(distanceX), 1f);
        BossTest.ChangeAnimation(BossTest.ga4);
        float forceFactor = 10f;
        Vector2 force = new Vector2(bossMainScript.transform.localScale.x * forceFactor, 0f);
        BossTest.bossRb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.35f);
        BossTest.attackHitboxes[3].enabled = true;
        BossTest.bossRb.AddForce(force, ForceMode2D.Impulse);
        
        BossTest.ISAStarted = true;
        yield return new WaitForSeconds(0.32f);
        BossTest.bossRb.velocity = Vector2.zero;
        BossTest.ChangeAnimation(BossTest.idleAnim);
        BossTest.attackHitboxes[3].enabled = false;
        yield return new WaitForSeconds(1f);
        BossTest.AttackStep = 0;
        BossTest.ISAStarted = false;
        attackCo = null;







    }
    public void Update()
    {
        timePassed += Time.deltaTime;
        if(BossTest.AttackStep == 0)
        {
            

            if(attackCo == null)
            {
                attackCo = bossMainScript.Run(Attack1());
            }
            
            


        }

        else if(BossTest.AttackStep == 1)
        {
            
          
            if (attackCo == null)
                attackCo = bossMainScript.Run(Attack2());

        }

        else if(BossTest.AttackStep == 2)
        {
            
           
            if (attackCo == null)
                attackCo = bossMainScript.Run(Attack3());

        }
        else if(BossTest.AttackStep == 3)
        {
           
            if (attackCo == null)
                attackCo = bossMainScript.Run(Attack4());



        }




       



    }

    public void Exit()
    {
        BossTest.isSpriteLocked = false;
        if(attackCo != null)
        {
            BossTest.attackHitboxes[0].enabled = false;
            BossTest.attackHitboxes[1].enabled = false;
            BossTest.attackHitboxes[2].enabled = false;
            BossTest.attackHitboxes[3].enabled = false;

            BossTest.ISAStarted = false;
            bossMainScript.StopCo(attackCo);
            attackCo = null;
           

        }

    }


}
