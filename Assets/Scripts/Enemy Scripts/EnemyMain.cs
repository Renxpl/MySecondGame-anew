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
    protected bool isAttacking = false;
    protected bool isTurningLocked = false;
    protected static string currentAnimation ="";
    protected static Animator enemyAnimator;
    protected Coroutine moving;
    protected Coroutine attacking;
    protected virtual void Start()
    {
        GameEvents.gameEvents.onGettingDmg += TakingDamage;
        player = GameObject.Find("Player");
        enemyRb = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        
    }

    protected virtual void Update()
    {
        backward = new Vector2(-transform.localScale.x,0f);
        xDiff = transform.localScale.x * (player.transform.position.x - transform.position.x);
        yDiff =  Mathf.Abs(player.transform.position.y - transform.position.y);
        if (!isKnockbacking)
        {


            if (yDiff < 1f)
            {
                if (xDiff > AttackRange)
                {
                    if (!IsMoving)
                    {
                        
                        moving = StartCoroutine(Move());
                        isStopped = false;
                    }

                }
                else if (xDiff >= 0)
                {
                    if (!isKnockbacking)
                    {
                        enemyRb.velocity = new Vector2(0f, 0f); isStopped = true;
                    }
                    if (moving != null)
                    {
                        StopCoroutine(moving);
                        IsMoving = false;
                    }
                    isAttacking = true;
                    AttackMode();
                }
                else if (xDiff >= -AttackRange)
                {
                    if (!isTurningLocked)
                        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

                }
                else
                {
                    if (!isTurningLocked)
                        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

                }
            }

            else
            {

                if (xDiff > AttackRange)
                {
                    if (!IsMoving)
                    {
                        moving = StartCoroutine(Move());
                    }

                }
                else if (xDiff >= 0)
                {
                    //Do Nothing
                }
                else if (xDiff >= -AttackRange)
                {
                    if (!isTurningLocked)
                        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                    //Do Nothing
                }
                else
                {
                    if (!isTurningLocked)
                        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

                }

            }
        }

        if(!isKnockbacking) Following();



    }
    protected virtual void Following()
    {
        if (!isAttacking)
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
                enemyAnimator.Play("Walking");
            }
            else
            {
                enemyAnimator.Play("Idle");
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
        enemyAnimator.Play("Knockback");
        enemyRb.AddForce(PlayerController.forward * forceFactor, ForceMode2D.Impulse);
        transform.localScale = new Vector2(Mathf.Sign(-PlayerController.forward.x),transform.localScale.y);
        yield return new WaitForSeconds(knockbackDuration);
        enemyRb.velocity = new Vector2(0f, 0f);
        isKnockbacking = false;
        

    }


    public static void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        currentAnimation = newAnimation;

        enemyAnimator.Play(currentAnimation);

    }

}
