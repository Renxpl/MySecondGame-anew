using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAirborneState :IState
{
    int a = 0;

    public static bool isAirborne = false;
    float jumpSpeed = 7.5f;
    float slowedJumpSpeed = 22.5f;
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


       // Debug.Log("debg");


        if (PlayerNeededValues.IsRolling)
        {

            PlayerController.playerSM.ChangeState(PlayerNeededValues.RollStateForPlayer);
            PlayerNeededValues.isRollingAirborne = false;
            return;

        }

        else if (PlayerNeededValues.IsKnocbacking)
        {
            PlayerController.playerSM.ChangeState(PlayerNeededValues.playerKbState);
        }


        else if (PlayerNeededValues.IsAirborneAttack)
        {
            PlayerController.playerSM.ChangeState(PlayerNeededValues.playerAAstate);
            return;
        }

        else if (PlayerNeededValues.IsKnocbacking)
        {
            PlayerController.playerSM.ChangeState(PlayerNeededValues.playerKbState);
            return;
        }
        

        else if (PlayerNeededValues.IsLeftWallClimbing || PlayerNeededValues.IsRightWallClimbing)
        {

           
            PlayerController.playerSM.ChangeState(PlayerNeededValues.playerWCState);


            return;


        }

        if (!PlayerNeededValues.CanDoActionDuringJump)
        {
            if(!PlayerNeededValues.isRollingAirborne || ! PlayerNeededValues.AAInit)
            CommandHandler.ResetNext();
        }
        
        isAirborne = true;

        float speed = Time.timeScale<1 ? slowedJumpSpeed : jumpSpeed;


        if (PlayerNeededValues.IsJumpingUp)
        {
            //Debug.Log("debg");
            if ((PlayerNeededValues.isAtRightWall || (PlayerNeededValues.RightBlindPointDebug)) && PlayerNeededValues.MoveInput.x > 0)
            {
                PlayerController.PlayerRB.velocity = new Vector2(0f, PlayerNeededValues.JumpSpeed);
            }

            else if ((PlayerNeededValues.isAtLeftWall || PlayerNeededValues.LeftBlindPointDebug) && PlayerNeededValues.MoveInput.x < 0)
            {
                PlayerController.PlayerRB.velocity = new Vector2(0f, PlayerNeededValues.JumpSpeed);
            }
            else if (BossScene.beingThrown)
            {
                PlayerController.PlayerRB.velocity = new Vector2(PlayerController.PlayerRB.velocity.x, PlayerController.PlayerRB.velocity.y);
            }

            else if (PlayerNeededValues.LockSpriteDirection)
            {
                PlayerController.PlayerRB.velocity = new Vector2(speed * Math.Sign(PlayerController.PlayerRB.transform.localScale.x), PlayerNeededValues.JumpSpeed);
                //PlayerController.ChangeAnimationState("JumpingUp");
            }
            else
            {
                PlayerController.PlayerRB.velocity = new Vector2(speed * Math.Sign(PlayerNeededValues.MoveInput.x), PlayerNeededValues.JumpSpeed);
                //PlayerController.ChangeAnimationState("JumpingUp");
            }
            PlayerController.ChangeAnimationState("JumpingUp");
            if (a % 2 == 0 ) { a++;  }

        }
        else
        {
            //Debug.Log("debg");
            if ((PlayerNeededValues.isAtRightWall || PlayerNeededValues.RightBlindPointDebug) && PlayerNeededValues.MoveInput.x > 0)
            {
                PlayerController.PlayerRB.velocity = new Vector2(0f, PlayerController.PlayerRB.velocity.y);
            }
            else if ((PlayerNeededValues.isAtLeftWall || PlayerNeededValues.LeftBlindPointDebug) && PlayerNeededValues.MoveInput.x < 0)
            {
                PlayerController.PlayerRB.velocity = new Vector2(0f, PlayerController.PlayerRB.velocity.y);
            }
            else if (BossScene.beingThrown)
            {
                PlayerController.PlayerRB.velocity = new Vector2(PlayerController.PlayerRB.velocity.x, PlayerController.PlayerRB.velocity.y);
            }

            else if (PlayerNeededValues.LockSpriteDirection)
            {
                PlayerController.PlayerRB.velocity = new Vector2(speed * Math.Sign(PlayerController.PlayerRB.transform.localScale.x), PlayerController.PlayerRB.velocity.y);
            }
            else
            {
                PlayerController.PlayerRB.velocity = new Vector2(speed * Math.Sign(PlayerNeededValues.MoveInput.x), PlayerController.PlayerRB.velocity.y);
                //PlayerController.ChangeAnimationState("JumpingDown");
            }
            PlayerController.ChangeAnimationState("JumpingDown");

            if (a % 2 == 1 && !PlayerNeededValues.isRollingAirborne) { a++; CommandHandler.ResetNext(); }
            

        }
        //Debug.Log("MoveInput Debug Display " + Math.Sign(PlayerNeededValues.MoveInput.x));
        //PlayerController.ChangeAnimationState("Idle");

    }


    public void Exit()
    {
        
        CommandHandler.StartNext();

    }



}
