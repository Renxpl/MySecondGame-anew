using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : IState
{
    
    
    public void Enter()
    {
         PlayerController.PlayerRB.velocity = new Vector2(0, 0);

    }

    public void Update()
    {


        



    }

    public void Exit() { }
}
