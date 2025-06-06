using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKbBehaviour 
{
    EnemyController self;
    Rigidbody2D enemyRb;
    Transform target;


    public EnemyKbBehaviour(EnemyController ctrl, Rigidbody2D rb, Transform playerTransform )
    {

        self = ctrl;
        enemyRb= rb;
        target = playerTransform;


    }

    public void Knockbacking()
    {

        self.Run(KBCoroutine());


    }



    IEnumerator KBCoroutine()
    {
        self.LockEnemySprite();
        self.GetComponent<Animator>().Play("Knockback");
        enemyRb.AddForce(PlayerController.forward * 5f / 3f, ForceMode2D.Impulse);
        self.gameObject.transform.localScale = new Vector2(Mathf.Sign(-PlayerController.forward.x), self.gameObject.transform.localScale.y);
        yield return new WaitForSeconds(0.25f);
        enemyRb.velocity = new Vector2(0f, 0f);
        self.GetComponent<Animator>().Play("Staggering");
        yield return new WaitForSeconds(1.5f);

        self.ReplenishingStance();
        self.ChangeState(new EnemyMovState());



    }







}
