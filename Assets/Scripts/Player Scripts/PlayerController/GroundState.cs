using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class GroundState : IState
{
    public IState CurrentState { get; private set; }
    public IdleState idleState = new IdleState();
    public WalkState walkState = new WalkState();
    public RollState rollState = new RollState();


    public void Enter() 
    {
      


    }


    public void Update() 
    {

        CurrentState?.Update();

    }


    public void Exit() 
    {
        CurrentState?.Exit();


    }

}
