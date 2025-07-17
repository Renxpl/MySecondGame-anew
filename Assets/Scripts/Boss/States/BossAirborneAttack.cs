using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAirborneAttack : IState
{
    float timePassed;
    Coroutine attackCo;
    BossTest bossMainScript = GameObject.Find("Boss").GetComponent<BossTest>();
    GameObject player = GameObject.Find("Player");
    int rnd;
    
    public void Enter()
    {
        rnd = UnityEngine.Random.Range(1,3);
        timePassed = 0f;
        BossTest.bossRb.velocity = Vector2.zero;
        BossTest.isSpriteLocked = true;
       
        if(rnd == 1)
        {
            BossTest.AttackStep = 4;
        }
        else
        {
            BossTest.AttackStep = 5;
        }
    }


    IEnumerator Attack1()
    {
        float distanceX = player.transform.position.x - bossMainScript.transform.position.x;
        if (distanceX != 0f) bossMainScript.transform.localScale = new Vector2(Mathf.Sign(distanceX), 1f);
        BossTest.ISAStarted = true;
        BossTest.bossRb.velocity = Vector2.zero;
        distanceX = player.transform.position.x - bossMainScript.transform.position.x;
        BossTest.ChangeAnimation(BossTest.aa1);
        float forceFactor = 2f * distanceX;
        Vector2 force = new Vector2(bossMainScript.transform.localScale.x * forceFactor, 0f);
        BossTest.bossRb.AddForce(force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.35f);
        BossTest.attackHitboxes[0].enabled = true;
        BossTest.bossRb.velocity = Vector2.zero;
        
        yield return new WaitForSeconds(0.32f);
        BossTest.attackHitboxes[0].enabled = false;
        BossTest.AttackStep = 0;
        BossTest.ISAStarted = false;
        BossTest.AttackOnceAirborne = true;
        attackCo = null;







    }
    IEnumerator Attack2()
    {
        float distanceX = player.transform.position.x - bossMainScript.transform.position.x;
        if (distanceX != 0f) bossMainScript.transform.localScale = new Vector2(Mathf.Sign(distanceX), 1f);
        BossTest.ISAStarted = true;
        BossTest.ChangeAnimation(BossTest.aa2);
        float forceFactor = 20f;
        Vector2 force = new Vector2(bossMainScript.transform.localScale.x * forceFactor, 0f);
        BossTest.bossRb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.35f);
        BossTest.attackHitboxes[1].enabled = true;
        BossTest.bossRb.AddForce(force, ForceMode2D.Impulse);

       
        yield return new WaitForSeconds(0.32f);
        BossTest.attackHitboxes[1].enabled = false;
        BossTest.AttackStep = 0;
        BossTest.ISAStarted = false;
        BossTest.AttackOnceAirborne = true;
        attackCo = null;







    }
   
    public void Update()
    {

        timePassed += Time.deltaTime;
        if (BossTest.AttackStep == 4)
        {
            

            if (attackCo == null)
            {
                attackCo = bossMainScript.Run(Attack1());
            }




        }

        else if (BossTest.AttackStep == 5)
        {
          
            if (attackCo == null)
                attackCo = bossMainScript.Run(Attack2());

        }

      








    }

    public void Exit()
    {
        
        BossTest.isSpriteLocked = false;

    }
}
