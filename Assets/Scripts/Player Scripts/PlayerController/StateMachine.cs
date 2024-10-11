using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateMachine 
{
    public IState CurrentState { get; private set; }

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
