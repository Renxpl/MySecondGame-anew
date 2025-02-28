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
        if (!PlayerNeededValues.IsGroundedPlayer || Mathf.Abs(PlayerNeededValues.MoveInput.x) > 0.15f ) 
        {

            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);
            timePassed = 0;

            return;

        }
        if (PlayerNeededValues.IsRolling || PlayerNeededValues.IsJumping || PlayerNeededValues.IsHeavyAttack||PlayerNeededValues.IsLightAttack || PlayerNeededValues.IsSpecialAttack || PlayerNeededValues.IsKnocbacking)
        {

            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);
            timePassed = 0;
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
            if (timePassed >= (timeToBePassed+ (1 / 0.5) *0.75))
            {
                timePassed = 0f;
            }
        }



    }


    public void Exit()
    { 

    }

}
