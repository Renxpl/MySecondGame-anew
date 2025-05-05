using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : IState
{
    public void Enter()
    {
        PlayerNeededValues.Gravity0 = true;
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
            if (PlayerNeededValues.IsRightWallClimbing && PlayerNeededValues.CanJumpFromRWall)
            {
                PlayerNeededValues.CanJumpFromRWall = false;
                PlayerController.playerSM.ChangeState(PlayerNeededValues.JumpStateForPlayer);
            }
            else if (PlayerNeededValues.IsLeftWallClimbing && PlayerNeededValues.CanJumpFromLWall)
            {
                PlayerNeededValues.CanJumpFromLWall= false;
                PlayerController.playerSM.ChangeState(PlayerNeededValues.JumpStateForPlayer);
            }


        }
        else
        {
            Debug.Log("Error, WCState");
        }
        
        PlayerController.PlayerRB.velocity = new Vector2(PlayerController.PlayerRB.velocity.x, -1f);

        //PlayerController.PlayerRB.AddForce(PlayerNeededValues.JumpSpeed * Vector2.up, ForceMode2D.Impulse);






    }


    public void Exit()
    {
        PlayerNeededValues.Gravity0 = false;
    }

}
