using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : IState
{
    Rigidbody2D playerRb;
    float rollingForceFactor = 25f;
    float timePassed = 0;
    float extraFactor = 1.5f;
    int counter;
    public void Enter()
    {
        //Debug.Log("WalkingStateStarted");
        playerRb = PlayerController.PlayerRB;
        CommandHandler.ResetNext();
        timePassed = 0;
        counter = 0;
        if (!PlayerNeededValues.IsGroundedPlayer)
        {
            PlayerNeededValues.Gravity0 = true;
        }
    }


    public void Update()
    {

        if (!PlayerNeededValues.IsRolling)
        {
            if (PlayerNeededValues.IsGroundedPlayer)
            {
                PlayerController.playerSM.ChangeState(PlayerNeededValues.GroundedStateForPlayer);
                return;

            }


            else
            {
                PlayerController.playerSM.ChangeState(PlayerNeededValues.AirborneStateForPlayer);
                return;
            }

        }


        timePassed += Time.deltaTime;
        if (timePassed < 0.083f)
        {
            //rollingForceFactor = 10f * extraFactor;
            rollingForceFactor = 10f;
        }
        else
        {
           // rollingForceFactor = 20f * extraFactor;
            rollingForceFactor = 20f;
        }
        
        if(counter > 0)
        {
            return;
        }

        if (Time.timeScale < 1)
        {
            //playerRb.velocity = new Vector2(30f * 1.5f * playerRb.gameObject.transform.localScale.x, playerRb.velocity.y);
            if(PlayerNeededValues.RollInput.x != 0)
            {
               
               if(timePassed < 0.1f) playerRb.velocity = new Vector2(rollingForceFactor * 5.25f * Mathf.Sign(PlayerNeededValues.RollInput.x), 0f);
                else
                {
                    counter++;
                    PlayerController.PlayerRB.MovePosition(new Vector2(PlayerController.PlayerRB.transform.position.x + Mathf.Sign(PlayerController.forward.x) * 2, PlayerController.PlayerRB.transform.position.y));
                }
            }
            else
            {
                //3.25 in old version
                if (timePassed < 0.1f) playerRb.velocity = new Vector2(rollingForceFactor * 5.25f * Mathf.Sign(playerRb.gameObject.transform.localScale.x), 0f);
                else
                {
                    counter++;
                    PlayerController.PlayerRB.MovePosition(new Vector2(PlayerController.PlayerRB.transform.position.x + Mathf.Sign(PlayerController.forward.x) * 2, PlayerController.PlayerRB.transform.position.y));
                }

            }
        }
        else
        {
            
            //playerRb.velocity = new Vector2(30f * playerRb.gameObject.transform.localScale.x, playerRb.velocity.y);
            if (PlayerNeededValues.RollInput.x != 0)
            {

                if (timePassed < 0.1f)playerRb.velocity = new Vector2(rollingForceFactor * Mathf.Sign(PlayerNeededValues.RollInput.x), 0f);
                 else
                {
                    counter++;
                    PlayerController.PlayerRB.MovePosition(new Vector2(PlayerController.PlayerRB.transform.position.x + Mathf.Sign(PlayerController.forward.x) * 2, PlayerController.PlayerRB.transform.position.y));
                }
            }
            else
            {

                if (timePassed < 0.1f)  playerRb.velocity = new Vector2(rollingForceFactor * Mathf.Sign(playerRb.gameObject.transform.localScale.x), 0f);
                else
                {
                    counter++;
                    PlayerController.PlayerRB.MovePosition(new Vector2(PlayerController.PlayerRB.transform.position.x + Mathf.Sign(PlayerController.forward.x) * 2, PlayerController.PlayerRB.transform.position.y));
                }
            }
        }
        PlayerController.ChangeAnimationState("Rolling");

    }


    public void Exit()
    {
        PlayerNeededValues.Gravity0 = false;
        CommandHandler.StartNext();
    }
}
