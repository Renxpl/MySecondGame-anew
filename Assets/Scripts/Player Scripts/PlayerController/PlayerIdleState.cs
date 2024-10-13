using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState
{
    public void Enter()
    {

    }


    public void Update()
    {
        //Going Back To GroundedState
        if (PlayerNeededValues.IsRolling || !PlayerNeededValues.IsGroundedPlayer || PlayerNeededValues.MoveInput.x != 0 ) 
        {

            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);

            return;

        }
        PlayerController.PlayerRB.velocity = new Vector2(0f, 0f);


        PlayerController.ChangeAnimationState("Idle");

    }


    public void Exit()
    { 

    }

}
