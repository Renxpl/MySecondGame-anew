using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGroundedState : IState
{
    StateMachine bossGroundedSm = new StateMachine();
    IState currentState;

    public void Enter()
    {


    }

    public void Update()
    {
        
        if (!BossTest.groundCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            BossTest.bossSM.ChangeState(BossTest.bossAirborneState);
            currentState = null;
            return;


        }

      


        if (Mathf.Abs(PlayerController.PlayerRB.position.x - BossTest.bossRb.position.x)> 2f)
        {
            if(currentState != BossTest.bossRunningState)
            bossGroundedSm.ChangeState(currentState = BossTest.bossRunningState);


        }
        else if (PlayerController.PlayerRB.position.y - BossTest.bossRb.position.y > 2f)
        {
            BossTest.bossSM.ChangeState(currentState = BossTest.bossFirstJumpState);
            currentState = null;
            


        }


        else
        {
            if (currentState != BossTest.bossIdleState)
                bossGroundedSm.ChangeState(currentState = BossTest.bossIdleState);

        }
        

        


        bossGroundedSm.Update();

    }

    public void Exit()
    {


    }


}
