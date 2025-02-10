using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerGrAttackState : IState
{

    public static bool sw = false;
    bool permissionforLA = false;
    bool permissionforHA = false;
    float factor = 1f;
    public void Enter()
    {
        CommandHandler.ResetNext();

    }

    public void Update()
    {
        if (!PlayerNeededValues.IsHeavyAttack && !PlayerNeededValues.IsLightAttack && !PlayerNeededValues.IsSpecialAttack)
        {
            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);
            return;
        }
        if(Time.timeScale < 1)
        {
            factor = 1.5f;
        }
        else
        {
            factor = 1f;
        }
        //Debug.Log("In Heavy Attack ");

        if (PlayerNeededValues.IsSpecialAttack && !sw)
        {
            Debug.Log("Special Attack");
            PlayerNeededValues.DecreaseStamina(15);
            sw = true;
            permissionforLA = false; permissionforHA = false;
        }

        else if (PlayerNeededValues.IsHeavyAttack && !sw)
        {
            if (PlayerNeededValues.LightAttackNumber == 2 && PlayerNeededValues.AttackNumber == 2)
            {
                PlayerNeededValues.IncreaseAttackNumber(0);
            }
            if (PlayerNeededValues.AttackNumber == 1)
            {
                //Debug.Log("Playing Heavy Attack 1");
                PlayerController.ChangeAnimationState("HeavyAttack1");
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 45f*factor, ForceMode2D.Impulse);

            }
            else if (PlayerNeededValues.AttackNumber == 2)
            {
                PlayerController.ChangeAnimationState("HeavyAttack2");
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 80f*factor, ForceMode2D.Impulse);
            }
            else if (PlayerNeededValues.AttackNumber >= 3)
            {
                PlayerController.ChangeAnimationState("HeavyAttack3");
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 60f*factor, ForceMode2D.Impulse);
            }
            PlayerNeededValues.DecreaseStamina(5);
            sw = true;
            PlayerNeededValues.IncreaseAttackNumber(0);
            permissionforLA = true; permissionforHA = false;
            //Debug.Log(PlayerNeededValues.LightAttackNumber);
            //Debug.Log(PlayerNeededValues.Stamina);
        }


        else if (PlayerNeededValues.IsLightAttack && !sw)
        {
            if (PlayerNeededValues.LightAttackNumber == 1)
            {
                //Debug.Log("Playing Light Attack 1");
                PlayerController.ChangeAnimationState("LightAttack1");
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 25f * factor, ForceMode2D.Impulse);
            }
            else if (PlayerNeededValues.LightAttackNumber == 2)
            {
                PlayerController.ChangeAnimationState("LightAttack2");
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 25f * factor, ForceMode2D.Impulse);
            }
            else if (PlayerNeededValues.LightAttackNumber == 3)
            {
                PlayerController.ChangeAnimationState("LightAttack3");
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 25f * factor, ForceMode2D.Impulse);
            }
            else if (PlayerNeededValues.LightAttackNumber == 4)
            {
                PlayerController.ChangeAnimationState("LightAttack4");
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 25f * factor, ForceMode2D.Impulse);
            }
            else if (PlayerNeededValues.LightAttackNumber >= 5)
            {
                PlayerController.ChangeAnimationState("LightAttack5");
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 150f * factor, ForceMode2D.Impulse);
            }
            PlayerNeededValues.DecreaseStamina(3);
            sw = true;
            PlayerNeededValues.IncreaseAttackNumber(1);
            permissionforLA = false; permissionforHA = true; 
            //Debug.Log(PlayerNeededValues.LightAttackNumber);

        }

    }


    public void Exit()
    {
        
        if (PlayerNeededValues.heavyAttackInput == CommandHandler.ShowNext())
        {
            if(permissionforHA) PlayerNeededValues.AdjustAttackNumber(0);
            if (PlayerNeededValues.Stamina < 5)
            {
                PlayerNeededValues.ResetAttackNumber(0);
                PlayerNeededValues.ResetStamina();
            }

        }
        else if (PlayerNeededValues.lightAttackInput == CommandHandler.ShowNext())
        {
            if(permissionforLA) PlayerNeededValues.AdjustAttackNumber(1);
            if (PlayerNeededValues.Stamina < 3)
            {
                PlayerNeededValues.ResetAttackNumber(1);
                PlayerNeededValues.ResetStamina();

            }

        }
        else
        {
            PlayerNeededValues.ResetAttackNumber(0);
            PlayerNeededValues.ResetAttackNumber(1);
            PlayerNeededValues.ResetStamina();
        }
        
        
        
        CommandHandler.StartNext();
    }
}
