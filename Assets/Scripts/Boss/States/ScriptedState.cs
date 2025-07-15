using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedState : IState
{

    GameObject player = GameObject.Find("Player");
    
    public void Enter()
    {
        BossTest.isSpriteLocked = true;

    }

    public void Update()
    {

        BossTest.ChangeAnimation(BossTest.runningAnim);
        if(Mathf.Abs(BossTest.bossRb.position.x - player.transform.position.x) < 1f)
        {
           
            //player.GetComponent<PlayerNeededValues>().StopTheWay();
            BossScene.beingThrown = true;
            
           
        }
        

       


    }

    public void Exit()
    {
        BossTest.isSpriteLocked = false;

    }
}
