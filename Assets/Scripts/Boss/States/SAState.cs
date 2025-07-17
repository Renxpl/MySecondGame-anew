using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAState : IState
{
    float timePassed;
    Coroutine attackCo;
    BossTest bossMainScript = GameObject.Find("Boss").GetComponent<BossTest>();
    GameObject player = GameObject.Find("Player");
    public void Enter()
    {
        timePassed = 0f;
        BossTest.bossRb.velocity = Vector2.zero;
        BossTest.isSpriteLocked = true;

    }


    IEnumerator Attack1()
    {


        yield return null;





    }
    IEnumerator Attack2()
    {
      yield return null;


    }
   
    public void Update()
    {
        timePassed += Time.deltaTime;
        if (BossTest.AttackStep == 0)
        {


            if (attackCo == null)
            {
                attackCo = bossMainScript.Run(Attack1());
            }




        }

        else if (BossTest.AttackStep == 1)
        {


            if (attackCo == null)
                attackCo = bossMainScript.Run(Attack2());

        }

       







    }

    public void Exit()
    {
        BossTest.isSpriteLocked = false;
        

    }
}
