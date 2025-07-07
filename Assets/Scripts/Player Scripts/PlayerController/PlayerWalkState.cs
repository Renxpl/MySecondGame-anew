using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : IState
{
    Rigidbody2D playerRb;
    float walkingSpeed = 2.5f;
    public void Enter()
    {
        //Debug.Log("WalkingStateStarted");
         playerRb = PlayerController.PlayerRB;

    }

    public void Update()
    {
        if (!(PlayerNeededValues.MoveInput.x > 0.15f || -0.15f > PlayerNeededValues.MoveInput.x)|| ((PlayerNeededValues.MoveInput.x > 0.75f || -0.75f > PlayerNeededValues.MoveInput.x) && !PlayerNeededValues.DigginScene) || !PlayerNeededValues.IsGroundedPlayer || (PlayerNeededValues.StopEverythingPlayer && !PlayerNeededValues.StopForTheWay && !PlayerNeededValues.DigginScene))
        {

            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);

            return;

        }
        if (PlayerNeededValues.IsRolling || PlayerNeededValues.IsJumping || PlayerNeededValues.IsHeavyAttack || PlayerNeededValues.IsLightAttack || PlayerNeededValues.IsSpecialAttack || PlayerNeededValues.IsKnocbacking || PlayerNeededValues.IsParrying || PlayerNeededValues.IsDigging)
        {

            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);

            return;


        }

        //Debug.Log("is Walking now");
        if (Time.timeScale < 1)
        {
            playerRb.velocity = new Vector2(walkingSpeed * 3f * Mathf.Sign(PlayerNeededValues.MoveInput.x), playerRb.velocity.y);
        }
        else
        {
            playerRb.velocity = new Vector2(walkingSpeed * Mathf.Sign(PlayerNeededValues.MoveInput.x), playerRb.velocity.y);
        }

        PlayerController.ChangeAnimationState("Walking");

    }

    public void Exit() { }
}
