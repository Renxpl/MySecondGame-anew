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

        Debug.Log(attackType);

    }


    IEnumerator Attack1()
    {

        BossTest.ChangeAnimation(BossTest.sa1);
        yield return new WaitForSeconds(0.3f);
        BossTest.attackHitboxes[4].enabled = true;
        yield return new WaitForSeconds(0.25f);
        BossTest.attackHitboxes[4].enabled = false;
        yield return new WaitForSeconds(0.35f);
        attackCo = null;
        BossTest.IsSAStarted = false;
        


    }
    IEnumerator Attack2()
    {
        BossTest.bossRb.transform.localScale = new Vector2(1f,1f);
        BossTest.ChangeAnimation(BossTest.sa2);
        yield return new WaitForSeconds(0.3f);
        BossTest.attackHitboxes[5].enabled = true;
        yield return new WaitForSeconds(0.25f);

        BossTest.attackHitboxes[5].enabled = false;
        yield return new WaitForSeconds(0.35f);
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
