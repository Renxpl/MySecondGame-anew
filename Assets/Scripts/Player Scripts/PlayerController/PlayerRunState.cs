using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : IState
{
    Rigidbody2D playerRb;
    float speed = 10f;
    public void Enter()
    {
        //Debug.Log("WalkingStateStarted");
        playerRb = PlayerController.PlayerRB;
    

    }

    public void Update()
    {

        if (!(PlayerNeededValues.MoveInput.x > 0.75f || -0.75f > PlayerNeededValues.MoveInput.x)|| !PlayerNeededValues.IsGroundedPlayer || (PlayerNeededValues.StopEverythingPlayer && !PlayerNeededValues.StopForTheWay && !PlayerNeededValues.DigginScene))
        {

            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);

            return;

        }
        if (PlayerNeededValues.IsRolling || PlayerNeededValues.IsJumping ||PlayerNeededValues.IsHeavyAttack||PlayerNeededValues.IsLightAttack || PlayerNeededValues.IsSpecialAttack || PlayerNeededValues.IsKnocbacking || PlayerNeededValues.IsParrying)
        {

            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);

            return;


        }
        if (!PlayerNeededValues.StopForTheWay)
        {


            if (Time.timeScale < 1)
            {
                playerRb.velocity = new Vector2(speed * 3f * Mathf.Sign(PlayerNeededValues.MoveInput.x), playerRb.velocity.y);
            }
            else
            {
                playerRb.velocity = new Vector2(speed * Mathf.Sign(PlayerNeededValues.MoveInput.x), playerRb.velocity.y);
            }

            PlayerController.ChangeAnimationState("Running");
        }

        else
        {
            playerRb.velocity = new Vector2(speed * 2f * Mathf.Sign(PlayerNeededValues.MoveInput.x), playerRb.velocity.y);
            PlayerController.ChangeAnimationState("Speedy Run");
        }




    }

   


    public void Exit()
    {

    }
}
