using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : IState
{
    Rigidbody2D playerRb;

    public void Enter()
    {
        //Debug.Log("WalkingStateStarted");
        playerRb = PlayerController.PlayerRB;

    }

    public void Update()
    {

        if (!(PlayerNeededValues.MoveInput.x > 0.75f || -0.75f > PlayerNeededValues.MoveInput.x)|| !PlayerNeededValues.IsGroundedPlayer)
        {

            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);

            return;

        }
        if (PlayerNeededValues.IsRolling || PlayerNeededValues.IsJumping)
        {

            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);

            return;


        }
        playerRb.velocity = new Vector2(15f * Mathf.Sign(PlayerNeededValues.MoveInput.x), playerRb.velocity.y);
        PlayerController.ChangeAnimationState("Running");
    }


    public void Exit()
    {

    }
}
