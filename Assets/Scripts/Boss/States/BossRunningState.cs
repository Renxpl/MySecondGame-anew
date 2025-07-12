using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRunningState : IState
{
    float speed = 9.5f;
    public void Enter()
    {


    }

    public void Update()
    {

        float distanceX = PlayerController.PlayerRB.position.x - BossTest.bossRb.position.x;



        BossTest.bossRb.velocity = new Vector2(Mathf.Sign(distanceX) * speed, 0);
      
        BossTest.bossRb.transform.localScale = new Vector2(Mathf.Sign(distanceX), 1f);
      


        BossTest.ChangeAnimation(BossTest.runningAnim);






    }

    public void Exit()
    {


    }



}
