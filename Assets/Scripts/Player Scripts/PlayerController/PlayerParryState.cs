using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryState : IState
{
    public void Enter()
    {

    }


    public void Update()
    {
        if (!PlayerNeededValues.IsParrying)
        {
            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);
          
            return;
        }
       if(PlayerNeededValues.IsRolling || PlayerNeededValues.IsKnocbacking)
        {
            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);

            return;
        }

        PlayerController.ChangeAnimationState("Idle");


    }


    public void Exit()
    {

       

    }
}
