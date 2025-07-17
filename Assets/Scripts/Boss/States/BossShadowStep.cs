using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShadowStep : IState
{

    float timePassed;
    float timeToBePassed = 0.333f;
    BossTest mainScript = GameObject.Find("Boss").GetComponent<BossTest>();
    Collider2D getDmgCol = GameObject.Find("Boss").transform.Find("GetDmgBoss").GetComponent<BoxCollider2D>();
    Vector2 locking;
    // need to fix multiple steps
    float veloc;
    public void Enter()
    {
        timePassed = 0f;
        BossTest.isSpriteLocked = true;
        getDmgCol.enabled = false;
        locking = BossTest.bossRb.transform.localScale;
        veloc = locking.x * 10f;
    }

    public void Update()
    {
        BossTest.ChangeAnimation(BossTest.ss);
        BossTest.bossRb.transform.localScale = locking;
        BossTest.bossRb.velocity = new Vector2(veloc,0f);
        timePassed += Time.deltaTime;
        if (timePassed > timeToBePassed)
        {
            BossTest.alreadyStepped = true;

        }

   


    }

    IEnumerator ResetStep()
    {
        yield return new WaitForSeconds(1f);
        BossTest.alreadyStepped = false;
        resettin = null;
    }

    Coroutine resettin = null;
    public void Exit()
    {
        if (resettin == null) resettin = mainScript.Run(ResetStep());
        BossTest.isSpriteLocked = false;
        getDmgCol.enabled = true;

    }
}
