using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrAttackState : IState
{
    public void Enter()
    {
        CommandHandler.ResetNext();

    }

    public void Update()
    {
        if (PlayerNeededValues.IsHeavyAttack)
        {
            if (PlayerNeededValues.AttackNumber == 1)
            {
                PlayerController.ChangeAnimationState("HeavyAttack1");
            }
            else if (PlayerNeededValues.AttackNumber == 2)
            {
                PlayerController.ChangeAnimationState("HeavyAttack2");
            }
            else if (PlayerNeededValues.AttackNumber == 3)
            {
                PlayerController.ChangeAnimationState("HeavyAttack3");
            }
        }
       
    }


    public void Exit()
    {
        if (PlayerNeededValues.heavyAttackInput == CommandHandler.ShowNext())
        {
            PlayerNeededValues.IncreaseAttackNumber();
        } 
        CommandHandler.StartNext();
    }
}
