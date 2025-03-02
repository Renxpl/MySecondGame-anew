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
    public static PlayerKnockbackState playerParryState { get; private set; }
    public static PlayerAirborneAttackState playerAAstate { get; private set; }

    public static bool IsGroundedPlayer { get; private set; }
    public static bool CanDoActionDuringJump { get; private set; }
    public static bool IsRolling { get; private set; }
    public static bool IsJumping { get; private set; }
    public static bool IsSpacePressing { get; private set; }
    public static bool IsJumpingUp { get; private set; }
    public static bool IsParrying { get; private set; }
    public static float JumpTime { get; private set; }
    public static float JumpSpeed { get; private set; }
    public static bool IsLightningAura { get; private set; }
    public static bool IsWindAura { get; private set; }
    public static int HP { get; private set; }
    public static int Stance { get; private set; }
    public static bool SwitchAACollider { get; set; }

    public static bool IsHeavyAttack { get; private set; }
    public static bool IsLightAttack { get; private set; }

    public static bool IsSpecialAttack { get; private set; }
    public static bool IsKnocbacking { get; private set; }
    public static bool IsSheating { get; private set; }
    public static bool IsUnsheating { get; private set; }
    public static bool IsDuringAttack { get; private set; }
    public static int ComboCounter { get; private set; }
    public static float AttackSpeed { get; private set; }
    public static bool IsAirborneAttack { get; set; }

    public static LayerMask groundLayer;
    public static GameObject player;


    public AnimationCurve jumpCurve;

    float parryInput = 0;

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
    public static bool IsHitting { get;  set; }
    //next input will be handled by bools
    [SerializeField] float knockbackDuration;
    [Header("Colliders")]
    [SerializeField] Collider2D lightAttackCollider;
    [SerializeField] Collider2D HA1Collider;
    [SerializeField] Collider2D HA2Collider;
    [SerializeField] Collider2D HA3Collider;
    [SerializeField] Collider2D SACollider;
    [SerializeField] Collider2D AACollider;

    Coroutine lightAttackCoroutine;
    Coroutine heavyAttackCoroutine;
    Coroutine rollingCoroutine;
    Coroutine resettingAttack;
    Coroutine resettingCombo;
    bool isHittingLocker;

    
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
        playerAAstate = new PlayerAirborneAttackState();
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
        HA1Collider.enabled = false;
        HA2Collider.enabled = false;
        HA3Collider.enabled = false;
        SACollider.enabled = false;
        AACollider.enabled = false;
        HP = 10;
        Stance = 5;
        
    }

    // Update is called once per frame
    void Update()
    {

        IsGroundedPlayer = Physics2D.Raycast(player.transform.position, Vector2.down, 1f, groundLayer);
        Debug.DrawRay(player.transform.position, Vector2.down * 1f, IsGroundedPlayer ? Color.green : Color.red);
        CanDoActionDuringJump = Physics2D.Raycast(player.transform.position, Vector2.down, 2.25f,groundLayer);
        if (IsGroundedPlayer)
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
            AttackSpeed = 1.15f;
        }
        else if (ComboCounter < 30)
        {
            AttackSpeed = 1.35f;
        }
        else
        {
            AttackSpeed = 1.6f;
        }

        if (IsHitting)
        {


            if (!isHittingLocker)
            {
                StartCoroutine(ComboIncrement());
                IsHitting = false;
                if(ComboCounter < 30) ComboCounter++;
            }


        }

        if (SwitchAACollider)
        {
            if (AACollider.enabled)
            {
                AACollider.enabled = false;
            }
            else
            {
                AACollider.enabled = true;
            }
            SwitchAACollider= false;
        }



    }
    IEnumerator ComboIncrement()
    {
        isHittingLocker= true;
        yield return new WaitForSeconds(0.15f);
        isHittingLocker = false;
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
            if (IsLightAttack)
            {
                if (lightAttackCoroutine != null) StopCoroutine(lightAttackCoroutine);
                IsLightAttack = false;
                lightAttackCollider.enabled = false;
                IsDuringAttack = false;
            }
            if (heavyAttackCoroutine != null) StopCoroutine(heavyAttackCoroutine);
            IsLightAttack = false;
            IsHeavyAttack = false;
            IsSheating= false;
            IsUnsheating= false;
            IsDuringAttack=false;
            PlayerGrAttackState.sw = false;
            rollingCoroutine =  StartCoroutine(RollingCoroutine());
           
            

        }


    }
    

    IEnumerator RollingCoroutine()
    {
        IsRolling = true;
        yield return new WaitForSecondsRealtime(0.25f * PlayerController.animatorTimeVector); 
        IsRolling= false;
        extraRollingWait = true;
        yield return new WaitForSecondsRealtime(0.175f * PlayerController.animatorTimeVector);
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
        if (IsGroundedPlayer)
        {
            //Debug.Log("HeavyAttack");
            if(IsRolling || extraRollingWait)
            {
                LightAttackNumber = 5;
            }
            CommandHandler.HandleCommand(lightAttackInput);
            //Debug.Log(heavyAttackInput);

        }
        else
        {
            IsAirborneAttack = true;
            SwitchAACollider = true;

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
         
            
            yield return new WaitForSecondsRealtime(0.25f * PlayerController.animatorTimeVector);
            IsDuringAttack = true;
            CommandHandler.ResetNext();
            PlayerController.PlayerRB.WakeUp();
            lightAttackCollider.enabled = true;
       
            yield return new WaitForSecondsRealtime(0.15f * PlayerController.animatorTimeVector);
            lightAttackCollider.enabled = false;
            yield return new WaitForSecondsRealtime(0.1f * PlayerController.animatorTimeVector);
            IsDuringAttack = false;
            LightAttackNumber++;
            PlayerGrAttackState.sw = false;
            IsLightAttack = false;
         

        }
        else if (LightAttackNumber == 2)
        {
         
            IsLightAttack = true;
            if (MoveInput.x != 0) { transform.localScale = new Vector2(Mathf.Sign(MoveInput.x), transform.localScale.y); }
            
            yield return new WaitForSecondsRealtime(0.25f * PlayerController.animatorTimeVector);
            IsDuringAttack = true;
            CommandHandler.ResetNext();
            PlayerController.PlayerRB.WakeUp();
            lightAttackCollider.enabled = true;
         
            yield return new WaitForSecondsRealtime(0.15f * PlayerController.animatorTimeVector);
            lightAttackCollider.enabled = false;
            yield return new WaitForSecondsRealtime(0.1f * PlayerController.animatorTimeVector);
            IsDuringAttack = false;
            LightAttackNumber++;
            PlayerGrAttackState.sw = false;
           
            IsLightAttack = false;
        }

        else if (LightAttackNumber == 3)
        {
            
            IsLightAttack = true;
            if (MoveInput.x != 0) { transform.localScale = new Vector2(Mathf.Sign(MoveInput.x), transform.localScale.y); }
            
            yield return new WaitForSecondsRealtime(0.25f * PlayerController.animatorTimeVector);
            IsDuringAttack = true;
            CommandHandler.ResetNext();
            PlayerController.PlayerRB.WakeUp();
            lightAttackCollider.enabled = true;
           
            yield return new WaitForSecondsRealtime(0.15f * PlayerController.animatorTimeVector);
            lightAttackCollider.enabled = false;
            yield return new WaitForSecondsRealtime(0.1f * PlayerController.animatorTimeVector);
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
            yield return new WaitForSecondsRealtime(0.15f * PlayerController.animatorTimeVector);
            lightAttackCollider.enabled = false;
            GameEvents.gameEvents.OnDisablingAttackCollider(gameObject);
            yield return new WaitForSecondsRealtime(0.1f * PlayerController.animatorTimeVector);
            IsDuringAttack = false;
            LightAttackNumber++;
            PlayerGrAttackState.sw = false;
            IsLightAttack = false;
        }

        else if (LightAttackNumber >= 5)
        {

            IsLightAttack = true;
            if (MoveInput.x != 0) { transform.localScale = new Vector2(Mathf.Sign(MoveInput.x), transform.localScale.y); }

            yield return new WaitForSecondsRealtime(0.25f * PlayerController.animatorTimeVector);
            IsDuringAttack = true;
            CommandHandler.ResetNext();
            PlayerController.PlayerRB.WakeUp();
            lightAttackCollider.enabled = true;
           
            yield return new WaitForSecondsRealtime(0.15f * PlayerController.animatorTimeVector);
            lightAttackCollider.enabled = false;
            yield return new WaitForSecondsRealtime(0.1f * PlayerController.animatorTimeVector);
            IsDuringAttack = false;
            LightAttackNumber = 1;
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
        //Debug.Log("In Coroutine");,
        if (ComboCounter < 10)
        {
            yield return new WaitForSecondsRealtime(0f * PlayerController.animatorTimeVector);
        }
        else if (ComboCounter <20)
        {
            IsHeavyAttack = true;
            IsDuringAttack = true;
            AttackNumber = 1;
            CommandHandler.ResetNext();
            PlayerController.PlayerRB.WakeUp();
            HA1Collider.enabled = true;
            //Debug.Log("AttackNUmber1");

            yield return new WaitForSecondsRealtime(0.5f * PlayerController.animatorTimeVector);
            HA1Collider.enabled = false;
            IsDuringAttack = false;
            PlayerGrAttackState.sw = false;
            
            IsHeavyAttack = false;
            ComboCounter = 0;
        }
        else if (ComboCounter < 30)
        {

            AttackNumber = 2;
            IsHeavyAttack = true;
            IsDuringAttack = true;
            CommandHandler.ResetNext();
            PlayerController.PlayerRB.WakeUp();
            HA1Collider.enabled = true;

    
            yield return new WaitForSecondsRealtime(0.583f * PlayerController.animatorTimeVector);
            HA1Collider.enabled = false;
            IsDuringAttack = false;
            PlayerGrAttackState.sw = false;
            
            IsHeavyAttack = false;
            ComboCounter = 0;
        }

        else if (ComboCounter < 40)
        {
            AttackNumber = 3;
            IsHeavyAttack = true;
           
            IsDuringAttack= true;
            CommandHandler.ResetNext();
            PlayerController.PlayerRB.WakeUp();
            HA1Collider.enabled = true;
            yield return new WaitForSecondsRealtime(0.75f * PlayerController.animatorTimeVector);
            HA1Collider.enabled = false;
            IsDuringAttack = false;
            PlayerGrAttackState.sw = false;
            
            IsHeavyAttack = false;
            ComboCounter = 0;
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
    public static void SetLightAttackNumber(int number)
    {
        LightAttackNumber = number;
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
        if (IsGroundedPlayer)
        {
            CommandHandler.HandleCommand(new SpecialAttackInput());
        }
    }

    public void SpecialAttackExecution()
    {
        StartCoroutine(SpecialAttack(SpecialAttackNumber));
    }

    IEnumerator SpecialAttack(int number)
    {
        IsSpecialAttack= true;
        IsDuringAttack = true;
        SACollider.enabled = true;
        yield return new WaitForSecondsRealtime(0.75f * PlayerController.animatorTimeVector);
        SACollider.enabled = false;
        IsDuringAttack = false;
        PlayerGrAttackState.sw = false;
        IsSpecialAttack= false;


    }
    protected virtual void TakingDamage(GameObject receiver, GameObject sender, Collider2D otherCollider, int attakVer)
    {
        if (receiver == gameObject)
        {
            if(HP > 0) HP--;
            if(Stance>0) Stance--;

            Debug.Log("PlayerHp:"+ HP+"  Player Stance:"+ Stance);
            
            if (!IsKnocbacking) 
            {
                //Debug.Log("Player Took Dmg");
                if (!IsDuringAttack) 
                {

                    if (Stance == 0) 
                    {
                        StartCoroutine(Knockback());
                        if (otherCollider.GetComponentInParent<Rigidbody2D>().transform.localScale.x == 1f)
                        {
                            transform.localScale = new Vector2(-1f, transform.localScale.y);
                        }

                        else if (otherCollider.GetComponentInParent<Rigidbody2D>().transform.localScale.x == -1f)
                        {
                            transform.localScale = new Vector2(1f, transform.localScale.y);
                        }

                    }
                   
                }
                else
                {
                    if(Stance == 0)
                    {
                        Stance = 5;
                    }



                }

                

             } 
            


        }
    }


    IEnumerator Knockback()
    {
        
        IsKnocbacking = true;
        if (IsLightAttack)
        {
            if (lightAttackCoroutine != null) StopCoroutine(lightAttackCoroutine);
            IsLightAttack = false;
            lightAttackCollider.enabled = false;
            IsDuringAttack = false;
            PlayerGrAttackState.sw = false;
        }
        if (IsRolling)
        {
            if (rollingCoroutine != null) StopCoroutine(rollingCoroutine);
            IsRolling = false;
            extraRollingWait = false;
        }
        yield return new WaitForSeconds(knockbackDuration);
        IsKnocbacking= false;
        Stance = 5;


    }


  
   
   

}



