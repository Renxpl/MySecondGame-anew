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
        

        if (Mathf.Abs(PlayerController.PlayerRB.position.x - BossTest.bossRb.position.x)> 2f)
        {
            if(currentState != BossTest.bossRunningState)
            bossGroundedSm.ChangeState(currentState = BossTest.bossRunningState);


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
