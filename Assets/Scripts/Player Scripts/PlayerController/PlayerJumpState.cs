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
        if (!(PlayerNeededValues.IsGroundedPlayer) && (!PlayerNeededValues.IsRightWallClimbing && !PlayerNeededValues.IsLeftWallClimbing || PlayerNeededValues.TimePassedOnWalls <= 0.1f))
        {
            //Debug.Log("Entered into Airborne State");


            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);


           


            //Debug.Log("Entered into Airborne State");




            return;
        }
       // Debug.Log("debg");
        if (PlayerNeededValues.IsGroundedPlayer || PlayerNeededValues.IsGroundedPlayerDebug)
        {
            if (Time.timeScale < 1)
            {
              if(Time.timeScale!=0)  PlayerController.PlayerRB.velocity = new Vector2(PlayerController.PlayerRB.velocity.x, PlayerNeededValues.JumpSpeed *  3f);
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
                if (PlayerNeededValues.IsRightWallClimbing && PlayerNeededValues.TimePassedOnWalls> 0.1f)
                {
                    //Debug.Log("Entered, RWJ");
                    PlayerController.PlayerRB.velocity = new Vector2(-60f*3f, 25f*3f);
                }
                if (PlayerNeededValues.IsLeftWallClimbing && PlayerNeededValues.TimePassedOnWalls > 0.1f)
                {
                    //Debug.Log("Entered, LWJ");
                    PlayerController.PlayerRB.velocity = new Vector2(60f*3f, 25f*3f);
                }


            }
            else
            {
               
                if (PlayerNeededValues.IsRightWallClimbing && PlayerNeededValues.TimePassedOnWalls > 0.1f)
                {
                    //Debug.Log("Entered, RWJ");
                    PlayerController.PlayerRB.velocity = new Vector2( -60f, 25f);
                }
                if (PlayerNeededValues.IsLeftWallClimbing && PlayerNeededValues.TimePassedOnWalls > 0.1f)
                {
                    //Debug.Log("Entered, LWJ");
                    PlayerController.PlayerRB.velocity = new Vector2(60f,25f) ;
                }
            }
        }
       
        //PlayerController.PlayerRB.AddForce(PlayerNeededValues.JumpSpeed * Vector2.up, ForceMode2D.Impulse);
        





    }


    public void Exit()
    {

    }




}
