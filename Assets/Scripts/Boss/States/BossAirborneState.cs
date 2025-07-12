using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAirborneState : IState
{

    StateMachine bossAirborneSm = new StateMachine();
    IState currentState;

    public void Enter()
    {


    }

    public void Update()
    {

        Debug.Log(BossTest.groundCollider.IsTouchingLayers(LayerMask.GetMask("Ground")));


        if (BossTest.groundCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            BossTest.bossSM.ChangeState(BossTest.bossGroundedState);
            currentState = null;
            return;


        }




        if (currentState != BossTest.bossJumpState)
            bossAirborneSm.ChangeState(currentState = BossTest.bossJumpState);

        if (Mathf.Abs(PlayerController.PlayerRB.position.x - BossTest.bossRb.position.x) > 2f)
        {
            //if (currentState != BossTest.bossRunningState)
                //bossGroundedSm.ChangeState(currentState = BossTest.bossRunningState);


        }


        else
        {
            //if (currentState != BossTest.bossIdleState)
                //bossGroundedSm.ChangeState(currentState = BossTest.bossIdleState);

        }





        bossAirborneSm.Update();

    }

    public void Exit()
    {


    }
}
