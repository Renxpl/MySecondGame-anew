using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborneAttackState : IState
{
   
    public void Enter()
    {

    }


    public void Update()
    {

        if (PlayerNeededValues.IsGroundedPlayer)
        {
            PlayerController.PlayerRB.velocity = Vector2.zero;
            PlayerNeededValues.IsAirborneAttack = false;
            PlayerNeededValues.SwitchAACollider = true;
            PlayerController.playerSM.ChangeState(PlayerNeededValues.AirborneStateForPlayer);


            return;

        }
        PlayerController.PlayerRB.velocity = new Vector2(PlayerController.PlayerRB.velocity.x, -20f);
        PlayerController.ChangeAnimationState("AirborneAttack");
        //Enable AirborneAttackCollider During this state


        //Debug.Log("MoveInput Debug Display " + Math.Sign(PlayerNeededValues.MoveInput.x));
      

    }


    public void Exit()
    {

        CommandHandler.StartNext();

    }
}
