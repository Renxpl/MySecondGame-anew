using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : IState
{

    float timeToPassed;
    float timeToBePassed = 1f;


    public void Enter()
    {
        timeToPassed = 0f;

    }

    public void Update()
    {
        timeToPassed += Time.deltaTime;
        if (timeToPassed < timeToBePassed)
        {
            BossTest.ChangeAnimation(BossTest.idleAnim);


        }

        else
        {

            BossTest.ChangeAnimation(BossTest.idleAnim);

            if(timeToPassed >= timeToBePassed + 0.83f)
            {
                timeToPassed = 0f;


            }


        }
        


    }

    public void Exit()
    {


    }
}
