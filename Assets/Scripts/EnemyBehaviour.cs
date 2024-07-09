using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    Animator enemyAnimator;
    [SerializeField]GameObject player;
    [SerializeField] bool finisher;
    int direction;
    Rigidbody2D soldierRb;
    public float distance;
    GettingDMG dmgScript;
    public bool IsAttacking { get; private set; }


    //Animation variables
    string currentAnimation = "";
    string idleAnim = "Idle";
    string finisher1Anim = "Finisher1";
    string enemyDodgeableAttackAnim = "EnemyDodgeableAttackTest";
    bool isFinisher1 = false;
  


    bool isInRange = false;
    void Start()
    {
        enemyAnimator= GetComponent<Animator>();    
        soldierRb= GetComponent<Rigidbody2D>();
        dmgScript = GetComponentInChildren<GettingDMG>();
    
    
    }

    // Update is called once per frame
    void Update()
    {
        direction = (int)Mathf.Sign(player.transform.position.x-transform.position.x);
        distance = Mathf.Abs(transform.position.x - player.transform.position.x);
        SpriteChanges();




        if (finisher)
        {
            if (distance < 2)
            {
                player.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
                if (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Finisher1"))
                    isFinisher1 = true;


            }
            else
            {
                player.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                isFinisher1 = false;
            }


        }
    }

    void FixedUpdate()
    {
        if (dmgScript.isGetMoved)
        {
            //gotta change the direction variable later
            soldierRb.AddForce(Mathf.Sign(-direction) * new Vector2(3f, 0), ForceMode2D.Impulse);
        }
        else if (isInRange)
        {
            if (distance > 2)
            {
                soldierRb.velocity = new Vector2(direction * 1f, soldierRb.velocity.y);
                
                IsAttacking = false;
            }


            else 
            {
                IsAttacking = true;
             
                soldierRb.velocity = new Vector2(direction * 0f, soldierRb.velocity.y);
         

            }


        }


        else
        {
            soldierRb.velocity = new Vector2(direction * 0f, soldierRb.velocity.y);
         
            IsAttacking = false;
        }


    }
    void LateUpdate()
    {
        AnimationHandle();
    }


    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        currentAnimation = newAnimation;

        enemyAnimator.Play(currentAnimation);

    }
    void AnimationHandle()
    {
        if (isFinisher1)
        {
            ChangeAnimationState(finisher1Anim);
        }


        else if (IsAttacking)
        {
            ChangeAnimationState(enemyDodgeableAttackAnim);
           

        }


        else
        {
            ChangeAnimationState(idleAnim);
        }

    }
    void SpriteChanges()
    {

        if(dmgScript.isGetMoved)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 141, 141, 255);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }

        if (distance < 5)
        {
            isInRange = true;



            if (transform.position.x - player.transform.position.x > 0)
            {
                transform.localScale = new Vector2(-1,1);



            }

            else if (transform.position.x - player.transform.position.x < 0)
            {
                transform.localScale = new Vector2(1, 1);
            }



        }
        else { isInRange = false; }
       
    }

}
