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

            Debug.Log("Transitioning to Airborne State for Boss");
            BossTest.bossSM.ChangeState(BossTest.bossAirborneState);
            currentState.Exit();
            currentState = null;
            return;


        }
        BossTest.AttackOnceAirborne = false;

        if (BossTest.IsInDialogue)
        {
            if (currentState != BossTest.scriptedState)
                bossGroundedSm.ChangeState(currentState = BossTest.scriptedState);


        }
        else if (BlackboardForBoss.state == BossState.ShadowStep)
        {
            if (currentState != BossTest.sSState)
                bossGroundedSm.ChangeState(currentState = BossTest.sSState);
        }
        else if (BlackboardForBoss.state == BossState.Attack)
        {
            if (currentState != BossTest.grAttackState)
                bossGroundedSm.ChangeState(currentState = BossTest.grAttackState);


        }

        else if (BlackboardForBoss.state == BossState.Running)
        {
            if (currentState != BossTest.bossRunningState)
                bossGroundedSm.ChangeState(currentState = BossTest.bossRunningState);


        }
        else if (BlackboardForBoss.state == BossState.Jump)
        {
            if (currentState != BossTest.bossFirstJumpState)
                bossGroundedSm.ChangeState(currentState = BossTest.bossFirstJumpState);




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
