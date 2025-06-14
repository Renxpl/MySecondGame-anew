using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerGroundedState : IState
{
  
    
    public IState CurrentState { get; private set; }
    

    //goes states

    public void Enter()
    {

        //Debug.Log("Entered into Grounded State");

       
    }
    public void Update()
    {
        if (!(PlayerNeededValues.IsGroundedPlayer))
        {
            //Debug.Log("Entered into Airborne State");
            PlayerController.playerSM.ChangeState(PlayerNeededValues.AirborneStateForPlayer);
            //Debug.Log("Entered into Airborne State");
            return;
        }
        PlayerAirborneState.isAirborne = false;

        if (PlayerNeededValues.IsRolling)
        {
           
            PlayerController.playerSM.ChangeState( CurrentState = PlayerNeededValues.RollStateForPlayer);


        }
        else if (PlayerNeededValues.IsKnocbacking)
        {
            PlayerController.playerSM.ChangeState(CurrentState = PlayerNeededValues.playerKbState);
        }
        
        else if (PlayerNeededValues.IsParrying)
        {
            PlayerController.playerSM.ChangeState(CurrentState = PlayerNeededValues.playerParryState);
        }

        else if (PlayerNeededValues.IsLightAttack || PlayerNeededValues.IsHeavyAttack || PlayerNeededValues.IsSpecialAttack)
        {
            
            PlayerController.playerSM.ChangeState(CurrentState = PlayerNeededValues.GrAttackState);
        }

       

        else if (PlayerNeededValues.IsJumping)
        {
            PlayerController.playerSM.ChangeState(CurrentState = PlayerNeededValues.JumpStateForPlayer);
        }


        else if ((PlayerNeededValues.MoveInput.x > 0.75f || -0.75f > PlayerNeededValues.MoveInput.x)&& !PlayerNeededValues.StopEverythingPlayer)
        {
            PlayerController.playerSM.ChangeState(CurrentState = PlayerNeededValues.RunStateForPlayer);



        }

        else if ((PlayerNeededValues.MoveInput.x > 0.15f || -0.15f > PlayerNeededValues.MoveInput.x) && !PlayerNeededValues.StopEverythingPlayer)
        {
           
            PlayerController.playerSM.ChangeState( CurrentState = PlayerNeededValues.WalkStateForPlayer);
           

        }

        else
        {
            
            PlayerController.playerSM.ChangeState(CurrentState = PlayerNeededValues.IdleStateForPlayer);
           

        }




        






    }

    public void Exit()
    {



    }



}
