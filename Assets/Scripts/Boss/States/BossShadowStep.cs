using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShadowStep : IState
{

    float timePassed;
    float timeToBePassed = 0.333f;
    BossTest mainScript = GameObject.Find("Boss").GetComponent<BossTest>();
    Collider2D getDmgCol = GameObject.Find("Boss").transform.Find("GetDmgBoss").GetComponent<BoxCollider2D>();


    public void Enter()
    {
        timePassed = 0f;
        BossTest.isSpriteLocked = true;
        getDmgCol.enabled = false;
    }

    public void Update()
    {
        BossTest.ChangeAnimation(BossTest.ss);
        BossTest.bossRb.velocity = new Vector2(BossTest.bossRb.transform.localScale.x * 10f,0f);
        timePassed += Time.deltaTime;
        if (timePassed > timeToBePassed)
        {
            BossTest.alreadyStepped = true;

        }

   


    }

    IEnumerator ResetStep()
    {
        yield return new WaitForSeconds(2f);
        BossTest.alreadyStepped = false;
    }


    public void Exit()
    {
        mainScript.Run(ResetStep());
        BossTest.isSpriteLocked = false;
        getDmgCol.enabled = true;

    }
}
