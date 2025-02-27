using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNeededValues : MonoBehaviour
{
    public static PlayerGroundedState GroundedStateForPlayer { get; private set; }
    public static PlayerAirborneState AirborneStateForPlayer { get; private set; }
    public static PlayerIdleState IdleStateForPlayer { get; private set; }
    public static PlayerWalkState WalkStateForPlayer { get; private set; }  
    public static PlayerRunState RunStateForPlayer { get; private set; }
    public static PlayerRollState RollStateForPlayer { get; private set; }
    public static PlayerAttackModeState AttackModeStateForPlayer { get; private set; }
    public static PlayerJumpState JumpStateForPlayer { get; private set; }

    public static PlayerGrAttackState GrAttackState { get; private set; }
    public static PlayerKnockbackState playerKbState { get; private set; }


    public static bool IsGroundedPlayer { get; private set; }
    public static bool IsRolling { get; private set; }
    public static bool IsJumping { get; private set; }
    public static bool IsSpacePressing { get; private set; }
    public static bool IsJumpingUp { get; private set; }
    public static float JumpTime { get; private set; }
    public static float JumpSpeed { get; private set; }
    public static bool IsLightningAura { get; private set; }
    public static bool IsWindAura { get; private set; }

    public static bool IsHeavyAttack { get; private set; }
    public static bool IsLightAttack { get; private set; }

    public static bool IsSpecialAttack { get; private set; }
    public static bool IsKnocbacking { get; private set; }
    public static bool IsSheating { get; private set; }
    public static bool IsUnsheating { get; private set; }
    public static bool IsDuringAttack { get; private set; }
    public static int ComboCounter { get; private set; }
    public static float AttackSpeed { get; private set; }

    public static LayerMask groundLayer;
    public static GameObject player;


    public AnimationCurve jumpCurve;



    public static Vector2 MoveInput { get; private set; }
    public static Vector2 RollInput { get; private set; }

    static float jumpInput;
    bool isLightningCoroutineStarted = false;
    bool isWindCoroutineStarted = false;
    bool extraRollingWait = false;
    static bool isResettingAttack = false;
    static bool isResettingCombo = false;

    public static HeavyAttackInput heavyAttackInput;
    public static LightAttackInput lightAttackInput;
    public static SpecialAttackInput specialAttackInput;
    public static RollInput rollInput;
    public static int AttackNumber { get; private set; }
    public static int LightAttackNumber { get; private set; }
    public static int SpecialAttackNumber { get; private set; }
    public static int Stamina { get; private set; }
    //next input will be handled by bools
    [SerializeField] float knockbackDuration;
    [SerializeField] Collider2D lightAttackCollider;

    Coroutine lightAttackCoroutine;
    Coroutine heavyAttackCoroutine;
    Coroutine resettingAttack;
    Coroutine resettingCombo;

    void Awake()
    {
        GroundedStateForPlayer = new PlayerGroundedState();
        AirborneStateForPlayer = new PlayerAirborneState();
        IdleStateForPlayer = new PlayerIdleState();
        WalkStateForPlayer = new PlayerWalkState();
        RunStateForPlayer = new PlayerRunState();
        RollStateForPlayer = new PlayerRollState();
        AttackModeStateForPlayer= new PlayerAttackModeState();
        JumpStateForPlayer = new PlayerJumpState();
        GrAttackState = new PlayerGrAttackState();
        heavyAttackInput = new HeavyAttackInput();
        lightAttackInput = new LightAttackInput();
        specialAttackInput = new SpecialAttackInput();
        rollInput = new RollInput();
        playerKbState = new PlayerKnockbackState();
    }

    // Start is called before the first frame update
    void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
        player = PlayerController.PlayerRB.gameObject;
        IsLightningAura= false;
        AttackNumber = 1;
        LightAttackNumber = 1;
        Stamina = 15;
        GameEvents.gameEvents.onGettingDmg += TakingDamage;
        MoveInput = new Vector2();
        RollInput = new Vector2();
        lightAttackCollider.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        IsGroundedPlayer = Physics2D.Raycast(player.transform.position, Vector2.down, 1f, groundLayer);
        Debug.DrawRay(player.transform.position, Vector2.down * 1f, IsGroundedPlayer ? Color.green : Color.red);
        
        if(IsGroundedPlayer)
        {
            IsJumping = false;
            JumpTime = 0;
        }


        if(jumpInput != 0 && JumpTime <= 0.5f &&IsSpacePressing)
        {
            JumpTime += Time.deltaTime;
            IsSpacePressing= true;
            JumpSpeed = jumpCurve.Evaluate(JumpTime);
        }
        else
        {
            IsSpacePressing = false;
            IsJumping= false;
        }

        IsJumpingUp = IsSpacePressing;


        if (isResettingAttack)
        {
            if(resettingAttack != null) StopCoroutine(resettingAttack);
            resettingAttack = StartCoroutine(ResettingLightAttack(1));
            
            isResettingAttack= false;
        }
        if (isResettingCombo)
        {
            if (resettingCombo != null) StopCoroutine(resettingCombo);
            resettingCombo = StartCoroutine(ResettingLightAttack(0));

            isResettingCombo = false;
        }

        if (ComboCounter < 10)
        {
            AttackSpeed = 1f;
        }
        else if(ComboCounter < 20)
        {
            AttackSpeed = 1.1f;
        }
        else if (ComboCounter < 30)
        {
            AttackSpeed = 1.22f;
        }
        else if (ComboCounter < 40)
        {
            AttackSpeed = 1.35f;
        }
        else
        {
            AttackSpeed = 1.5f;
        }

        if (!IsLightAttack)
        {
            lightAttackCollider.enabled = false;
        }
    }
    

    void OnMove(InputValue input)
    {
        MoveInput = input.Get<Vector2>();
        //Debug.Log("MoveInput Debug Display " + MoveInput);
        //Debug.Log("IsGrounded: " + IsGroundedPlayer);
    }


    void OnRolling()
    {
        //Debug.Log("Rolling");


        CommandHandler.HandleCommand(rollInput);




    }
    public void RollExecute()
    {
        
        if (!IsRolling && !extraRollingWait)
        {
            RollInput = MoveInput;
            if(lightAttackCoroutine!= null)   StopCoroutine(lightAttackCoroutine);
            if (heavyAttackCoroutine != null) StopCoroutine(heavyAttackCoroutine);
            IsLightAttack = false;
            IsHeavyAttack = false;
            IsSheating= false;
            IsUnsheating= false;
            IsDuringAttack=false;
            PlayerGrAttackState.sw = false;
            StartCoroutine(RollingCoroutine());
           
            

        }


    }
    

    IEnumerator RollingCoroutine()
    {
        IsRolling = true;
        yield return new WaitForSecondsRealtime(0.25f * PlayerController.animatorTimeVector); 
        IsRolling= false;
        extraRollingWait = true;
        yield return new WaitForSecondsRealtime(0.15f * PlayerController.animatorTimeVector);
        extraRollingWait= false;
    }

    void OnJumping(InputValue input)
    {
        //Debug.Log("Jumping");
        jumpInput = input.Get<float>();
        //Debug.Log(jumpInput);
        if (jumpInput != 0)
        {
            CommandHandler.HandleCommand(new JumpInput());
        }


        
        //Debug.Log("Space Value:" + input.Get<float>());


    }
    public static void JumpExecute()
    {
        
        if (IsGroundedPlayer && jumpInput != 0) { IsJumping = true; IsSpacePressing = true; }
    }

    void OnToLightningAura(InputValue input)
    {

        //Debug.Log("Aura Input:" + input.Get<float>());
        if (IsLightningAura && input.Get<float>() == 1) { IsLightningAura = false; }
        else if(!IsLightningAura && input.Get<float>() == 1) { IsLightningAura = true; IsWindAura = false; if (!isLightningCoroutineStarted) StartCoroutine(PlayTransition(2)); }
        Debug.Log("Aura Input:" + IsLightningAura);
    }

    IEnumerator PlayTransition(int i)
    {
        isLightningCoroutineStarted = true;
        if (i == 2)AuraTransitionController.AnimatorForAuraTransition.Play("LT for Idle");
        else if(i == 1) AuraTransitionController.AnimatorForAuraTransition.Play("WT for Idle");
        yield return new WaitForSeconds(0.25f);
        AuraTransitionController.AnimatorForAuraTransition.Play("Nothing");
        isLightningCoroutineStarted = false;
    }

    void OnToWindAura(InputValue input)
    {

        //Debug.Log("Aura Input:" + input.Get<float>());
        if (IsWindAura && input.Get<float>() == 1) { IsWindAura = false; }
        else if (!IsWindAura && input.Get<float>() == 1) { IsWindAura = true; IsLightningAura = false; if (!isWindCoroutineStarted) StartCoroutine(PlayTransition(1)); }
        Debug.Log("Aura Input:" + IsWindAura);
    }


    void OnLightAttack()
    {
        if (IsGroundedPlayer && Stamina >= 3)
        {
            //Debug.Log("HeavyAttack");
            CommandHandler.HandleCommand(lightAttackInput);
            //Debug.Log(heavyAttackInput);

        }


    }
    public void LightAttackExecution()
    {
        lightAttackCoroutine=StartCoroutine(LightAttack(LightAttackNumber));
        
    }
    IEnumerator LightAttack(int count)
    {
        //Debug.Log("In Coroutine");
        
        if (LightAttackNumber == 1)
        {
            //Debug.Log("AttackNUmber1");
            
            IsLightAttack = true;
            if (MoveInput.x != 0) { transform.localScale = new Vector2(Mathf.Sign(MoveInput.x), transform.localScale.y); }
         
            IsDuringAttack = true;
            yield return new WaitForSecondsRealtime(0.25f * PlayerController.animatorTimeVector);
            CommandHandler.ResetNext();
            lightAttackCollider.enabled = true;
            if (ComboCounter < 60) ComboCounter++;
            yield return new WaitForSecondsRealtime(0.083f * PlayerController.animatorTimeVector);
            lightAttackCollider.enabled = false;
            GameEvents.gameEvents.OnDisablingAttackCollider(gameObject);
            yield return new WaitForSecondsRealtime(0.167f * PlayerController.animatorTimeVector);
            IsDuringAttack = false;
            LightAttackNumber++;
            PlayerGrAttackState.sw = false;
            IsLightAttack = false;
         

        }
        else if (LightAttackNumber == 2)
        {
         
            IsLightAttack = true;
            if (MoveInput.x != 0) { transform.localScale = new Vector2(Mathf.Sign(MoveInput.x), transform.localScale.y); }
            IsDuringAttack = true;
            yield return new WaitForSecondsRealtime(0.25f * PlayerController.animatorTimeVector);
            CommandHandler.ResetNext();
            lightAttackCollider.enabled = true;
            if (ComboCounter < 60) ComboCounter++;
            yield return new WaitForSecondsRealtime(0.083f * PlayerController.animatorTimeVector);
            lightAttackCollider.enabled = false;
            GameEvents.gameEvents.OnDisablingAttackCollider(gameObject);
            yield return new WaitForSecondsRealtime(0.167f * PlayerController.animatorTimeVector);
            IsDuringAttack = false;
            LightAttackNumber++;
            PlayerGrAttackState.sw = false;
           
            IsLightAttack = false;
        }

        else if (LightAttackNumber == 3)
        {
            
            IsLightAttack = true;
            if (MoveInput.x != 0) { transform.localScale = new Vector2(Mathf.Sign(MoveInput.x), transform.localScale.y); }
            IsDuringAttack = true;
            yield return new WaitForSecondsRealtime(0.25f * PlayerController.animatorTimeVector);
            CommandHandler.ResetNext();
            lightAttackCollider.enabled = true;
            if (ComboCounter < 60) ComboCounter++;
            yield return new WaitForSecondsRealtime(0.083f * PlayerController.animatorTimeVector);
            lightAttackCollider.enabled = false;
            GameEvents.gameEvents.OnDisablingAttackCollider(gameObject);
            yield return new WaitForSecondsRealtime(0.167f * PlayerController.animatorTimeVector);
            IsDuringAttack = false;
            LightAttackNumber = 1;
            PlayerGrAttackState.sw = false;
            IsLightAttack = false;
        }
        else if (LightAttackNumber == 4)
        {
            
            IsLightAttack = true;
            if (MoveInput.x != 0) { transform.localScale = new Vector2(Mathf.Sign(MoveInput.x), transform.localScale.y); }
            IsDuringAttack = true;
            yield return new WaitForSecondsRealtime(0.25f * PlayerController.animatorTimeVector);
            CommandHandler.ResetNext();
            lightAttackCollider.enabled = true;
            if (ComboCounter < 60) ComboCounter++;
            yield return new WaitForSecondsRealtime(0.083f * PlayerController.animatorTimeVector);
            lightAttackCollider.enabled = false;
            GameEvents.gameEvents.OnDisablingAttackCollider(gameObject);
            yield return new WaitForSecondsRealtime(0.167f * PlayerController.animatorTimeVector);
            IsDuringAttack = false;
            LightAttackNumber++;
            PlayerGrAttackState.sw = false;
            IsLightAttack = false;
        }

        else if (LightAttackNumber >= 5)
        {
           
            IsLightAttack = true;
            if (MoveInput.x != 0) { transform.localScale = new Vector2(Mathf.Sign(MoveInput.x), transform.localScale.y); }
            IsDuringAttack = true;
            yield return new WaitForSecondsRealtime(0.25f * PlayerController.animatorTimeVector);
            CommandHandler.ResetNext();
            lightAttackCollider.enabled = true;
            if (ComboCounter < 60) ComboCounter++;
            yield return new WaitForSecondsRealtime(0.083f * PlayerController.animatorTimeVector);
            lightAttackCollider.enabled = false;
            GameEvents.gameEvents.OnDisablingAttackCollider(gameObject);
            yield return new WaitForSecondsRealtime(0.167f * PlayerController.animatorTimeVector);
            IsDuringAttack= false;
            LightAttackNumber= 1;
            PlayerGrAttackState.sw = false;
            IsLightAttack = false;
        }
    }


    void OnHeavyAttack()
    {
        
        if (IsGroundedPlayer && Stamina>=5)
        {
            //Debug.Log("HeavyAttack");
            CommandHandler.HandleCommand(heavyAttackInput);
            //Debug.Log(heavyAttackInput);
          
        }
        

    }

    public void HeavyAttackExecution()
    {
       
       heavyAttackCoroutine =  StartCoroutine(HeavyAttack(AttackNumber));
        
    }
    IEnumerator HeavyAttack(int count)
    {
        //Debug.Log("In Coroutine");
        if (AttackNumber == 1)
        {
            IsHeavyAttack = true;
            
            //Debug.Log("AttackNUmber1");
            yield return new WaitForSecondsRealtime(0.1f* PlayerController.animatorTimeVector);
            PlayerGrAttackState.isLeaping = true;
            IsDuringAttack= true;
            yield return new WaitForSecondsRealtime(0.4f * PlayerController.animatorTimeVector);
            IsDuringAttack= false;
            PlayerGrAttackState.sw = false;
            if (heavyAttackInput != CommandHandler.ShowNext() && lightAttackInput != CommandHandler.ShowNext() && specialAttackInput != CommandHandler.ShowNext())
            {
                IsSheating = true;
                yield return new WaitForSeconds(0.2f * PlayerController.animatorTimeVector);
                CommandHandler.ResetNext();
                yield return new WaitForSeconds(0.217f * PlayerController.animatorTimeVector);
                IsSheating = false;
            }
            IsHeavyAttack = false;
        }
        else if (AttackNumber == 2)
        {
            

            IsHeavyAttack = true;
            yield return new WaitForSecondsRealtime(0.1f * PlayerController.animatorTimeVector);
            IsDuringAttack = true;
            yield return new WaitForSecondsRealtime(0.483f * PlayerController.animatorTimeVector);
            IsDuringAttack= false;
            PlayerGrAttackState.sw = false;
            if (heavyAttackInput != CommandHandler.ShowNext() && lightAttackInput != CommandHandler.ShowNext() && specialAttackInput != CommandHandler.ShowNext())
            {
                IsSheating = true;
                yield return new WaitForSeconds(0.2f * PlayerController.animatorTimeVector);
                CommandHandler.ResetNext();
                yield return new WaitForSeconds(0.217f * PlayerController.animatorTimeVector);
                IsSheating = false;
            }
            IsHeavyAttack = false;
        }

        else if (AttackNumber >= 3)
        {
           
            IsHeavyAttack = true;
            yield return new WaitForSecondsRealtime(0.1f * PlayerController.animatorTimeVector);
            IsDuringAttack= true;
            yield return new WaitForSecondsRealtime(0.317f * PlayerController.animatorTimeVector);
            IsDuringAttack= false;
            PlayerGrAttackState.sw = false;
            if (heavyAttackInput != CommandHandler.ShowNext() && lightAttackInput != CommandHandler.ShowNext() && specialAttackInput != CommandHandler.ShowNext())
            {
                IsSheating = true;
                yield return new WaitForSeconds(0.2f * PlayerController.animatorTimeVector);
                CommandHandler.ResetNext();
                yield return new WaitForSeconds(0.217f * PlayerController.animatorTimeVector);
                IsSheating = false;
            }
            IsHeavyAttack = false;
        }
        
    }


    public static void ResetAttackNumber(int which)
    {
        if (which == 0)
        {
            isResettingCombo = true;
        }
        else if (which == 1)
        {
            isResettingAttack= true;
        }
    }
    IEnumerator ResettingLightAttack(int resetType)
    {
      
       if(resetType == 1)
        {
            yield return new WaitForSecondsRealtime(0.1f * PlayerController.animatorTimeVector);

            if (!IsLightAttack) LightAttackNumber = 1;
        }
       else if(resetType == 0)
        {
            float counter = 0;
            float resetTime = 5f;
            while(counter < resetTime)
            {
                if (IsLightAttack)
                {
                    break;
                }
                yield return new WaitForSecondsRealtime(0.25f);
                counter += 0.25f;
            }
            if (counter == resetTime) ComboCounter = 0;
        }
       
    }
    public  static void IncreaseAttackNumber(int which)
    {
        if (which == 0)
        {
            AttackNumber++;
        }
        else if (which == 1)
        {
            LightAttackNumber++;
        }
    }
    public static void ResetStamina()
    {
        Stamina = 15;
    }
    public static void DecreaseStamina(int number)
    {
        Stamina -= number;
    }
    public static void AdjustAttackNumber(int which)
    {
        if (which == 0)
        {

            AttackNumber += (int)Mathf.Round((LightAttackNumber - 1f) / 2f);
            //Debug.Log("Light attack Number :" + LightAttackNumber);
            //Debug.Log((int)Mathf.Round((LightAttackNumber -0.99f) / 2f));
            
        }
        else if (which == 1)
        {
            LightAttackNumber += (AttackNumber - 1) * 2;
        }
    }

    void OnSpecialAttack()
    {
        //CommandHandler.HandleCommand(new SpecialAttackInput());
    }

    public void SpecialAttackExecution()
    {
        StartCoroutine(SpecialAttack(SpecialAttackNumber));
    }

    IEnumerator SpecialAttack(int number)
    {
        IsSpecialAttack= true;
        yield return new WaitForSecondsRealtime(0f * PlayerController.animatorTimeVector);
        IsSpecialAttack= false;


    }
    protected virtual void TakingDamage(GameObject receiver, GameObject sender, Collider2D otherCollider, int attakVer)
    {
        if (receiver == gameObject)
        {

            //Debug.Log("GettingDmg");

            if (!IsKnocbacking && !IsRolling) 
            {

                if(!IsDuringAttack) StartCoroutine(Knockback());

                if(otherCollider.GetComponentInParent<Rigidbody2D>().transform.localScale.x == 1f)
                {
                    transform.localScale = new Vector2(-1f,transform.localScale.y);
                }

                else if(otherCollider.GetComponentInParent<Rigidbody2D>().transform.localScale.x == -1f)
                {
                    transform.localScale = new Vector2(1f, transform.localScale.y);
                }


             } 
            


        }
    }


    IEnumerator Knockback()
    {
        IsKnocbacking = true;
        yield return new WaitForSeconds(knockbackDuration);
        IsKnocbacking= false;


    }


  

   

}



