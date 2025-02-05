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


public class RollInput: ICommand
{
    public void Execute() 
    {

    }

}


public class LightAttackInput : ICommand
{
    public void Execute()
    {

    }

}
public class HeavyAttackInput : ICommand
{
    public void Execute()
    {

    }

}

public class SpecialAttackInput : ICommand
{
    public void Execute()
    {

    }

}