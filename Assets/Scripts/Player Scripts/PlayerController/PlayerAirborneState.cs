using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborneState :IState
{
    int a = 0;
    public void Enter()
    {

    }


    public void Update()
    {
        if(a  == 0)
        {
            Debug.Log("Airborne state is updated");
            a++;
        }



    }


    public void Exit()
    {

    }



}
