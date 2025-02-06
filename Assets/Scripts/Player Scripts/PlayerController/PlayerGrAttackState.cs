using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerGrAttackState : IState
{
    public void Enter()
    {
        CommandHandler.ResetNext();

    }

    public void Update()
    {
        if (!PlayerNeededValues.IsHeavyAttack && !PlayerNeededValues.IsLightAttack)
        {
            PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);
            return;
        }
        Debug.Log("In Heavy Attack ");
        if (PlayerNeededValues.IsHeavyAttack)
        {
            if (PlayerNeededValues.AttackNumber == 1)
            {
                Debug.Log("Playing Heavy Attack 1");
                PlayerController.ChangeAnimationState("HeavyAttack1");
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 3f, ForceMode2D.Impulse);
            }
            else if (PlayerNeededValues.AttackNumber == 2)
            {
                PlayerController.ChangeAnimationState("HeavyAttack2");
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 3f, ForceMode2D.Impulse);
            }
            else if (PlayerNeededValues.AttackNumber == 3)
            {
                PlayerController.ChangeAnimationState("HeavyAttack3");
                PlayerController.PlayerRB.AddForce(PlayerController.forward * 1.5f, ForceMode2D.Impulse);
            }
        }
       
    }


    public void Exit()
    {
        if (PlayerNeededValues.heavyAttackInput == CommandHandler.ShowNext())
        {
            PlayerNeededValues.IncreaseAttackNumber();
        }
        else
        {
            PlayerNeededValues.ResetAttackNumber();
        }
        if(PlayerNeededValues.AttackNumber == 4)
        {
            PlayerNeededValues.ResetAttackNumber();
        }
        CommandHandler.StartNext();
    }
}
