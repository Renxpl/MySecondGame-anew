using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerGroundedState : IState
{
  
    
    public IState CurrentState { get; private set; }


    //goes states

    public void Enter()
    {



        PlayerController.playerSM.ChangeState(this);

    }

    public void Update()
    {
        




    }

    public void Exit()
    {



    }



}
