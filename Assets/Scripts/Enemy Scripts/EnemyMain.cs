using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyMain : MonoBehaviour
{
    [SerializeField] protected float AttackRange;
    [SerializeField] protected float enemyVelocity;
    [SerializeField] protected float minMovementDuration;
    [SerializeField] protected float forceFactor;
    [SerializeField] protected float knockbackDuration;
    protected bool IsMoving = false;
    

    [SerializeField] GameObject getDmgRb;
    protected GameObject player;
    protected Rigidbody2D enemyRb;

    protected Vector2 firstPosition;
    protected float xDiff;
    protected float yDiff;
    protected Vector2 backward;
    protected bool isStopped = true;
    protected bool isKnockbacking = false;

    protected virtual void Start()
    {
        GameEvents.gameEvents.onGettingDmg += TakingDamage;
        player = GameObject.Find("Player");
        enemyRb = GetComponent<Rigidbody2D>();
        
    }

    protected virtual void Update()
    {
        backward = new Vector2(-transform.localScale.x,0f);
        xDiff = transform.localScale.x * (player.transform.position.x - transform.position.x);
        yDiff =  Mathf.Abs(player.transform.position.y - transform.position.y);

        if (yDiff < 1f)
        {
            if(xDiff > AttackRange)
            {
                if (!IsMoving)
                {
                    StartCoroutine(Move());
                    isStopped= false;
                }
              
            }
            else if (xDiff>=0)
            {
                if (!isKnockbacking)
                {
                    enemyRb.velocity = new Vector2(0f, 0f); isStopped = true;
                }
                
                AttackMode();
            }
            else if (xDiff >= -AttackRange)
            {
                
                transform.localScale = new Vector2 (-transform.localScale.x, transform.localScale.y);
                
            }
            else
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                
            }
        }

        else
        {

            if (xDiff > AttackRange)
            {
                if (!IsMoving)
                {
                    StartCoroutine(Move());
                }
               
            }
            else if (xDiff >= 0)
            {
               //Do Nothing
            }
            else if (xDiff >= -AttackRange)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                //Do Nothing
            }
            else
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                
            }

        }
        if(!isKnockbacking) Following();



    }
    protected virtual void Following()
    {
        if (IsMoving)
        {
            if (transform.localScale.x == 1)
            {
                enemyRb.velocity = new Vector2(enemyVelocity, 0f);
            }
            else if (transform.localScale.x == -1)
            {
                enemyRb.velocity = new Vector2(-enemyVelocity, 0f);
            }
            else
            {
                Debug.Log("There is a problem of localScale.x discrepancy");
            }
        }

    }

    protected virtual IEnumerator Move()
    {

        IsMoving = true;
        yield return new WaitForSeconds(minMovementDuration);
        IsMoving = false;

    }

    protected abstract void AttackMode();

    protected virtual void TakingDamage(GameObject receiver, GameObject sender, Collider2D otherCollider, int attakVer)
    {
        if(receiver == gameObject)
        {

            Debug.Log("GettingDmg");

            
            if(!isKnockbacking) StartCoroutine(KnockBacking());


        }
    }

    IEnumerator KnockBacking()
    {

        isKnockbacking = true;
        enemyRb.AddForce(backward * forceFactor, ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockbackDuration);
        enemyRb.velocity = new Vector2(0f, 0f);
        isKnockbacking = false;
        

    }

}
