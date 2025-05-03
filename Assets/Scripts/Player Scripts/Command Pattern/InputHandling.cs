using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandling : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class JumpInput : ICommand
{
    public void Execute()
    {
        PlayerNeededValues.JumpExecute();
    }

    public void Exit()
    {
        
    }



}

public class AAInput : ICommand
{
    public void Execute()
    {
        GameObject.FindObjectOfType<PlayerNeededValues>().AAExecute();
    }
}
public class RollInput: ICommand
{
    public void Execute() 
    {
        GameObject.FindObjectOfType<PlayerNeededValues>().RollExecute();
    }

}


public class LightAttackInput : ICommand
{
    public void Execute()
    {
        GameObject.FindObjectOfType<PlayerNeededValues>().LightAttackExecution();
    }

}
public class HeavyAttackInput : ICommand
{
    public void Execute()
    {
        GameObject.FindObjectOfType<PlayerNeededValues>().HeavyAttackExecution();
    }

}

public class SpecialAttackInput : ICommand
{
    public void Execute()
    {
        GameObject.FindObjectOfType<PlayerNeededValues>().SpecialAttackExecution();
    }

}
