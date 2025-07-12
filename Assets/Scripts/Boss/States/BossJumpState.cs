using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJumpState : IState
{
    public void Enter()
    {

        

    }

    public void Update()
    {

        


        if (BossTest.bossRb.velocity.y > 0)
        {
            BossTest.ChangeAnimation(BossTest.ju);
        }
        else
        {
            BossTest.ChangeAnimation(BossTest.jd);
        }





    }

    public void Exit()
    {


    }






}
