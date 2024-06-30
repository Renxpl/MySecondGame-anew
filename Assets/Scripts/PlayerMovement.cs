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
    Vector2 moveInput;

    string lightAttackAnim = "LightAttackTest3";
    string idle = "Idle";
    string walkingAnim = "WalkingTest";

    [Header("Animation and Movement variables")]
    [SerializeField] float walkingSpeed;

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
        if (isLightAttacking)
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
        Debug.Log("MoveInput Debug Display " + moveInput);
    }

    void SpriteChangesInAction()
    {
        if (myRb.velocity.x > 0)
        {
            isWalking = true;
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (myRb.velocity.x < 0)
        {
            isWalking = true;
            transform.localScale = new Vector2(-1f, 1f);
        }
        else { isWalking = false; }
       

        
    }
    


}
