using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFirstJump : IState
{
    // Start is called before the first frame update
    public void Enter()
    {

        float distanceY = PlayerController.PlayerRB.transform.position.y - BossTest.bossRb.position.y;

        float g = Mathf.Abs(Physics2D.gravity.y * BossTest.bossRb.gravityScale);

        float v = Mathf.Sqrt(2 * g * distanceY);
        Debug.Log(v);

        BossTest.bossRb.velocity = new Vector2(0, v);

    }

    // Update is called once per frame
    public void Update()
    {

        if (BossTest.bossRb.velocity.y > 0)
        {
            BossTest.ChangeAnimation(BossTest.ju);
        }
        else
        {
            BossTest.ChangeAnimation(BossTest.jd);
        }
    }


    public void Exit()
    {


    }

}
