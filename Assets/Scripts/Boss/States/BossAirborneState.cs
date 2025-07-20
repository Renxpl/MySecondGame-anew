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

        Debug.Log(BossTest.groundCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) );


        if (BossTest.groundCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || BossTest.knockback)
        {
            BossTest.AttackStep = 1;
            BossTest.bossSM.ChangeState(BossTest.bossGroundedState);
            currentState.Exit();
            currentState = null;
            return;


        }





        if ((BlackboardForBoss.state == BossState.Attack && !BossTest.AttackOnceAirborne)|| BossTest.ISAStarted)
        {
            if (currentState != BossTest.aAState)
                bossAirborneSm.ChangeState(currentState = BossTest.aAState);


        }


        else
        {
            if (currentState != BossTest.bossJumpState)
                bossAirborneSm.ChangeState(currentState = BossTest.bossJumpState);

        }





        bossAirborneSm.Update();

    }

    public void Exit()
    {


    }
}
