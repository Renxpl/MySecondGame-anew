using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D myRb;
    string currentAnimation = "";
    Animator playerAnimator;
    
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

    public bool IsLightAttacking { get; private set; }
    bool isAttacking1 = false;
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
    [SerializeField] float timeSlowDuration = 1.5f;

    float hFormInput;
    float parryInput;


    [Header("Slowing Time")]
    //slowing time
    public float slowMotionTimeScale = 0.5f;

    float startTimeScale;
    float startFixedDeltaTime;
    [SerializeField] EnemyBehaviour enemyBehaviour;
    [SerializeField] GettingDMG enemyGettingDmg;
    bool isCoroutineFinished = true;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        myRb = GetComponent<Rigidbody2D>();
        swordCollider = GetComponentInChildren<PolygonCollider2D>();
        startTimeScale= Time.timeScale;
        startFixedDeltaTime= Time.fixedDeltaTime;
        if(GameEvents.gameEvents !=null)GameEvents.gameEvents.onDepleted += HandleDead;
        
    }

    IEnumerator TimeSlow()
    {
        isCoroutineFinished= false;
        Time.timeScale = slowMotionTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime * slowMotionTimeScale;
        enemyGettingDmg.dmg = 3;
        yield return new WaitForSecondsRealtime(timeSlowDuration);
        Time.timeScale = startTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime;
        enemyGettingDmg.dmg = 1;
        isCoroutineFinished = true;


    }

    void Update()
    {
       
        if(enemyBehaviour.IsAttacking && isRolling && enemyBehaviour.distance < 2.5f && enemyBehaviour != null)
        {
            StartCoroutine(TimeSlow());
            

        }

        if(Time.timeScale < 1f)
        {

            playerAnimator.speed = 1f / (Time.timeScale*2f);
            walkingSpeed = 3.5f / (Time.timeScale *2f);

        }
        else
        {
            playerAnimator.speed = 1f;
            walkingSpeed = 3.5f;
        }


       /* else if(isCoroutineFinished)
        {
            Time.timeScale = startTimeScale;
            Time.fixedDeltaTime= startFixedDeltaTime;
            enemyGettingDmg.dmg = 1;
            Debug.Log("Time is not Slow");
        }*/


        SpriteChangesInAction();
        if (parryInput > 0)
        {
            isParrying= true;
        }
        else { isParrying = false; }
    }
    void FixedUpdate()
    {
        bool notAttacking = !isAttacking1 && !isAttacking2 && !isAttacking3;
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
            //Debug.Log(transform.localScale.x);
        }
        if ((isAttacking1 && !isHForm) || (isAttacking3 && !isHForm))
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


    void HandleDead()
    {
        Destroy(gameObject);

    }


    void OnFire(InputValue input)
    {
        if (!isAttacking1 && !isAttacking2 && !isAttacking3)
        {
            isAttacking1 = true;
            Attack1 = StartCoroutine(LightAttacking());

        }
        if(isNextAttackUnlocked && isAttacking1 && !isAttacking3)
        {
            isAttacking2 = true;
        }
        if (isNextAttackUnlocked && !isAttacking1 && isAttacking2)
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
            IsLightAttacking= true;
            yield return new WaitForSecondsRealtime(0.067f);
            isNextAttackUnlocked = true;
            yield return new WaitForSecondsRealtime(0.1f);
            IsLightAttacking = false;
            if (isAttacking2)
            {
                Attack2 = StartCoroutine(Attacking2());
            }
           

        }
        isNextAttackUnlocked = false;
        isAttacking1 = false;
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

            IsLightAttacking = true;
            isNextAttackUnlocked = true;
            yield return new WaitForSecondsRealtime(0.2f);
            IsLightAttacking = false;
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

            IsLightAttacking = true;
            isNextAttackUnlocked = false;
            yield return new WaitForSecondsRealtime(0.15f);
            IsLightAttacking = false;


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

            else if (isAttacking1)
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
            if (isAttacking1)
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
        //Debug.Log("MoveInput Debug Display " + moveInput);
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
        //Debug.Log(hFormInput);

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
