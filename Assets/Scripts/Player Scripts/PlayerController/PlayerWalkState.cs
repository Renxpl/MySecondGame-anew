using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : IState
{
    Rigidbody2D playerRb;
    
    public void Enter()
    {
        //Debug.Log("WalkingStateStarted");
         playerRb = PlayerController.PlayerRB;

    }

    public void Update()
    {

        //Debug.Log("is Walking now");
        playerRb.velocity = new Vector2(200f * Time.deltaTime,playerRb.velocity.y);



    }

    public void Exit() { }
}
