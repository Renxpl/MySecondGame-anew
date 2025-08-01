using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerGrAttackState : IState
{

    public static bool sw = false;
    public static bool isLeaping = false; 
    bool permissionforLA = false;
    bool permissionforHA = false;
    float factor = 1f;
    public void Enter()
    {
        CommandHandler.ResetNext();

    }

    public void Update()
    {

        if ((!PlayerNeededValues.IsHeavyAttack && !PlayerNeededValues.IsLightAttack && !PlayerNeededValues.IsSpecialAttack) || (CommandHandler.ShowNext() == PlayerNeededValues.rollInput  && !PlayerNeededValues.IsSpecialAttack && !PlayerNeededValues.IsDuringAttack) || PlayerNeededValues.IsKnocbacking || PlayerNeededValues.IsParrying)
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
            //Debug.Log("Special Attack");
            
            sw = true;
            PlayerController.ChangeAnimationState("SpecialAttack");
            permissionforLA = false; permissionforHA = false;
        }

        else if (PlayerNeededValues.IsHeavyAttack && !sw)
        {
           
            if (PlayerNeededValues.AttackNumber == 1)
            {
                //Debug.Log("Playing Heavy Attack 1");
                PlayerController.ChangeAnimationState("HeavyAttack1");
                //PlayerController.PlayerRB.AddForce(PlayerController.forward * 45f*factor, ForceMode2D.Impulse);

            }
            else if (PlayerNeededValues.AttackNumber == 2)
            {
                PlayerController.ChangeAnimationState("HeavyAttack2");
                //PlayerController.PlayerRB.AddForce(PlayerController.forward * 300f * (1 / Time.timeScale) * factor, ForceMode2D.Impulse);
               
            }
            else if (PlayerNeededValues.AttackNumber >= 3)
            {
                PlayerController.ChangeAnimationState("HeavyAttack3");
                //PlayerController.PlayerRB.AddForce(PlayerController.forward * 300f*factor, ForceMode2D.Impulse);
                //PlayerController.PlayerRB.MovePosition(new Vector2(PlayerController.PlayerRB.transform.position.x + Mathf.Sign(PlayerController.forward.x) * 9, PlayerController.PlayerRB.transform.position.y));
            }
            sw = true;
            permissionforLA = true; permissionforHA = false;
            //Debug.Log(PlayerNeededValues.LightAttackNumber);
            //Debug.Log(PlayerNeededValues.Stamina);
        }


        else if (PlayerNeededValues.IsLightAttack && !sw)
        {
            
            if (PlayerNeededValues.LightAttackNumber == 1)
            {
                //Debug.Log("Playing Light Attack 1");
                if (!PlayerNeededValues.StopForTheWay)
                {

                    if (PlayerNeededValues.ComboCounter < 10)
                        PlayerController.ChangeAnimationState("LightAttack1_1");
                    else if (PlayerNeededValues.ComboCounter < 20)
                        PlayerController.ChangeAnimationState("LightAttack1_2");
                    else
                        PlayerController.ChangeAnimationState("LightAttack1_3");


                }
                else PlayerController.ChangeAnimationState("RA1");
                //PlayerController.PlayerRB.AddForce(PlayerController.forward * 15f * (1/Time.timeScale)* factor, ForceMode2D.Impulse);
            }
            else if (PlayerNeededValues.LightAttackNumber == 2)
            {
                if (!PlayerNeededValues.StopForTheWay)
                {
                    if(PlayerNeededValues.ComboCounter < 10)
                    PlayerController.ChangeAnimationState("LightAttack2_1");

                    else if (PlayerNeededValues.ComboCounter < 20)
                        PlayerController.ChangeAnimationState("LightAttack2_2");
                    else
                        PlayerController.ChangeAnimationState("LightAttack2_3");

                }
                else
                {
                    PlayerController.ChangeAnimationState("RA2");

                }
                //PlayerController.PlayerRB.AddForce(PlayerController.forward * 15f * (1 / Time.timeScale) * factor, ForceMode2D.Impulse);
            }
            else if (PlayerNeededValues.LightAttackNumber == 3)
            {
                if (PlayerNeededValues.ComboCounter < 10)
                    PlayerController.ChangeAnimationState("LightAttack3_1");
                else if (PlayerNeededValues.ComboCounter < 20)
                    PlayerController.ChangeAnimationState("LightAttack3_2");
                else
                    PlayerController.ChangeAnimationState("LightAttack3_3");
                //PlayerController.PlayerRB.AddForce(PlayerController.forward * 15f * (1 / Time.timeScale) * factor, ForceMode2D.Impulse);
            }
            else if (PlayerNeededValues.LightAttackNumber == 4)
            {
                PlayerController.ChangeAnimationState("LightAttack4");
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 25f * (1 / Time.timeScale) * factor, ForceMode2D.Impulse);
            }
            else if (PlayerNeededValues.LightAttackNumber == 5)
            {
                PlayerController.ChangeAnimationState("LightAttack5");
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 50f * (1 / Time.timeScale) * factor, ForceMode2D.Impulse);
                
            }
            //PlayerNeededValues.DecreaseStamina(3);
            sw = true;
            //PlayerNeededValues.IncreaseAttackNumber(1);
            permissionforLA = false; permissionforHA = true; 
            //Debug.Log(PlayerNeededValues.LightAttackNumber);

        }
        if(PlayerNeededValues.IsHeavyAttack && isLeaping)
        {
            if (PlayerNeededValues.AttackNumber == 2)
            {
                //Debug.Log("Playing Heavy Attack 1");
                
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 350f * factor, ForceMode2D.Impulse);
               

            }
            isLeaping = false;

        }

    }


    public void Exit()
    {
        
        if (PlayerNeededValues.heavyAttackInput == CommandHandler.ShowNext())
        {
            if (PlayerNeededValues.Stamina < 5)
            {
                PlayerNeededValues.ResetAttackNumber(0);
                PlayerNeededValues.ResetAttackNumber(1);
                PlayerNeededValues.ResetStamina();
            }
            

        }
        else if (PlayerNeededValues.lightAttackInput == CommandHandler.ShowNext())
        {
            
            if (PlayerNeededValues.Stamina < 3)
            {
                PlayerNeededValues.ResetAttackNumber(0);
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
