using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsmanBehaviour : EnemyMain
{
    
    protected override void Start()
    {
        base.Start();
        attackCollider.enabled = false;
        HP = 9;
        HP = 50; stance = 50;
       
    }
    protected override void Update()
    {
        base.Update();
        Debug.DrawRay(transform.position, -transform.right * 2f, Color.red);
        if (isKnockbacking)
        {
           if(attacking != null)
            {
                StopCoroutine(attacking);
                attackCollider.enabled = false;
                isTurningLocked = false;
                isAttacking = false;
            }
        }

    }
    protected override void AttackMode()
    {
        if(attacking != null)
        {
            StopCoroutine(attacking);
            isTurningLocked = false;
            isAttacking = false;
            attackCollider.enabled = false;
        }
        attacking = StartCoroutine(Attacking());
        //Debug.Log("Attacking-Swordsman");
    
    }
    IEnumerator Attacking()
    {
        isAttacking = true;
        isTurningLocked= true;
        enemyAnimator.Play("Attack1");
        yield return new WaitForSeconds(0.45f);
        attackCollider.enabled = true;
        GetComponent<Rigidbody2D>().WakeUp();
        yield return new WaitForSeconds(0.15f);
        attackCollider.enabled = false;
        yield return new WaitForSeconds(0.15f);
        isTurningLocked = false;
        isAttacking= false;
    }

}
