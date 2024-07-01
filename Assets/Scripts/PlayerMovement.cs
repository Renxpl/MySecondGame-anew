using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D myRb;
    string currentAnimation = "";
    Animator playerAnimator;
    bool isLightAttacking = false;
    bool isWalking= false;
    bool isHForm = false;
    Vector2 moveInput;

    

    string lightAttackAnim = "LightAttackTest3";
    string idle = "Idle";
    string walkingAnim = "WalkingTest";
    string hForm = "HForm";
    string hFormStance = "HFormStance";




    [Header("Animation and Movement variables")]
    [SerializeField] Sprite idleStanceSprite;
    [SerializeField] Sprite hFormStanceSprite;

    [Header("Animation and Movement variables")]
    [SerializeField] float walkingSpeed;


    float hFormInput;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        myRb = GetComponent<Rigidbody2D>();



    }



    void Update()
    {
        SpriteChangesInAction();
    }
    void FixedUpdate()
    {
        if(moveInput.x >0 || moveInput.x < 0)
        myRb.velocity = new Vector2(Mathf.Sign(moveInput.x) * walkingSpeed, myRb.velocity.y);
        else
        {
            myRb.velocity = new Vector2(0, myRb.velocity.y);
        }
    }


    void LateUpdate()
    {
        AnimationHandle();
    }

    void OnFire(InputValue input)
    {
        StartCoroutine(LightAttacking());
    }
    IEnumerator LightAttacking()
    {
        isLightAttacking= true;
        yield return new WaitForSecondsRealtime(0.5f);
        isLightAttacking= false;
    }

    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        currentAnimation = newAnimation;

        playerAnimator.Play(currentAnimation);

    }
    void AnimationHandle()
    {
        if (isHForm)
        {
            
            ChangeAnimationState(hForm);
        }
        else if (isLightAttacking)
        {
            ChangeAnimationState(lightAttackAnim);
            
        }
        else if(isWalking)
        {
            ChangeAnimationState(walkingAnim);
        }
        else 
        {
            ChangeAnimationState(idle);
        }
        
    }

    void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
        //Debug.Log("MoveInput Debug Display " + moveInput);
    }

    void SpriteChangesInAction()
    {
        if (myRb.velocity.x > 0f)
        {
            isWalking = true;
            if(myRb.velocity.x > 0.5f)
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (myRb.velocity.x < 0f)
        {
            isWalking = true;
            if (myRb.velocity.x < 0.5f)
                transform.localScale = new Vector2(-1f, 1f);
        }
        else { isWalking = false; }



        if (hFormInput > 0.1)
        {
            isHForm = true;
            

        }
        else
        {
            isHForm = false;
        }
    }
    
    void OnHForm(InputValue input)
    {

        hFormInput= input.Get<float>();
        Debug.Log(hFormInput);
        
    }
   
}
