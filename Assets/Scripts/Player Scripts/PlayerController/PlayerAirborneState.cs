using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAirborneState :IState
{
    int a = 0;

    public static bool isAirborne = false;
    float jumpSpeed = 7.5f;
    public void Enter()
    {

    }


    public void Update()
    {
        
        if (PlayerNeededValues.IsGroundedPlayer)
        {
            isAirborne = false; 
            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);
            
            
            return;

        }

        if (PlayerNeededValues.IsAirborneAttack)
        {
            PlayerController.playerSM.ChangeState(PlayerNeededValues.playerAAstate);
            return;
        }

        else if (PlayerNeededValues.IsKnocbacking)
        {
            PlayerController.playerSM.ChangeState(PlayerNeededValues.playerKbState);
            return;
        }
        if (!PlayerNeededValues.CanDoActionDuringJump)
        {
            CommandHandler.ResetNext();
        }

        isAirborne = true;

        if (PlayerNeededValues.IsJumpingUp)
        {
            PlayerController.PlayerRB.velocity = new Vector2(jumpSpeed * Math.Sign(PlayerNeededValues.MoveInput.x), PlayerNeededValues.JumpSpeed);
            PlayerController.ChangeAnimationState("JumpingUp");
           if (a % 2 == 0 ) { a++;  }

        }
        else
        {
            PlayerController.PlayerRB.velocity = new Vector2(jumpSpeed * Math.Sign(PlayerNeededValues.MoveInput.x), PlayerController.PlayerRB.velocity.y);
            PlayerController.ChangeAnimationState("JumpingDown");
           if (a % 2 == 1) { a++; CommandHandler.ResetNext(); }
            

        }
        //Debug.Log("MoveInput Debug Display " + Math.Sign(PlayerNeededValues.MoveInput.x));
        //PlayerController.ChangeAnimationState("Idle");

    }


    public void Exit()
    {
        
        CommandHandler.StartNext();

    }



}
