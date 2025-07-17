using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShadowStep : IState
{

    float timePassed;
    float timeToBePassed = 1f;


    public void Enter()
    {
        timePassed = 0f;

    }

    public void Update()
    {
        BossTest.ChangeAnimation(BossTest.ss);



   


    }

    public void Exit()
    {


    }
}
