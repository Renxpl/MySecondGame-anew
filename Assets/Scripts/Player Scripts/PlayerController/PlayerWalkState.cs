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
        if (!(PlayerNeededValues.MoveInput.x > 0.15f || -0.15f > PlayerNeededValues.MoveInput.x)|| ((PlayerNeededValues.MoveInput.x > 0.75f || -0.75f > PlayerNeededValues.MoveInput.x)) || !PlayerNeededValues.IsGroundedPlayer)
        {

            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);

            return;

        }
        if (PlayerNeededValues.IsRolling || PlayerNeededValues.IsJumping || PlayerNeededValues.IsHeavyAttack || PlayerNeededValues.IsLightAttack)
        {

            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);

            return;


        }

        //Debug.Log("is Walking now");
        playerRb.velocity = new Vector2(3f * Mathf.Sign(PlayerNeededValues.MoveInput.x),playerRb.velocity.y);

        PlayerController.ChangeAnimationState("Walking");

    }

    public void Exit() { }
}
