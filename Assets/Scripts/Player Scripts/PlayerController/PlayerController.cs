using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static Rigidbody2D PlayerRB { get; private set; }
    public static StateMachine playerSM;
    public static string currentAnimation = "";
    public static Animator playerAnimator;
    public static Vector2 forward;

    [Header("Slowing Time")]
    //slowing time
    public static float slowMotionTimeScale = 1/3f;
    public static  float timeSlowDuration = 3f;
    public static  float animationTimeVector = 1f;
    float startTimeScale;
    float startFixedDeltaTime;
    bool isTimeSlowStarted;
    
    Coroutine timeSlow = null;
    public static float animatorTimeVector;

    void Awake()
    {
        PlayerRB = gameObject.GetComponent<Rigidbody2D>();
        playerSM = new StateMachine();
        playerAnimator = gameObject.GetComponent<Animator>();
    }
    void Start()
    {
       
        playerSM?.ChangeState(PlayerNeededValues.GroundedStateForPlayer);
       // Debug.Log("Ground state started");
        forward = new Vector2(transform.localScale.x,0f);
        startTimeScale = Time.timeScale;
        startFixedDeltaTime = Time.fixedDeltaTime;
        animatorTimeVector = 1f;

    }

    void Update()
    {
        

        /*if (PlayerNeededValues.MoveInput.x > 0.75f || 0.75f < PlayerNeededValues.MoveInput.x)
        {

            playerSM.ChangeState(playerSM.walkState);



        }


         */
        playerSM?.Update();
        //Debug.Log("state machine updated");

        if (!PlayerNeededValues.IsKnocbacking && !PlayerNeededValues.IsLightAttack && !PlayerNeededValues.IsHeavyAttack && !PlayerNeededValues.IsSpecialAttack && !PlayerNeededValues.IsAirborneAttack)
        {
            if (PlayerNeededValues.MoveInput.x > 0 && PlayerRB.velocity.x > 0)
            {
                transform.localScale = new Vector2(1f, 1f);
            }
            else if (PlayerNeededValues.MoveInput.x < 0 && PlayerRB.velocity.x < 0)
            {
                transform.localScale = new Vector2(-1f, 1f);
            }
        }
        

        forward = new Vector2(transform.localScale.x, 0f);

       

        if (Time.timeScale < 1f)
        {
            //PlayerRB.mass = 15f;
            PlayerRB.gravityScale = 24f;
            //PlayerRB.drag = 5f;

            if (PlayerNeededValues.IsLightAttack)
            {
                //animatorTimeVector = animationTimeVector * (1f/PlayerNeededValues.AttackSpeed);
                playerAnimator.speed = 1f / (Time.timeScale * (1f / PlayerNeededValues.AttackSpeed));
            }
            else
            {
                animatorTimeVector = animationTimeVector;
                playerAnimator.speed = 1f / (Time.timeScale * animatorTimeVector);
            }
           
           

        }
        else
        {
            PlayerRB.gravityScale = 8f;
            //PlayerRB.mass = 5f;
            //PlayerRB.drag = 5f;
            if (PlayerNeededValues.IsLightAttack)
            {

                //animatorTimeVector = (1f / PlayerNeededValues.AttackSpeed);
                playerAnimator.speed = 1f / (Time.timeScale * (1f / PlayerNeededValues.AttackSpeed));
            }
            else
            {
                animatorTimeVector = 1f;
                playerAnimator.speed = 1f;
            }
           
          
        }

    }


    public static void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        currentAnimation = newAnimation;

        playerAnimator.Play(currentAnimation);

    }

    

    IEnumerator TimeSlow()
    {

        PlayerNeededValues.SpecialAttackBar -= 3;
        Time.timeScale = slowMotionTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime * slowMotionTimeScale;
        
        yield return new WaitForSecondsRealtime(timeSlowDuration);
        Time.timeScale = startTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime;

        
        
        timeSlow = null;
        isTimeSlowStarted= false;

    }
    void OnTimeSlow(InputValue input)
    {
        Debug.Log(input.Get<float>());
        if (PlayerNeededValues.SpecialAttackBar >= 3 && !isTimeSlowStarted)
        {
            isTimeSlowStarted= true;
            if (timeSlow != null) { StopCoroutine(timeSlow); }
          
            timeSlow = StartCoroutine(TimeSlow());
        }
       

    }

   

}
