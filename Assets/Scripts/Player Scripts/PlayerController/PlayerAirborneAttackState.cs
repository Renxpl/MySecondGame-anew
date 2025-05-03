using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerAirborneAttackState : IState
{


   
    
  
    public void Enter()
    {
        
        PlayerNeededValues.ResetAA = false;

    }


    public void Update()
    {

        if (PlayerNeededValues.IsGroundedPlayer)
        {
            PlayerController.PlayerRB.velocity = Vector2.zero;
            PlayerNeededValues.IsAirborneAttack = false;
            PlayerNeededValues.SwitchAACollider = true;
            PlayerController.playerSM.ChangeState(PlayerNeededValues.AirborneStateForPlayer);


            return;

        }
        else if (!PlayerNeededValues.IsAirborneAttack)
        {
            PlayerController.playerSM.ChangeState(PlayerNeededValues.AirborneStateForPlayer);
        }



        //PlayerController.PlayerRB.velocity = new Vector2(PlayerController.PlayerRB.velocity.x, -20f);
        
        PlayerController.PlayerRB.velocity = new Vector2(0f, -0.3f);
        




        //Debug.Log("MoveInput Debug Display " + Math.Sign(PlayerNeededValues.MoveInput.x));


    }


    public void Exit()
    {
        PlayerNeededValues.ResetAA = true;
        CommandHandler.StartNext();
        

    }
}
