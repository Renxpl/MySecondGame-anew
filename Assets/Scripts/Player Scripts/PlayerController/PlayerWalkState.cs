using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : IState
{
    Rigidbody2D playerRb;
    
    public void Enter()
    {
        //Debug.Log("WalkingStateStarted");
         playerRb = PlayerController.PlayerRB;

    }

    public void Update()
    {
        if (!(PlayerNeededValues.MoveInput.x > 0.15f || -0.15f > PlayerNeededValues.MoveInput.x)|| ((PlayerNeededValues.MoveInput.x > 0.75f || -0.75f > PlayerNeededValues.MoveInput.x)))
        {

            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);

            return;

        }
        if (PlayerNeededValues.IsRolling)
        {

            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);

            return;


        }

        //Debug.Log("is Walking now");
        playerRb.velocity = new Vector2(200f * Time.deltaTime,playerRb.velocity.y);



    }

    public void Exit() { }
}
