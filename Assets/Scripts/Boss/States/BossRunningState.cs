using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRunningState : IState
{
    float speed = 9.5f;
    BossTest mainScript = GameObject.Find("Boss").GetComponent<BossTest>();
    public void Enter()
    {


    }

    public void Update()
    {
        Transform currentP;
        if (BossTest.CurrentHealth / mainScript.hp > 1 / 3f)
            currentP = mainScript.sA1P;
        else
            currentP = mainScript.sA2P;
        
        float distanceX;
        
        if(BlackboardForBoss.purpose == BossPurpose.SpecialAttack)
        {
            distanceX = currentP.position.x - BossTest.bossRb.position.x;
        }
        else
        {
            distanceX = PlayerController.PlayerRB.position.x - BossTest.bossRb.position.x;
        }
            



        BossTest.bossRb.velocity = new Vector2(Mathf.Sign(distanceX) * speed, 0);
      
        //BossTest.bossRb.transform.localScale = new Vector2(Mathf.Sign(distanceX), 1f);
      


        BossTest.ChangeAnimation(BossTest.runningAnim);






    }

    public void Exit()
    {


    }



}
