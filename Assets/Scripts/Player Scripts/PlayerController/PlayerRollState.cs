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

    }


    public void Update()
    {
        if (!PlayerNeededValues.IsRolling)
        {
            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);
            return;
        }
        playerRb.velocity = new Vector2(50f * playerRb.gameObject.transform.localScale.x, playerRb.velocity.y);
        PlayerController.ChangeAnimationState("Rolling");
    }


    public void Exit()
    {

    }
}
