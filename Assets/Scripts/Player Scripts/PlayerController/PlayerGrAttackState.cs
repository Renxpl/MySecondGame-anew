using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerGrAttackState : IState
{

    bool sw = false;
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
        }

        else if (PlayerNeededValues.IsHeavyAttack && !sw)
        {
            if (PlayerNeededValues.AttackNumber == 1)
            {
                Debug.Log("Playing Heavy Attack 1");
                PlayerController.ChangeAnimationState("HeavyAttack1");
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 30f*factor, ForceMode2D.Impulse);

            }
            else if (PlayerNeededValues.AttackNumber == 2)
            {
                PlayerController.ChangeAnimationState("HeavyAttack2");
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 30f*factor, ForceMode2D.Impulse);
            }
            else if (PlayerNeededValues.AttackNumber >= 3)
            {
                PlayerController.ChangeAnimationState("HeavyAttack3");
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 15f*factor, ForceMode2D.Impulse);
            }
            PlayerNeededValues.DecreaseStamina(5);
            sw = true;
            PlayerNeededValues.IncreaseAttackNumber(0);
            //Debug.Log(PlayerNeededValues.LightAttackNumber);
            //Debug.Log(PlayerNeededValues.Stamina);
        }


        else if (PlayerNeededValues.IsLightAttack && !sw)
        {
            if (PlayerNeededValues.LightAttackNumber == 1)
            {
                Debug.Log("Playing Light Attack 1");
                PlayerController.ChangeAnimationState("LightAttack1");
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 15f * factor, ForceMode2D.Impulse);
            }
            else if (PlayerNeededValues.LightAttackNumber == 2)
            {
                PlayerController.ChangeAnimationState("LightAttack2");
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 15f * factor, ForceMode2D.Impulse);
            }
            else if (PlayerNeededValues.LightAttackNumber == 3)
            {
                PlayerController.ChangeAnimationState("LightAttack3");
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 15f * factor, ForceMode2D.Impulse);
            }
            else if (PlayerNeededValues.LightAttackNumber == 4)
            {
                PlayerController.ChangeAnimationState("LightAttack4");
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 15f * factor, ForceMode2D.Impulse);
            }
            else if (PlayerNeededValues.LightAttackNumber >= 5)
            {
                PlayerController.ChangeAnimationState("LightAttack5");
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 15f * factor, ForceMode2D.Impulse);
            }
            PlayerNeededValues.DecreaseStamina(3);
            sw = true;
            PlayerNeededValues.IncreaseAttackNumber(1);
            //Debug.Log(PlayerNeededValues.LightAttackNumber);

        }

    }


    public void Exit()
    {

        if (PlayerNeededValues.heavyAttackInput == CommandHandler.ShowNext())
        {
            PlayerNeededValues.AdjustAttackNumber(0);
           

        }
        else if (PlayerNeededValues.lightAttackInput == CommandHandler.ShowNext())
        {
            PlayerNeededValues.AdjustAttackNumber(1);

        }
        else
        {
            PlayerNeededValues.ResetAttackNumber(0);
            PlayerNeededValues.ResetAttackNumber(1);
            PlayerNeededValues.ResetStamina();
        }
        if(PlayerNeededValues.Stamina < 3)
        {
            PlayerNeededValues.ResetAttackNumber(0);
            PlayerNeededValues.ResetAttackNumber(1);
            PlayerNeededValues.ResetStamina();
        }
        sw= false;
        CommandHandler.StartNext();
    }
}
