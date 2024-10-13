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
       /* if (!(PlayerNeededValues.IsGroundedPlayer))
        {
            Debug.Log("Entered into Airborne State");
            PlayerController.playerSM.ChangeState(PlayerNeededValues.AirborneStateForPlayer);
            Debug.Log("Entered into Airborne State");
            return;
        }
        Debug.Log("Grounded State Update finished");**/
        if (PlayerNeededValues.MoveInput.x > 0.75f || -0.75f > PlayerNeededValues.MoveInput.x)
        {
            //Debug.Log("Entered into Walking State");
            PlayerController.playerSM.ChangeState(CurrentState = PlayerNeededValues.WalkStateForPlayer);
            //Debug.Log("Entered into Walking State");

        }



    }

    public void Exit()
    {



    }



}
