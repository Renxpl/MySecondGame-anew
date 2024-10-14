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
           
            return;
        }


        PlayerController.PlayerRB.velocity = new Vector2(8f* Math.Sign(PlayerNeededValues.MoveInput.x), PlayerController.PlayerRB.velocity.y);
        Debug.Log("MoveInput Debug Display " + Math.Sign(PlayerNeededValues.MoveInput.x));
        PlayerController.ChangeAnimationState("Idle");
    }


    public void Exit()
    {

    }



}
