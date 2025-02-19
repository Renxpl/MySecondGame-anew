using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : IState
{
    Rigidbody2D playerRb;

    public void Enter()
    {
        //Debug.Log("WalkingStateStarted");
        playerRb = PlayerController.PlayerRB;
        CommandHandler.ResetNext();
    }


    public void Update()
    {
        if (!PlayerNeededValues.IsRolling)
        {
            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);
            return;
        }
        if(Time.timeScale < 1)
        {
            //playerRb.velocity = new Vector2(30f * 1.5f * playerRb.gameObject.transform.localScale.x, playerRb.velocity.y);
            playerRb.velocity = new Vector2(30f * 1.5f * PlayerNeededValues.RollInput.x, playerRb.velocity.y);
            if(PlayerNeededValues.RollInput.x != 0)
            {
                playerRb.velocity = new Vector2(30f * 1.5f * Mathf.Sign(PlayerNeededValues.RollInput.x), playerRb.velocity.y);
            }
            else
            {
                playerRb.velocity = new Vector2(30f * 1.5f * Mathf.Sign(playerRb.gameObject.transform.localScale.x), playerRb.velocity.y);
            }
        }
        else
        {
            //playerRb.velocity = new Vector2(30f * playerRb.gameObject.transform.localScale.x, playerRb.velocity.y);
            if (PlayerNeededValues.RollInput.x != 0)
            {
                playerRb.velocity = new Vector2(30f * Mathf.Sign(PlayerNeededValues.RollInput.x), playerRb.velocity.y);
            }
            else
            {
                playerRb.velocity = new Vector2(30f * Mathf.Sign(playerRb.gameObject.transform.localScale.x), playerRb.velocity.y);
            }
        }
        PlayerController.ChangeAnimationState("Rolling");

    }


    public void Exit()
    {
        CommandHandler.StartNext();
    }
}
