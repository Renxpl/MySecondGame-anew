using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateMachine 
{
    public IState CurrentState { get; private set; }
    public GroundState groundState = new GroundState();
    public AirborneState airborneState = new AirborneState();

    public void Enter(IState newState)
    {
        CurrentState?.Exit();
        CurrentState= newState;
        newState.Enter();

    }

    public void Update()
    {
        CurrentState?.Update();
    }



   
}
