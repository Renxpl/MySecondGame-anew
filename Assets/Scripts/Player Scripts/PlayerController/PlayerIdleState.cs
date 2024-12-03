using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState
{


    float timePassed;
    float timeToBePassed = 5f;
    public void Enter()
    {

    }


    public void Update()
    {
        //Going Back To GroundedState
        if (!PlayerNeededValues.IsGroundedPlayer || PlayerNeededValues.MoveInput.x != 0 ) 
        {

            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);

            return;

        }
        if (PlayerNeededValues.IsRolling || PlayerNeededValues.IsJumping)
        {

            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);

            return;


        }
        timePassed += Time.deltaTime;

        PlayerController.PlayerRB.velocity = new Vector2(0f, 0f);

        if (timePassed < timeToBePassed)
        {
            PlayerController.ChangeAnimationState("Idle");
        }
        else
        {
            PlayerController.ChangeAnimationState("IdleAnim1");
            if (timePassed >= (timeToBePassed+ (1 / 0.7) *0.583))
            {
                timePassed = 0f;
            }
        }



    }


    public void Exit()
    { 

    }

}
