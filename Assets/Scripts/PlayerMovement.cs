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
    bool hFormLayer = true;
    bool isRolling = false;
    Vector2 moveInput;

    

    string lightAttackAnim = "LightAttackTest3";
    string idle = "Idle";
    string walkingAnim = "WalkingTest";
    string rollingAnim = "RollingTest1";


    string hForm = "HForm";
    string hFormStance = "HFormStance";
    string hFormAttack = "HFormAttack";
    string hToL = "HtoL";

    



    [Header("Animation and Movement variables")]
    [SerializeField] Sprite idleStanceSprite;
    [SerializeField] Sprite hFormStanceSprite;

    [Header("Animation and Movement variables")]
    [SerializeField] float walkingSpeed;
    [SerializeField] float hFormTransitionSeconds=0.3f;


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
        if(moveInput.x >0.15f || moveInput.x < -0.15f)
        {
            if(!isHForm)
                myRb.velocity = new Vector2(Mathf.Sign(moveInput.x) * walkingSpeed, myRb.velocity.y);
            else
                myRb.velocity = new Vector2(Mathf.Sign(moveInput.x) * walkingSpeed/2f, myRb.velocity.y);
        }
        
        else
        {
            myRb.velocity = new Vector2(0, myRb.velocity.y);
        }

        if (isRolling && !isHForm)
        {
            
            //myRb.velocity = new Vector2( Mathf.Sign(moveInput.x) * 48f, myRb.velocity.y);
            myRb.AddForce(Mathf.Sign(transform.localScale.x) * new Vector2(2.5f, 0) / Time.fixedDeltaTime, ForceMode2D.Impulse);
            Debug.Log(transform.localScale.x);
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
        if(isHForm)
            yield return new WaitForSecondsRealtime(0.82f);
        else
            yield return new WaitForSecondsRealtime(0.5f);
        isLightAttacking = false;
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
            if (!hFormLayer) { StartCoroutine(HFormTransition(hForm));  return;}
            if (isLightAttacking)
            {
                ChangeAnimationState(hFormAttack);

            }
            
            else
            {
                ChangeAnimationState(hFormStance);

            }

        }
        else
        {
            if (!hFormLayer) return;
            playerAnimator.SetLayerWeight(1, 0f);
            if (isLightAttacking)
            {
                ChangeAnimationState(lightAttackAnim);

            }
            else if (isRolling)
            {
                ChangeAnimationState(rollingAnim);
            }
            else if (isWalking)
            {
                ChangeAnimationState(walkingAnim);
            }
            else
            {
                ChangeAnimationState(idle);
            }
        }
        
    }
    IEnumerator HFormTransition(string a)
    {
        ChangeAnimationState(a);
        yield return new WaitForSecondsRealtime(hFormTransitionSeconds);
        hFormLayer = true;


    }
    void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
        Debug.Log("MoveInput Debug Display " + moveInput);
    }

    void SpriteChangesInAction()
    {
        if (myRb.velocity.x > 0f)
        {
            isWalking = true;
            if (myRb.velocity.x > 0.5f)
                transform.localScale = new Vector2(1f, 1f); 

        }
        else if (myRb.velocity.x < 0f)
        {
            isWalking = true;
            if (myRb.velocity.x < -0.5f)
                 transform.localScale = new Vector2(-1f, 1f);  
        }
        else { isWalking = false; }



        if (hFormInput > 0.1)
        {
            if (!isHForm) { isHForm = true; hFormLayer = false; }
            playerAnimator.SetLayerWeight(1, 1f);

        }
        else
        {
            if (isHForm) { StartCoroutine(HFormTransition(hToL)); isHForm = false; hFormLayer = false; }
            
        }
    }
    
    void OnHForm(InputValue input)
    {

        hFormInput= input.Get<float>();
        Debug.Log(hFormInput);

    }
    void OnRolling()
    {
        StartCoroutine(RollingCoroutine());


    }


    IEnumerator RollingCoroutine()
    {
        isRolling = true;
        //myRb.velocity = new Vector2(48f, myRb.velocity.y);
        //myRb.AddForce(new Vector2(10,0) , ForceMode2D.Impulse);
        yield return new WaitForSecondsRealtime(0.25f);
        isRolling = false;

    }
   
}
