using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : IState
{
    
    public void Enter()
    {
        PlayerNeededValues.Gravity0 = true;
        PlayerNeededValues.TimePassedOnWalls = 0f;
        
    }


    public void Update()
    {
        if (!PlayerNeededValues.IsLeftWallClimbing && !PlayerNeededValues.IsRightWallClimbing)
        {
            //Debug.Log("Entered into Airborne State");

           
            PlayerController.playerSM.ChangeState(PlayerNeededValues.AirborneStateForPlayer);





            //Debug.Log("Entered into Airborne State");


            return;
        }

        

        if (PlayerNeededValues.IsJumping)
        {
         
            if (PlayerNeededValues.IsRightWallClimbing && PlayerNeededValues.CanJumpFromRWall && PlayerNeededValues.TimePassedOnWalls >0.1f)
            {
                //Debug.Log("Entered, WCState");
                PlayerNeededValues.CanJumpFromRWall = false;
                PlayerController.playerSM.ChangeState(PlayerNeededValues.JumpStateForPlayer);
                PlayerController.PlayerRB.transform.localScale = new Vector2(-1f,1f);
               // PlayerController.PlayerRB.velocity = new Vector2(PlayerController.PlayerRB.velocity.x, -0.5f);
            }
            else if (PlayerNeededValues.IsLeftWallClimbing && PlayerNeededValues.CanJumpFromLWall && PlayerNeededValues.TimePassedOnWalls > 0.1f)
            {
                //Debug.Log("Entered, WCState");
                PlayerNeededValues.CanJumpFromLWall= false;
                PlayerController.playerSM.ChangeState(PlayerNeededValues.JumpStateForPlayer);
                PlayerController.PlayerRB.transform.localScale = new Vector2(1f, 1f);
               // PlayerController.PlayerRB.velocity = new Vector2(PlayerController.PlayerRB.velocity.x, -0.5f);
            }


        }
        else
        {
            Debug.Log("Error, WCState");
        }
        //  Debug.Log("Entered, WCState");
        PlayerController.PlayerRB.velocity = new Vector2(0f, -0.5f);

        //PlayerController.PlayerRB.AddForce(PlayerNeededValues.JumpSpeed * Vector2.up, ForceMode2D.Impulse);

        if (PlayerNeededValues.IsRightWallClimbing)
        {
            if(PlayerController.PlayerRB.transform.localScale.x ==1)
                PlayerController.ChangeAnimationState("WR1");
            else
                PlayerController.ChangeAnimationState("WL1");
        }
        else if (PlayerNeededValues.IsLeftWallClimbing)
        {
            if (PlayerController.PlayerRB.transform.localScale.x == 1)
                PlayerController.ChangeAnimationState("WL1");
            else
                PlayerController.ChangeAnimationState("WR1");
        }

        PlayerNeededValues.TimePassedOnWalls += Time.deltaTime;


    }


    public void Exit()
    {
        PlayerNeededValues.Gravity0 = false;
    }

}
