using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAState : IState
{
   
    Coroutine attackCo;
    BossTest bossMainScript = GameObject.Find("Boss").GetComponent<BossTest>();
    
    int attackType;
    public void Enter()
    {
       
        BossTest.bossRb.velocity = Vector2.zero;
        BossTest.isSpriteLocked = true;
        BossTest.IsSAStarted = true;
        if (BossTest.CurrentHealth / bossMainScript.hp > 1 / 3f)
            attackType = 0;
        else
            attackType = 1;

        //debug
        //attackType = 0;
        
        //Debug.Log(attackType);

    }


    IEnumerator Attack1()
    {
        //debug
        //BossTest.bossRb.MovePosition(bossMainScript.sA1P.position);

        BossTest.bossRb.transform.localScale = new Vector2(1f, 1f);

        BossTest.ChangeAnimation(BossTest.sa1_1);
        yield return new WaitForSeconds(0.35f);
        BossTest.attackHitboxes[4].enabled = true;
        yield return new WaitForSeconds(0.15f);
        BossTest.attackHitboxes[4].enabled = false;

        BossTest.ChangeAnimation(BossTest.sa1_2);
        yield return new WaitForSeconds(0.43f);
        BossTest.attackHitboxes[5].enabled = true;
        yield return new WaitForSeconds(0.15f);
        BossTest.attackHitboxes[5].enabled = false;

        BossTest.ChangeAnimation(BossTest.sa1_3);
        yield return new WaitForSeconds(0.43f);
        BossTest.attackHitboxes[6].enabled = true;
        yield return new WaitForSeconds(0.15f);
        BossTest.attackHitboxes[6].enabled = false;

        attackCo = null;
        BossTest.IsSAStarted = false;
        


    }
    IEnumerator Attack2()
    {
        
        
        BossTest.attackHitboxes[7].offset = new Vector2(-33.2f, -3.5f);
        BossTest.bossRb.transform.localScale = new Vector2(1f,1f);
        BossTest.bossRb.bodyType = RigidbodyType2D.Kinematic;
        //debug
       // BossTest.bossRb.MovePosition(bossMainScript.sA2P.position);

        BossTest.ChangeAnimation(BossTest.sa2);
        yield return new WaitForSeconds(0.25f);
        Vector2 target = new Vector2(BossTest.bossRb.gameObject.transform.position.x+30f, BossTest.bossRb.gameObject.transform.position.y + 8f);
        BossTest.bossRb.MovePosition(target);
        BossTest.attackHitboxes[7].enabled = true;
        float counter = 0;
        while (counter <= 1f)
        {
            

            yield return new WaitForSeconds(0.084f);
            counter += 0.084f;
            BossTest.attackHitboxes[7].offset = new Vector2(BossTest.attackHitboxes[7].offset.x + 2.67f, BossTest.attackHitboxes[7].offset.y);

        }
        

        BossTest.attackHitboxes[7].enabled = false;
        BossTest.bossRb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(0.25f);
        attackCo = null;
        BossTest.IsSAStarted = false;
    }
   
    public void Update()
    {
      
        if (attackType== 0)
        {
           



            if (attackCo == null)
            {
                attackCo = bossMainScript.Run(Attack1());
            }




        }

        else if (attackType == 1)
        {


            if (attackCo == null)
                attackCo = bossMainScript.Run(Attack2());

        }

       







    }

    public void Exit()
    {
        BossTest.isSpriteLocked = false;
        BossTest.IsSAStarted = false;

    }
}
