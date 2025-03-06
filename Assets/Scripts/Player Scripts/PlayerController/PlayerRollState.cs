using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : IState
{
    Rigidbody2D playerRb;
    float rollingForceFactor = 25f;
    float timePassed = 0;
    public void Enter()
    {
        //Debug.Log("WalkingStateStarted");
        playerRb = PlayerController.PlayerRB;
        CommandHandler.ResetNext();
        timePassed = 0;
    }


    public void Update()
    {

        if (!PlayerNeededValues.IsRolling)
        {
            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);
            return;
        }
        timePassed += Time.deltaTime;
        if (timePassed < 0.083f)
        {
            rollingForceFactor = 10f;
        }
        else
        {
            rollingForceFactor = 20f;
        }
        
        if (Time.timeScale < 1)
        {
            //playerRb.velocity = new Vector2(30f * 1.5f * playerRb.gameObject.transform.localScale.x, playerRb.velocity.y);
            if(PlayerNeededValues.RollInput.x != 0)
            {
                playerRb.velocity = new Vector2(rollingForceFactor * 3.25f * Mathf.Sign(PlayerNeededValues.RollInput.x), playerRb.velocity.y);
            }
            else
            {
                playerRb.velocity = new Vector2(rollingForceFactor * 3.25f * Mathf.Sign(playerRb.gameObject.transform.localScale.x), playerRb.velocity.y);
            }
        }
        else
        {
            //playerRb.velocity = new Vector2(30f * playerRb.gameObject.transform.localScale.x, playerRb.velocity.y);
            if (PlayerNeededValues.RollInput.x != 0)
            {
                playerRb.velocity = new Vector2(rollingForceFactor * Mathf.Sign(PlayerNeededValues.RollInput.x), playerRb.velocity.y);
            }
            else
            {
                playerRb.velocity = new Vector2(rollingForceFactor * Mathf.Sign(playerRb.gameObject.transform.localScale.x), playerRb.velocity.y);
            }
        }
        PlayerController.ChangeAnimationState("Rolling");

    }


    public void Exit()
    {
        
        CommandHandler.StartNext();
    }
}
