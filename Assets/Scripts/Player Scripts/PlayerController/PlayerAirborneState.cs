using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAirborneState :IState
{

    
    public void Enter()
    {

    }


    public void Update()
    {
        if (PlayerNeededValues.IsGroundedPlayer)
        {
            
            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);
           
            return;
        }

        if (PlayerNeededValues.IsSpacePressing)
        {
            PlayerController.PlayerRB.velocity = new Vector2(8f * Math.Sign(PlayerNeededValues.MoveInput.x), PlayerNeededValues.JumpSpeed);
           // Debug.Log("azalma");
        }
        else
        {
            PlayerController.PlayerRB.velocity = new Vector2(8f * Math.Sign(PlayerNeededValues.MoveInput.x), PlayerController.PlayerRB.velocity.y);
            Debug.Log("korunma");
        }
        //Debug.Log("MoveInput Debug Display " + Math.Sign(PlayerNeededValues.MoveInput.x));
        PlayerController.ChangeAnimationState("Idle");

    }


    public void Exit()
    {

    }



}
