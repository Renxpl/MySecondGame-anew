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
        else if (PlayerNeededValues.IsParrying)
        {
            if(PlayerNeededValues.IsRolling) command?.Execute();
            else next = command;
        }


        else if (PlayerNeededValues.IsSpecialAttack)
        {
            if (PlayerNeededValues.IsRolling && !PlayerNeededValues.IsKnocbacking && !PlayerNeededValues.IsDuringAttack && PlayerNeededValues.IsParrying)
            {
                command?.Execute();
            }
            else next = command;
        }
        else if (PlayerNeededValues.IsHeavyAttack)
        {
            if (PlayerNeededValues.IsRolling && !PlayerNeededValues.IsKnocbacking && !PlayerNeededValues.IsDuringAttack && PlayerNeededValues.IsParrying)
            {
                command?.Execute();
            }
            else next = command;
        }
        else if (PlayerNeededValues.IsLightAttack)
        {
            if (PlayerNeededValues.IsRolling && !PlayerNeededValues.IsKnocbacking && !PlayerNeededValues.IsDuringAttack && PlayerNeededValues.IsParrying)
            {
                command?.Execute();
            }
            else next = command;
        }
        else if (PlayerNeededValues.IsAirborneAttack)
        {


            next = command;


        }
        else if (PlayerAirborneState.isAirborne)
        {
            //Debug.Log("Airborne");
            if (PlayerNeededValues.isRollingAirborne || PlayerNeededValues.AAInit && !PlayerNeededValues.IsKnocbacking || PlayerNeededValues.IsRightWallClimbing || PlayerNeededValues.IsLeftWallClimbing)
            {
                command?.Execute();
            }
            
            else next = command;
        }



        else
        {
            //Debug.Log("1");
            if(!PlayerNeededValues.IsKnocbacking)
            command?.Execute();
        }

    }

    public static void ResetNext()
    {
        next = null;
    }

    public static void StartNext()
    {
        if(next != PlayerNeededValues.parryInp)
        next?.Execute();
        next= null;
    }

    public static ICommand ShowNext()
    {
        return next;
    }


}
