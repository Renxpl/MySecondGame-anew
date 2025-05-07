using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEdgeClimbState : IState
{
    public void Enter()
    {
        PlayerNeededValues.Gravity0 = true;
        

    }


    public void Update()
    {
       
       
        

        if (PlayerNeededValues.IsJumping)
        {
            PlayerController.playerSM.ChangeState(PlayerNeededValues.JumpStateForPlayer);


        }


        PlayerController.ChangeAnimationState("EdgeClimb");

        PlayerController.PlayerRB.velocity = new Vector2(0f, 0f);

        //PlayerController.PlayerRB.AddForce(PlayerNeededValues.JumpSpeed * Vector2.up, ForceMode2D.Impulse);

        


    }


    public void Exit()
    {
        PlayerNeededValues.Gravity0 = false;
    }
}
