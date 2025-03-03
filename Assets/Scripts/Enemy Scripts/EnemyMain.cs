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
    protected int stance;
    

    [SerializeField] GameObject getDmgRb;
    protected GameObject player;
    protected Rigidbody2D enemyRb;
    [SerializeField] protected Collider2D attackCollider;

    protected Vector2 firstPosition;
    protected float xDiff;
    protected float yDiff;
    protected Vector2 backward;
    protected bool isStopped = true;
    protected bool isKnockbacking = false;
    protected bool isAttacking = false;
    protected bool isTurningLocked = false;
    protected bool isInStagger = false;
    protected string currentAnimation ="";
    protected Animator enemyAnimator;
    protected Coroutine moving;
    protected Coroutine attacking;

    protected Color baseColor;
    [SerializeField]protected Color onDmgColor;

    protected float HP;
    protected virtual void Start()
    {
        GameEvents.gameEvents.onGettingDmg += TakingDamage;
        player = GameObject.Find("Player");
        enemyRb = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        stance = 3;
        baseColor = GetComponent<SpriteRenderer>().color;
        
    }

    protected virtual void Update()
    {
        backward = new Vector2(-transform.localScale.x,0f);
        xDiff = transform.localScale.x * (player.transform.position.x - transform.position.x);
        yDiff =  Mathf.Abs(player.transform.position.y - transform.position.y);
        
        if (!isKnockbacking &&!isAttacking)
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
                    if (moving != null)
                    {
                        StopCoroutine(moving);
                        IsMoving = false;
                    }
                    enemyRb.velocity = new Vector2(0f, 0f); isStopped = true;
                    
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
                    if (!IsMoving )
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

        if(!isKnockbacking && !isAttacking) Following();


        if(HP <= 0)
        {
            gameObject.SetActive(false);
        }
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
            enemyAnimator.Play("Walking");
        }
        else
        {
            enemyAnimator.Play("Idle");
        }

    }

    protected virtual IEnumerator Move()
    {
      
        IsMoving = true;
        yield return new WaitForSeconds(minMovementDuration);
        IsMoving = false;

    }

    protected abstract void AttackMode();

    protected virtual void TakingDamage(GameObject receiver, GameObject sender, Collider2D otherCollider, int attackVer)
    {
        if(receiver == gameObject)
        {
            //for light attacks
            if(attackVer == 0)
            {
                stance--;
                if (!isKnockbacking && stance == 0) StartCoroutine(KnockBacking());
                StartCoroutine(TurningColorRed());
                if (!isInStagger) HP--;
                else HP -= 2;
                Debug.Log("Enemy HP:" + HP);

                if (PlayerNeededValues.IsLightAttack) PlayerNeededValues.IsHitting = true;
            }
            //for AA
            else if(attackVer == 1)
            {

            }
           
            //for HA1
            else if (attackVer == 10)
            {

            }
            
            //for HA2
            else if (attackVer == 11)
            {

            }
            //forHA3
            else if (attackVer == 12)
            {

            }
            //for SA1
            else if(attackVer == 20)
            {

            }



        }
    }
    IEnumerator TurningColorRed() 
    {
        GetComponent<SpriteRenderer>().color = onDmgColor;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = baseColor;

    }

    IEnumerator KnockBacking()
    {
        
        isKnockbacking = true;
        enemyAnimator.Play("Knockback");
        enemyRb.AddForce(PlayerController.forward * forceFactor, ForceMode2D.Impulse);
        transform.localScale = new Vector2(Mathf.Sign(-PlayerController.forward.x),transform.localScale.y);
        yield return new WaitForSeconds(knockbackDuration);
        enemyRb.velocity = new Vector2(0f, 0f);
        //will play stagger in future
        isInStagger = true;
        enemyAnimator.Play("Staggering");
        yield return new WaitForSeconds(1.5f);
        isInStagger = false;
        stance = 3;
        isKnockbacking = false;
        

    }


    public void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        currentAnimation = newAnimation;

        enemyAnimator.Play(currentAnimation);

    }

}
