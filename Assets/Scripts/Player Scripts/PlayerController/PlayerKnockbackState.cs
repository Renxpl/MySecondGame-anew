using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockbackState : IState
{
    float backward;
    public void Enter()
    {
        backward = PlayerController.PlayerRB.gameObject.transform.localScale.x* -1f;
    }


    public void Update()
    {
        if (!PlayerNeededValues.IsKnocbacking)
        {
            if (PlayerNeededValues.IsGroundedPlayer) PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);
            else PlayerController.playerSM.ChangeState(PlayerNeededValues.AirborneStateForPlayer);
            return;
        }
        if (Time.timeScale < 1)
        {
            PlayerController.PlayerRB.velocity = new Vector2(backward* 5f * 1.5f, PlayerController.PlayerRB.velocity.y);
        }
        else
        {
            PlayerController.PlayerRB.velocity = new Vector2(backward * 5f, PlayerController.PlayerRB.velocity.y);
           
        }
        
        PlayerController.ChangeAnimationState("Idle");

    }


    public void Exit()
    {

        CommandHandler.StartNext();

    }
}
