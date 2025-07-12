using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJumpState : IState
{

    bool firstTimeStop;

    public void Enter()
    {
        firstTimeStop = false;


    }

    public void Update()
    {

        if(Mathf.Abs( PlayerController.PlayerRB.position.y - BossTest.bossRb.position.y) < 0.5f && !firstTimeStop)
        {
            BossTest.bossRb.velocity = Vector2.zero;
            firstTimeStop= true;
        }


        if (BossTest.bossRb.velocity.y > 0)
        {
            BossTest.ChangeAnimation(BossTest.ju);
        }
        else
        {
            BossTest.ChangeAnimation(BossTest.jd); 
        }

        if(BossTest.bossRb.velocity.y < 0)
        {

            BossTest.bossRb.velocity = new Vector2(BossTest.bossRb.velocity.x, BossTest.bossRb.velocity.y * 1.1f);


        }




    }

    public void Exit()
    {

        firstTimeStop = false;
    }






}
