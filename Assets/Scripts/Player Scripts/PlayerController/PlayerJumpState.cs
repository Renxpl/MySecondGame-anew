using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IState
{
    



    public void Enter()
    {

    }


    public void Update()
    {
        if (!(PlayerNeededValues.IsGroundedPlayer))
        {
            //Debug.Log("Entered into Airborne State");


            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);


           


            //Debug.Log("Entered into Airborne State");




            return;
        }
        if (Time.timeScale < 1)
        {
            PlayerController.PlayerRB.velocity = new Vector2(PlayerController.PlayerRB.velocity.x, PlayerNeededValues.JumpSpeed * (1/Time.timeScale)*1.25f);
        }
        else 
        {
            PlayerController.PlayerRB.velocity = new Vector2(PlayerController.PlayerRB.velocity.x, PlayerNeededValues.JumpSpeed);
        }

        //PlayerController.PlayerRB.AddForce(PlayerNeededValues.JumpSpeed * Vector2.up, ForceMode2D.Impulse);
        





    }


    public void Exit()
    {

    }




}
