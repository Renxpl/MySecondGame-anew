using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : IState
{

    float timePassed;
    float timeToBePassed = 1f;


    public void Enter()
    {
        timePassed = 0f;

    }

    public void Update()
    {




        timePassed += Time.deltaTime;




        BossTest.bossRb.velocity = Vector2.zero;



        if (timePassed < timeToBePassed)
        {

            BossTest.ChangeAnimation(BossTest.idleAnim);


        }

        else
        {

            BossTest.ChangeAnimation(BossTest.idleAnim1);

            if(timePassed >= timeToBePassed + 0.83f)
            {
                timePassed = 0f;


            }


        }
        


    }

    public void Exit()
    {


    }
}
