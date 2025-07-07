using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDigginState : IState
{
    public void Enter()
    {

    }


    public void Update()
    {
      
       
        if (PlayerNeededValues.IsRolling || PlayerNeededValues.IsJumping || PlayerNeededValues.IsHeavyAttack || PlayerNeededValues.IsLightAttack || PlayerNeededValues.IsSpecialAttack || PlayerNeededValues.IsKnocbacking || PlayerNeededValues.IsParrying || !PlayerNeededValues.IsDigging)
        {

            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);

            return;


        }

        
            PlayerController.ChangeAnimationState("Diggin");
        
       



    }


    public void Exit()
    {

    }
}
