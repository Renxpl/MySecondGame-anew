using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGroundedState : IState
{
    public void Enter()
    {


    }

    public void Update()
    {


        if (Mathf.Abs(PlayerController.PlayerRB.transform.position.x - BossTest.bossRb.position.x)> 2f)
        {

            BossTest.bossSM.ChangeState(BossTest.bossRunningState);


        }


        else
        {

            BossTest.bossSM.ChangeState(BossTest.bossIdleState);

        }



    }

    public void Exit()
    {


    }


}
