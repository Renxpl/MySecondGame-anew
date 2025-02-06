using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommandHandler 
{
    
    static ICommand next;
    
    public static  void HandleCommand(ICommand command)
    {
        if(PlayerNeededValues.IsRolling)
        {
            next = command;

        }
        else if (PlayerNeededValues.IsHeavyAttack)
        {
            next = command;
        }
        else if (PlayerNeededValues.IsLightAttack)
        {
            next = command;
        }
        else if (PlayerAirborneState.isAirborne)
        {
            Debug.Log("Airborne");
            next = command;
        }
        else
        {
            command?.Execute();
        }

    }

    public static void ResetNext()
    {
        next = null;
    }

    public static void StartNext()
    {
        next?.Execute();
        next= null;
    }



}
