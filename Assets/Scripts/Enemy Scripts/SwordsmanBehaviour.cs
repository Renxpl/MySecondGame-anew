using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsmanBehaviour : EnemyMain
{
    
    protected override void Start()
    {
        base.Start();
        attackCollider.enabled = false;
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
                GameEvents.gameEvents.OnDisablingAttackCollider(gameObject);
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
            GameEvents.gameEvents.OnDisablingAttackCollider(gameObject);
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
        yield return new WaitForSeconds(0.42f);
        attackCollider.enabled = true;
        yield return new WaitForSeconds(0.083f);
        attackCollider.enabled = false;
        GameEvents.gameEvents.OnDisablingAttackCollider(gameObject);
        yield return new WaitForSeconds(0.247f);
        isTurningLocked = false;
        isAttacking= false;
    }

}
