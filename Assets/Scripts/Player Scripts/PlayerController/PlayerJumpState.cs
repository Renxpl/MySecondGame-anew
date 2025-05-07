using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IState
{


    bool firstTime;

    public void Enter()
    {
        firstTime = true;
    }


    public void Update()
    {
        if (!(PlayerNeededValues.IsGroundedPlayer) && !PlayerNeededValues.IsRightWallClimbing && !PlayerNeededValues.IsLeftWallClimbing)
        {
            //Debug.Log("Entered into Airborne State");


            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);


           


            //Debug.Log("Entered into Airborne State");




            return;
        }
        if (PlayerNeededValues.IsGroundedPlayer)
        {
            if (Time.timeScale < 1)
            {
                PlayerController.PlayerRB.velocity = new Vector2(PlayerController.PlayerRB.velocity.x, PlayerNeededValues.JumpSpeed * (1 / Time.timeScale) * 1.25f);
            }
            else
            {
                PlayerController.PlayerRB.velocity = new Vector2(PlayerController.PlayerRB.velocity.x, PlayerNeededValues.JumpSpeed);
            }
        }
        
        else
        {
            PlayerNeededValues.LockSpriteDirection = true;
            if (Time.timeScale < 1)
            {
                if (PlayerNeededValues.IsRightWallClimbing)
                {
                    PlayerController.PlayerRB.velocity = new Vector2(-1f/4f*(PlayerNeededValues.JumpSpeed * (1 / Time.timeScale) * 1.25f), PlayerNeededValues.JumpSpeed * (1 / Time.timeScale) * 1.25f);
                }
                if (PlayerNeededValues.IsLeftWallClimbing)
                {
                    PlayerController.PlayerRB.velocity = new Vector2(1f / 4f * (PlayerNeededValues.JumpSpeed * (1 / Time.timeScale) * 1.25f), PlayerNeededValues.JumpSpeed * (1 / Time.timeScale) * 1.25f);
                }


            }
            else
            {
               
                if (PlayerNeededValues.IsRightWallClimbing)
                {
                    PlayerController.PlayerRB.velocity = new Vector2( -50f, 20f);
                }
                if (PlayerNeededValues.IsLeftWallClimbing)
                {
                    PlayerController.PlayerRB.velocity = new Vector2( 50f,20f) ;
                }
            }
        }
       
        //PlayerController.PlayerRB.AddForce(PlayerNeededValues.JumpSpeed * Vector2.up, ForceMode2D.Impulse);
        





    }


    public void Exit()
    {

    }




}
