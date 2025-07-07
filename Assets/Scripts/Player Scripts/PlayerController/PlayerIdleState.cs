using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState
{


    float timePassed;
    float timeToBePassed = 0.75f;
    public void Enter()
    {

    }


    public void Update()
    {
        //Going Back To GroundedState
        if (!PlayerNeededValues.IsGroundedPlayer || Mathf.Abs(PlayerNeededValues.MoveInput.x) > 0.15f ) 
        {

            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);
            timePassed = 0;

            return;

        }
        if (PlayerNeededValues.IsRolling || PlayerNeededValues.IsJumping || PlayerNeededValues.IsHeavyAttack||PlayerNeededValues.IsLightAttack || PlayerNeededValues.IsSpecialAttack || PlayerNeededValues.IsKnocbacking || PlayerNeededValues.IsParrying || PlayerNeededValues.IsDigging)
        {

            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);
            timePassed = 0;
            return;


        }

        timePassed += Time.deltaTime;

        PlayerController.PlayerRB.velocity = new Vector2(0f, 0f);

        if (timePassed < timeToBePassed)
        {
            PlayerController.ChangeAnimationState("Idle");
        }
        else
        {
            PlayerController.ChangeAnimationState("IdleAnim1");
            if (timePassed >= (timeToBePassed+ 0.83f))
            {
                timePassed = 0f;
            }
        }



    }


    public void Exit()
    { 

    }

}
