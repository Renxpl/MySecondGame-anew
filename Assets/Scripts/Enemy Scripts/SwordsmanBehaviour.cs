using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsmanBehaviour : EnemyMain
{
    [SerializeField] Collider2D attackCollider;
    protected override void Start()
    {
        base.Start();
        attackCollider.enabled = false;
    }
    protected override void Update()
    {
        base.Update();
        Debug.DrawRay(transform.position, -transform.right * 2f, Color.red);

    }
    protected override void AttackMode()
    {
        attacking = StartCoroutine(Attacking());
        //Debug.Log("Attacking-Swordsman");
    
    }
    IEnumerator Attacking()
    {
        isTurningLocked= true;
        
        enemyAnimator.Play("Attack1");
        yield return new WaitForSeconds(0.42f);
        attackCollider.enabled = true;
        yield return new WaitForSeconds(0.05f);
        attackCollider.enabled = false;
        yield return new WaitForSeconds(0.28f);
        isTurningLocked = false;
        isAttacking= false;
    }

}
