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
    bool isParrying = false;
    Vector2 moveInput;
    Coroutine Attack1 = null;
    Coroutine Attack2 = null;
    Coroutine Attack3 = null;

    string lightAttackAnim = "LightAttackTest3";
    string idle = "Idle";
    string walkingAnim = "WalkingTest";
    string rollingAnim = "RollingTest1";


    //for Light Attacks
    string lightAttack1Anim = "LightAttack1";
    string lightAttack2Anim = "LightAttack2";
    string lightAttack3Anim = "LightAttack3";
    
    bool isAttacking2 = false;
    bool isAttacking3 = false;
    bool isNextAttackUnlocked = false;
    


    //hForm
    string hForm = "HForm";
    string hFormStance = "HFormStance";
    string hFormAttack = "HFormAttack";
    string hToL = "HtoL";
    string parry = "Parry";


    //Hitboxes
    [Header("Hitboxes")]
    PolygonCollider2D swordCollider;
    [SerializeField]BoxCollider2D bodyHitbox;
    [SerializeField] BoxCollider2D rollingHitbox;




    [Header("Animation and Movement variables")]
    [SerializeField] Sprite idleStanceSprite;
    [SerializeField] Sprite hFormStanceSprite;

    [Header("Animation and Movement variables")]
    [SerializeField] float walkingSpeed;
    [SerializeField] float hFormTransitionSeconds=0.3f;


    float hFormInput;
    float parryInput;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        myRb = GetComponent<Rigidbody2D>();
        swordCollider = GetComponentInChildren<PolygonCollider2D>();
        
        
    }



    void Update()
    {
        SpriteChangesInAction();
        if (parryInput > 0)
        {
            isParrying= true;
        }
        else { isParrying = false; }
    }
    void FixedUpdate()
    {
        bool notAttacking = !isLightAttacking && !isAttacking2 && !isAttacking3;
        if ((moveInput.x >0.15f || moveInput.x < -0.15f)&& notAttacking)
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

        if (isRolling && !isHForm && notAttacking)
        {
            
            //myRb.velocity = new Vector2( Mathf.Sign(moveInput.x) * 48f, myRb.velocity.y);
            myRb.AddForce(Mathf.Sign(transform.localScale.x) * new Vector2(1.75f, 0) / Time.fixedDeltaTime, ForceMode2D.Impulse);
            Debug.Log(transform.localScale.x);
        }
        if ((isLightAttacking && !isHForm) || (isAttacking3 && !isHForm))
        {
            
            
                myRb.AddForce(Mathf.Sign(transform.localScale.x) * new Vector2(2f, 0) / Time.fixedDeltaTime, ForceMode2D.Impulse);
            

        }

        if (isRolling)
        {
            bodyHitbox.enabled= false;
            rollingHitbox.enabled = true;

        }
        else
        {
            bodyHitbox.enabled = true;
            rollingHitbox.enabled = false;
        }


    }


    void LateUpdate()
    {
        AnimationHandle();
    }

    void OnFire(InputValue input)
    {
        if (!isLightAttacking && !isAttacking2 && !isAttacking3)
        {
            isLightAttacking = true;
            Attack1 = StartCoroutine(LightAttacking());

        }
        if(isNextAttackUnlocked && isLightAttacking && !isAttacking3)
        {
            isAttacking2 = true;
        }
        if (isNextAttackUnlocked && !isLightAttacking && isAttacking2)
        {
            isAttacking3 = true;
        }

    }
    IEnumerator LightAttacking()
    {
        
        if (isHForm)
        { 
            yield return new WaitForSecondsRealtime(0.82f); 
        }
        else
        {
            
            yield return new WaitForSecondsRealtime(0.067f);
            isNextAttackUnlocked = true;
            yield return new WaitForSecondsRealtime(0.1f);
            
            if (isAttacking2)
            {
                Attack2 = StartCoroutine(Attacking2());
            }
           

        }
        isNextAttackUnlocked = false;
        isLightAttacking = false;
        Attack1 = null;
    }
    IEnumerator Attacking2()
    {
       
        if (isHForm)
        {
            yield return new WaitForSecondsRealtime(0.82f);
        }
        else
        {
            yield return new WaitForSecondsRealtime(0.383f);


            isNextAttackUnlocked = true;
            yield return new WaitForSecondsRealtime(0.2f);
        
            if (isAttacking3)
            {
                Attack3 = StartCoroutine(Attacking3());
            }


        }
        isNextAttackUnlocked = false;
        isAttacking2 = false;
        Attack2 = null;
    }
    IEnumerator Attacking3()
    {
      

        if (isHForm)
        {
            yield return new WaitForSecondsRealtime(0.82f);
        }
        else
        {
            yield return new WaitForSecondsRealtime(0.1f);


            isNextAttackUnlocked = false;
            yield return new WaitForSecondsRealtime(0.15f);
         


        }
        isNextAttackUnlocked = false;
        isAttacking3 = false;
        Attack3 = null;
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
            if (isParrying)
            {
                ChangeAnimationState(parry);
            }

            else if (isLightAttacking)
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
                if(Attack1==null)
                Attack1 = StartCoroutine(LightAttacking());

            }
            else if (isAttacking2)
            {
                ChangeAnimationState(lightAttack2Anim);
               
            }
            else if (isAttacking3)
            {
                ChangeAnimationState(lightAttack3Anim);
                
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
   

    void OnParry(InputValue input)
    {
        parryInput= input.Get<float>();


    }

}
