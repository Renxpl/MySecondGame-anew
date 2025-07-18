using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJumpState : IState
{

    bool firstTimeStop;
    BossTest mainScript = GameObject.Find("Boss").GetComponent<BossTest>();
    public void Enter()
    {
        firstTimeStop = false;
        BossTest.isSpriteLocked = true;

    }

    public void Update()
    {

        if(Mathf.Abs(PlayerController.PlayerRB.position.y - BossTest.bossRb.position.y) < 0.5f && !firstTimeStop)
        {
            BossTest.bossRb.velocity = Vector2.zero;
            firstTimeStop= true;
        }
        Transform currentP;
        if (BossTest.CurrentHealth / mainScript.hp > 1 / 3f)
            currentP = mainScript.sA1P;
        else
            currentP = mainScript.sA2P;

        float distanceX;

        if (BlackboardForBoss.purpose == BossPurpose.SpecialAttack)
        {
            distanceX = currentP.position.x - BossTest.bossRb.position.x;
        }
        else
        {
            distanceX = PlayerController.PlayerRB.position.x - BossTest.bossRb.position.x;
        }
        if (Mathf.Abs(PlayerController.PlayerRB.position.x - BossTest.bossRb.position.x) > 2f)
        {
            
            if (BossTest.bossRb.velocity.y < 0)
            {

                BossTest.bossRb.velocity = new Vector2(distanceX * 2, BossTest.bossRb.velocity.y * 1.1f);


            }
            else
            {

                BossTest.bossRb.velocity = new Vector2(distanceX * 2, BossTest.bossRb.velocity.y);

            }

        }
        else
        {
            if (BossTest.bossRb.velocity.y < 0)
            {

                BossTest.bossRb.velocity = new Vector2(0f, BossTest.bossRb.velocity.y * 1.1f);


            }
            else
            {
                BossTest.bossRb.velocity = new Vector2(0f, BossTest.bossRb.velocity.y);
            }
            

        }


        if (BossTest.bossRb.velocity.y > 0)
        {
            BossTest.ChangeAnimation(BossTest.ju);
        }
        else
        {
            BossTest.ChangeAnimation(BossTest.jd); 
        }

        if(distanceX != 0)
        BossTest.bossRb.transform.localScale = new Vector2(Mathf.Sign(distanceX), 1f);



    }

    public void Exit()
    {

        firstTimeStop = false;
        BossTest.isSpriteLocked = false;
    }






}
