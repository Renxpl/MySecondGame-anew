using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAirborneState :IState
{
    int a = 0;
    
    public void Enter()
    {

    }


    public void Update()
    {
        if (PlayerNeededValues.IsGroundedPlayer)
        {
            
            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);
            CommandHandler.StartNext();

            return;
        }

        if (PlayerNeededValues.IsJumpingUp)
        {
            PlayerController.PlayerRB.velocity = new Vector2(8f * Math.Sign(PlayerNeededValues.MoveInput.x), PlayerNeededValues.JumpSpeed);
            PlayerController.ChangeAnimationState("JumpingUp");
            if (a % 2 == 0 ) { Debug.Log("azalma"); a++;  }

        }
        else
        {
            PlayerController.PlayerRB.velocity = new Vector2(8f * Math.Sign(PlayerNeededValues.MoveInput.x), PlayerController.PlayerRB.velocity.y);
            PlayerController.ChangeAnimationState("JumpingDown");
            if (a % 2 == 1) { Debug.Log("korunma"); a++; }
            CommandHandler.ResetNext();
        }
        //Debug.Log("MoveInput Debug Display " + Math.Sign(PlayerNeededValues.MoveInput.x));
        //PlayerController.ChangeAnimationState("Idle");

    }


    public void Exit()
    {

    }



}
