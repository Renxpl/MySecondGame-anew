using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborneState :IState
{
    int a = 0;
    public void Enter()
    {

    }


    public void Update()
    {
        if (PlayerNeededValues.IsGroundedPlayer)
        {
            
            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);
           
            return;
        }



    }


    public void Exit()
    {

    }



}
