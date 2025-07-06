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
    public static PlayerParryState playerParryState { get; private set; }
    public static PlayerAirborneAttackState playerAAstate { get; private set; }
    public static PlayerWallClimbState playerWCState { get; private set; }
    public static PlayerEdgeClimbState playerECState { get; private set; }

    public static bool IsGroundedPlayer { get; private set; }
    public static bool IsLeftWallClimbing { get; private set; }
    public static bool IsRightWallClimbing { get; private set; }



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
    public static float Stance { get; private set; }
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
    public static ParryInput parryInp;
    public static SpecialAttackInput specialAttackInput;
    public static RollInput rollInput;
    public static AAInput aaInput;
    public static bool isRollingAirborne;
    public static int AttackNumber { get; private set; }
    public static int LightAttackNumber { get; private set; }
    public static int SpecialAttackNumber { get; private set; }
    public static int Stamina { get; private set; }
    public static bool IsHitting { get; set; }
    //next input will be handled by bools
    [SerializeField] float knockbackDuration;
    [Header("Colliders")]
    [SerializeField] Collider2D lightAttackCollider;
    [SerializeField] Collider2D HA1Collider;
    [SerializeField] Collider2D HA2Collider;
    [SerializeField] Collider2D HA3Collider;
    [SerializeField] Collider2D SACollider;
    [SerializeField] Collider2D AACollider;
    Collider2D getDmgCollider;

    Coroutine lightAttackCoroutine;
    Coroutine heavyAttackCoroutine;
    Coroutine rollingCoroutine;
    Coroutine resettingAttack;
    Coroutine resettingCombo;
    Coroutine comboIncrement;
    Coroutine aaCombo;

    static public float SpecialAttackBar { get; set; }
    static public int AACounter { get; set; }
    static public bool AAInit { get; set; }
    bool firstTimeGrounded;
    static public float SpecialAttackBarTiming = 0;
    bool isTakenDmg = false;
    float isTakenDmgCounter = 0;
    bool lockCounter;
    public static bool Gravity0 { get; set; }
    public static bool ResetAA { get; set; }
    public static bool LockAA { get; set; }

    float timePassedForAA = 0f;
    float timePassedForOpenAA = 0f;
    [SerializeField] float timeForAALock;

    bool lockDashAirborne = false;

    bool airborneKnockbackDebug;

    bool firstTimeWalled = false;
    bool firstTimeOnEdge = false;


    public static bool CanJumpFromLWall { get; set; }
    public static bool CanJumpFromRWall { get; set; }
    public static bool LockSpriteDirection { get; set; }

    float timeForLWall = 0f;
    float timeForRWall = 0f;
    float timeForSpriteLock = 0f;
    float timeToPassOnwalls = 0.75f;

    public float staRegenTime;


    public static float TimePassedOnWalls { get; set; }
    float timeToGetWalls;

    float addToSABAr;
    bool firstTimeStopEv;
    public static bool StopEverythingPlayer { get; private set; }



    Collider2D parryHB;
    void Awake()
    {
        GroundedStateForPlayer = new PlayerGroundedState();
        AirborneStateForPlayer = new PlayerAirborneState();
        IdleStateForPlayer = new PlayerIdleState();
        WalkStateForPlayer = new PlayerWalkState();
        RunStateForPlayer = new PlayerRunState();
        RollStateForPlayer = new PlayerRollState();
        AttackModeStateForPlayer = new PlayerAttackModeState();
        JumpStateForPlayer = new PlayerJumpState();
        GrAttackState = new PlayerGrAttackState();
        heavyAttackInput = new HeavyAttackInput();
        lightAttackInput = new LightAttackInput();
        parryInp = new ParryInput();
        specialAttackInput = new SpecialAttackInput();
        rollInput = new RollInput();
        playerKbState = new PlayerKnockbackState();
        playerAAstate = new PlayerAirborneAttackState();
        aaInput = new AAInput();
        playerWCState = new PlayerWallClimbState();
        playerECState = new PlayerEdgeClimbState();
        playerParryState = new PlayerParryState();
    }

    // Start is called before the first frame update
    void Start()
    {
        getDmgCollider = transform.Find("Get Dmg Hitbox").GetComponent<Collider2D>();
        groundLayer = LayerMask.GetMask("Ground");
        player = PlayerController.PlayerRB.gameObject;
        IsLightningAura = false;
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
        GameEvents.gameEvents.onComboIncrement += ComboAdjuster;
        SpecialAttackBar = 32;
        AACounter = 0;
        parryHB = transform.Find("ParryHitbox").GetComponent<Collider2D>();
        parryHB.enabled = false;

    }

    // Update is called once per frame

    public static float CanParry { get; private set; }
    void Update()
    {

        IsGroundedPlayer = Physics2D.Raycast(player.transform.position, Vector2.down, 1.1f, groundLayer) || Physics2D.Raycast(player.transform.position + new Vector3(0.25f, 0f, 0f), Vector2.down, 1.1f, groundLayer) || Physics2D.Raycast(player.transform.position - new Vector3(0.25f, 0f, 0f), Vector2.down, 1.1f, groundLayer);
        IsRightWallClimbing = Physics2D.Raycast(player.transform.position, Vector2.right, 0.65f, groundLayer) && !IsGroundedPlayer && TimePassedOnWalls < timeToPassOnwalls;
        IsLeftWallClimbing = Physics2D.Raycast(player.transform.position, Vector2.left, 0.65f, groundLayer) && !IsGroundedPlayer && TimePassedOnWalls < timeToPassOnwalls;
        Debug.DrawRay(player.transform.position + new Vector3(0.25f, 0f, 0f), Vector2.down * 1.1f, IsGroundedPlayer ? Color.green : Color.red);
        Debug.DrawRay(player.transform.position - new Vector3(0.25f, 0f, 0f), Vector2.down * 1.1f, IsGroundedPlayer ? Color.green : Color.red);
        Debug.DrawRay(player.transform.position, Vector2.down * 1.1f, IsGroundedPlayer ? Color.green : Color.red);
        Debug.DrawRay(player.transform.position, Vector2.right * 0.65f, IsRightWallClimbing ? Color.green : Color.blue);
        Debug.DrawRay(player.transform.position, Vector2.left * 0.65f, IsLeftWallClimbing ? Color.green : Color.cyan);

        CanDoActionDuringJump = Physics2D.Raycast(player.transform.position, Vector2.down, 2.25f, groundLayer);





        if (!IsRightWallClimbing && !IsLeftWallClimbing)
        {
            firstTimeWalled = true;
        }
        else
        {
            lockDashAirborne = false;
        }




        if (!IsGroundedPlayer)
        {
            firstTimeGrounded = true;

        }
        else
        {
            TimePassedOnWalls = 0f;
            AACounter = 0;
            lockDashAirborne = false;

        }
        bool canDoWallJump = (IsRightWallClimbing || IsLeftWallClimbing) && firstTimeWalled;




        if (canDoWallJump)
        {
            IsJumping = false;
            JumpTime = 0;
            firstTimeWalled = false;
        }

        if (IsGroundedPlayer && firstTimeGrounded)
        {
            IsJumping = false;
            JumpTime = 0;
            firstTimeGrounded = false;
        }
        if (UIManagement.IsPaused)
        {
            StopEverythingPlayer = true;
        }
        if (StopEverythingPlayer)
        {
           



            MoveInput = Vector2.zero;
            if (firstTimeStopEv)
            {
                PlayerController.PlayerRB.velocity = Vector2.zero;
                firstTimeStopEv = false;
            }

            CommandHandler.ResetNext();
            if (UIManagement.IsPaused) return;
            if (!PlayerController.IsInteractable) StopEverythingPlayer = false;
            return;
        }
        else
        {
            firstTimeStopEv = true;
        }

        if (jumpInput != 0 && JumpTime <= 0.5f && IsSpacePressing)
        {
            if (Time.timeScale < 1)
            {
                JumpTime += (1 / Time.timeScale) * Time.deltaTime;
                IsSpacePressing = true;
                JumpSpeed = jumpCurve.Evaluate(JumpTime);
            }
            else
            {
                JumpTime += Time.deltaTime;
                IsSpacePressing = true;
                JumpSpeed = jumpCurve.Evaluate(JumpTime);
            }


        }
        else
        {
            IsSpacePressing = false;
            IsJumping = false;
        }

        IsJumpingUp = IsSpacePressing;


        if (isResettingAttack)
        {
            if (resettingAttack != null) StopCoroutine(resettingAttack);
            resettingAttack = StartCoroutine(ResettingLightAttack(1));

            isResettingAttack = false;
        }
        if (isResettingCombo)
        {
            if (resettingCombo != null) StopCoroutine(resettingCombo);
            resettingCombo = StartCoroutine(ResettingLightAttack(0));

            isResettingCombo = false;
        }

        if (ComboCounter <= 10)
        {
            AttackSpeed = 1f + ComboCounter * 0.010f;
        }
        else if (ComboCounter <= 20)
        {
            AttackSpeed = 1f + ComboCounter * 0.014f;
        }
        else if (ComboCounter <= 30)
        {
            AttackSpeed = 1f + ComboCounter * 0.02f;
            //Debug.Log(AttackSpeed);
        }
        else
        {
            AttackSpeed = 1.6f;
        }


        /*



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
    }*/

        //if (ComboCounter < 30) ComboCounter++;

        //SpecialAttackBarControl
        SpecialAttackBarControl();

        //Stance Control
        StanceControl();

        DebugCheats();


        AAReset();

        OpenAA();

        HandlingTimeForWalls();

        HandleSpriteLock();

        FallingOfTheWall();

        if (!IsParrying)
        {
            CanParry += Time.deltaTime;
        }
        else
        {
            CanParry = 0f;
        }

        if (ComboCounter < 0) ComboCounter = 0;
    }

    void FallingOfTheWall()
    {
        if (TimePassedOnWalls >= timeToPassOnwalls)
        {

            timeToGetWalls = Time.deltaTime;



        }
        else
        {
            timeToGetWalls = 0f;
        }

        if (timeToGetWalls > 1f)
        {
            TimePassedOnWalls = 0f;
        }

    }

    void HandleSpriteLock()
    {

        if (LockSpriteDirection)
        {
            timeForSpriteLock += Time.deltaTime;


        }

        else
        {
            timeForSpriteLock = 0f;
        }

        if (timeForSpriteLock > 0.25f)
        {
            LockSpriteDirection = false;


        }


    }

    void HandlingTimeForWalls()
    {

        if (!CanJumpFromLWall)
        {
            timeForLWall += Time.deltaTime;
        }

        if (timeForLWall >= 1f || (!CanJumpFromRWall && timeForLWall > timeForRWall))
        {
            CanJumpFromLWall = true;
            timeForLWall = 0;


        }

        if (!CanJumpFromRWall)
        {
            timeForRWall += Time.deltaTime;
        }

        if (timeForRWall >= 1f || (!CanJumpFromLWall && timeForRWall > timeForLWall))
        {
            CanJumpFromRWall = true;
            timeForRWall = 0;

        }





    }


    void DebugCheats()
    {
        /*
        if (ComboCounter < 20)
        {
            ComboCounter++;
        }
        */
        /*
        if (ComboCounter < 30)
        {
            ComboCounter++;
        }
        */

        if (airborneKnockbackDebug)
        {

            StartCoroutine(Knockback());
            airborneKnockbackDebug = false;
        }




    }
    //Debug Input
    void OnDebug()
    {

        //airborneKnockbackDebug = true;

    }

    void OpenAA()
    {



        if (LockAA)
        {
            timePassedForOpenAA += Time.deltaTime;


        }


        if (timePassedForOpenAA > timeForAALock)
        {


            LockAA = false;
            timePassedForOpenAA = 0f;
        }

    }

    void AAReset()
    {


        if (ResetAA)
        {
            timePassedForAA += Time.deltaTime;


        }
        else
        {
            timePassedForAA = 0f;
        }

        if (timePassedForAA > 0.3f)
        {
            AACounter = 0;

            ResetAA = false;
        }




    }

    void SpecialAttackBarControl()
    {
        SpecialAttackBarTiming += Time.deltaTime;
        if (SpecialAttackBarTiming >= 7.5f)
        {
            if (SpecialAttackBar < 32f)
            {
                addToSABAr += Time.deltaTime;
                if (addToSABAr >= 0.16f)
                {
                    SpecialAttackBar += 0.16f;
                    addToSABAr = 0f;
                }
            }
        }
        else
        {
            addToSABAr = 0f;
        }
        if (SpecialAttackBar > 32)
        {
            SpecialAttackBar = 32;



        }
    }
    void StanceControl()
    {
        float regenTimeLock;
        if (Stance <= 0)
        {
            regenTimeLock = 1f;
        }
        else
        {
            regenTimeLock = staRegenTime;
        }

        if (isTakenDmg)
        {

            isTakenDmgCounter += Time.deltaTime;


            if (isTakenDmgCounter > regenTimeLock)
            {
                if (Stance < 5 && !lockCounter) Stance += 0.8f * Time.deltaTime;
                else if (Stance < 5 && lockCounter) Stance += 2f * Time.deltaTime;

                if (Stance > 5) Stance = 5;
            }

            if (Stance <= 0)
            {
                lockCounter = true;
            }

        }
        if (Stance == 5)
        {
            isTakenDmg = false;
            lockCounter = false;
        }
    }

    public void ComboAdjuster()
    {

        if (comboIncrement == null) comboIncrement = StartCoroutine(ComboIncrement());

    }

    IEnumerator ComboIncrement()
    {


        if (ComboCounter < 30) ComboCounter++;
        yield return new WaitForSeconds(0.2f);
        comboIncrement = null;
    }

    void OnMove(InputValue input)
    {
        if (StopEverythingPlayer) return;
        MoveInput = input.Get<Vector2>();


        //Debug.Log("MoveInput Debug Display " + MoveInput);
        //Debug.Log("IsGrounded: " + IsGroundedPlayer);
    }


    void OnRolling()
    {
        //Debug.Log("Rolling");
        if (StopEverythingPlayer) return;
        if (!IsGroundedPlayer) isRollingAirborne = true;
        if (!lockDashAirborne) CommandHandler.HandleCommand(rollInput);




    }
    public void RollExecute()
    {

        if (!IsRolling && !extraRollingWait)
        {
            if (isRollingAirborne) lockDashAirborne = true;

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
            IsSheating = false;
            IsUnsheating = false;
            IsDuringAttack = false;
            PlayerGrAttackState.sw = false;
            rollingCoroutine = StartCoroutine(RollingCoroutine());



        }


    }


    IEnumerator RollingCoroutine()
    {
        IsRolling = true;
        getDmgCollider.enabled = false;
        yield return new WaitForSecondsRealtime(0.15f * PlayerController.animatorTimeVector);
        getDmgCollider.enabled = true;
        yield return new WaitForSecondsRealtime(0.1f * PlayerController.animatorTimeVector);

        IsRolling = false;

        extraRollingWait = true;
        yield return new WaitForSecondsRealtime(0.175f * PlayerController.animatorTimeVector);
        extraRollingWait = false;
    }

    Coroutine intoConv;
    void OnJumping(InputValue input)
    {

        if (PlayerController.IsInteractable)
        {
            if (input.Get<float>() == 1f)
            {
                if (intoConv == null)
                    intoConv = StartCoroutine(IntoConv());
                //if(StopEverythingPlayer)StopEverythingPlayer= false;
                //else StopEverythingPlayer= true;
                /*
                GameObject.Find("CanvasForWorld").transform.Find("Dialogue").gameObject.SetActive(true);
                int convoTurn = PlayerController.ConversationCounterpart.GetComponent<VerballyInteractable>().GetConvoTurn();
                Conversation convo = PlayerController.ConversationCounterpart.GetComponent<VerballyInteractable>().GetConversation();
                Debug.Log(convoTurn);
                
                if((convo.lines.Length <= convoTurn && !Dialogue.IsWriting) ) 
                {
                    Debug.Log("Convo Fault");

                    if (convo.lines.Length <= convoTurn)
                    {
                        PlayerController.ConversationCounterpart.GetComponent<VerballyInteractable>().SetTurnToCp();
                        GameObject.Find("CanvasForWorld").transform.Find("Dialogue").gameObject.SetActive(false);
                    }
                    return;
                }

                if (Dialogue.IsWriting)
                {
                    Dialogue.instance.FlushOut();



                }
                else if(convo.lines[convoTurn].speakerName == gameObject.name)
                {

                    GameEvents.gameEvents.OnDialogueManagement(gameObject, convo.lines[convoTurn].text);
                    PlayerController.ConversationCounterpart.GetComponent<VerballyInteractable>().IncreaseTurn();
                    if(convoTurn == convo.checkpoint)
                    {
                        PlayerController.ConversationCounterpart.GetComponent<VerballyInteractable>().FinishConvo();
                        GameObject.Find("CanvasForWorld").transform.Find("Dialogue").gameObject.SetActive(false);
                    }

                }

                else
                {
                    PlayerController.ConversationCounterpart.GetComponent<VerballyInteractable>().Speak();
                    if(convoTurn == convo.checkpoint)
                    {
                        PlayerController.ConversationCounterpart.GetComponent<VerballyInteractable>().FinishConvo();
                        GameObject.Find("CanvasForWorld").transform.Find("Dialogue").gameObject.SetActive(false);
                    }

                }*/


                //Debug.Log("Interacting");
            }

            return;
        }
        //Debug.Log("Jumping");
        jumpInput = input.Get<float>();
        //Debug.Log(jumpInput);
        if (jumpInput != 0)
        {
            CommandHandler.HandleCommand(new JumpInput());
        }





        //Debug.Log("Space Value:" + input.Get<float>());


    }
    bool cpReached;


    IEnumerator IntoConv()
    {
        StopEverythingPlayer = true;
        bool fin = false;
        GameObject.Find("CanvasForWorld").transform.Find("Dialogue").gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.01f);
        int convoTurn = PlayerController.ConversationCounterpart.GetComponent<VerballyInteractable>().GetConvoTurn();
        Conversation convo = PlayerController.ConversationCounterpart.GetComponent<VerballyInteractable>().GetConversation();
        Debug.Log(convoTurn);

        if ((convo.lines.Length <= convoTurn && !Dialogue.IsWriting) || (cpReached && !Dialogue.IsWriting))
        {
            Debug.Log("Convo Fault");

            if (convo.lines.Length <= convoTurn)
            {
                PlayerController.ConversationCounterpart.GetComponent<VerballyInteractable>().SetTurnToCp();
                GameObject.Find("CanvasForWorld").transform.Find("Dialogue").gameObject.SetActive(false);

                StopEverythingPlayer = false;
            }
            if (cpReached)
            {
                PlayerController.ConversationCounterpart.GetComponent<VerballyInteractable>().SetTurnToCp();
                GameObject.Find("CanvasForWorld").transform.Find("Dialogue").gameObject.SetActive(false);
                if (convo.lines[convoTurn].speakerName == gameObject.name)
                {
                    Dialogue.instance.Align(gameObject);
                }
                else
                {
                    Dialogue.instance.Align(PlayerController.ConversationCounterpart);

                }
                StopEverythingPlayer = false;
                cpReached = false;
            }




            fin = true;
        }
        if (!fin)
        {
            if (Dialogue.IsWriting)
            {
                Dialogue.instance.FlushOut();



            }
            else if (convo.lines[convoTurn].speakerName == gameObject.name)
            {

                GameEvents.gameEvents.OnDialogueManagement(gameObject, convo.lines[convoTurn].text);
                PlayerController.ConversationCounterpart.GetComponent<VerballyInteractable>().IncreaseTurn();
                convoTurn = PlayerController.ConversationCounterpart.GetComponent<VerballyInteractable>().GetConvoTurn();

                if (convoTurn == convo.checkpoint)
                {
                    cpReached = true;
                }

            }

            else
            {
                PlayerController.ConversationCounterpart.GetComponent<VerballyInteractable>().Speak();
                convoTurn = PlayerController.ConversationCounterpart.GetComponent<VerballyInteractable>().GetConvoTurn();

                if (convoTurn == convo.checkpoint)
                {
                    cpReached = true;
                }

            }
        }

        intoConv = null;
    }



    public static void JumpExecute()
    {

        if ((IsGroundedPlayer || IsRightWallClimbing || IsLeftWallClimbing) && jumpInput != 0) { Debug.Log("Jumping"); IsJumping = true; IsSpacePressing = true; }
    }

    void OnToLightningAura(InputValue input)
    {
        if (StopEverythingPlayer) return;
        //Debug.Log("Aura Input:" + input.Get<float>());
        if (IsLightningAura && input.Get<float>() == 1) { IsLightningAura = false; }
        else if (!IsLightningAura && input.Get<float>() == 1) { IsLightningAura = true; IsWindAura = false; if (!isLightningCoroutineStarted) StartCoroutine(PlayTransition(2)); }
        Debug.Log("Aura Input:" + IsLightningAura);
    }

    IEnumerator PlayTransition(int i)
    {
        isLightningCoroutineStarted = true;
        if (i == 2) AuraTransitionController.AnimatorForAuraTransition.Play("LT for Idle");
        else if (i == 1) AuraTransitionController.AnimatorForAuraTransition.Play("WT for Idle");
        yield return new WaitForSeconds(0.25f);
        AuraTransitionController.AnimatorForAuraTransition.Play("Nothing");
        isLightningCoroutineStarted = false;
    }

    void OnToWindAura(InputValue input)
    {
        if (StopEverythingPlayer) return;
        //Debug.Log("Aura Input:" + input.Get<float>());
        if (IsWindAura && input.Get<float>() == 1) { IsWindAura = false; }
        else if (!IsWindAura && input.Get<float>() == 1) { IsWindAura = true; IsLightningAura = false; if (!isWindCoroutineStarted) StartCoroutine(PlayTransition(1)); }
        Debug.Log("Aura Input:" + IsWindAura);
    }


    void OnLightAttack()
    {
        if (StopEverythingPlayer) return;
        if (IsGroundedPlayer)
        {
            //Debug.Log("HeavyAttack");

            CommandHandler.HandleCommand(lightAttackInput);
            //Debug.Log(heavyAttackInput);

        }
        else
        {
            if (!LockAA)
            {
                AAInit = true;
                CommandHandler.HandleCommand(aaInput);

            }



        }


    }

    public void AAExecute()
    {
        aaCombo = StartCoroutine(AACoroutine());



    }


    IEnumerator AACoroutine()
    {
        Gravity0 = true;

        if (AACounter % 2 == 0)
        {
            IsAirborneAttack = true;
            yield return new WaitForSecondsRealtime(0.08f * PlayerController.animatorTimeVector);
            PlayerController.ChangeAnimationState("AirborneAttack1");
            CommandHandler.ResetNext();
            PlayerController.PlayerRB.WakeUp();
            AACollider.enabled = true;
            yield return new WaitForSecondsRealtime(0.24f * PlayerController.animatorTimeVector);
            AACollider.enabled = false;
            IsAirborneAttack = false;
            AAInit = false;
            AACounter++;


        }
        else
        {
            IsAirborneAttack = true;
            yield return new WaitForSecondsRealtime(0.08f * PlayerController.animatorTimeVector);
            PlayerController.ChangeAnimationState("AirborneAttack2");
            CommandHandler.ResetNext();
            PlayerController.PlayerRB.WakeUp();
            lightAttackCollider.enabled = true;
            yield return new WaitForSecondsRealtime(0.24f * PlayerController.animatorTimeVector);
            lightAttackCollider.enabled = false;
            AACounter = 0;
            IsAirborneAttack = false;
            AAInit = false;
            LockAA = true;
            if (CommandHandler.ShowNext() == aaInput) CommandHandler.ResetNext();
        }

        Gravity0 = false;

    }




    public void LightAttackExecution()
    {

        lightAttackCoroutine = StartCoroutine(LightAttack(LightAttackNumber));

    }
    IEnumerator LightAttack(int count)
    {
        //Debug.Log("In Coroutine");
        PlayerController.PlayerRB.velocity = Vector2.zero;
        if (LightAttackNumber == 1)
        {
            //Debug.Log("AttackNUmber1");

            IsLightAttack = true;
            if (MoveInput.x != 0) { transform.localScale = new Vector2(Mathf.Sign(MoveInput.x), transform.localScale.y); }


            yield return new WaitForSecondsRealtime(0.167f * PlayerController.animatorTimeVector * (1f / AttackSpeed));
            IsDuringAttack = true;
            CommandHandler.ResetNext();
            PlayerController.PlayerRB.WakeUp();
            lightAttackCollider.enabled = true;
            if (Time.timeScale == 1) PlayerController.PlayerRB.AddForce(PlayerController.forward * 17f, ForceMode2D.Impulse);

            else PlayerController.PlayerRB.AddForce(PlayerController.forward * 30f, ForceMode2D.Impulse);
            yield return new WaitForSecondsRealtime(0.25f * PlayerController.animatorTimeVector * (1f / AttackSpeed));
            lightAttackCollider.enabled = false;
            yield return new WaitForSecondsRealtime(0f * PlayerController.animatorTimeVector * (1f / AttackSpeed));

            IsDuringAttack = false;
            LightAttackNumber++;
            PlayerGrAttackState.sw = false;
            IsLightAttack = false;


        }
        else if (LightAttackNumber == 2)
        {

            IsLightAttack = true;
            if (MoveInput.x != 0) { transform.localScale = new Vector2(Mathf.Sign(MoveInput.x), transform.localScale.y); }

            yield return new WaitForSecondsRealtime(0.167f * PlayerController.animatorTimeVector * (1f / AttackSpeed));
            IsDuringAttack = true;
            CommandHandler.ResetNext();
            PlayerController.PlayerRB.WakeUp();
            lightAttackCollider.enabled = true;
            if (Time.timeScale == 1) PlayerController.PlayerRB.AddForce(PlayerController.forward * 17f, ForceMode2D.Impulse);

            else PlayerController.PlayerRB.AddForce(PlayerController.forward * 30f, ForceMode2D.Impulse);

            yield return new WaitForSecondsRealtime(0.25f * PlayerController.animatorTimeVector * (1f / AttackSpeed));
            lightAttackCollider.enabled = false;
            yield return new WaitForSecondsRealtime(0f * PlayerController.animatorTimeVector * (1f / AttackSpeed));
            IsDuringAttack = false;
            LightAttackNumber++;
            PlayerGrAttackState.sw = false;

            IsLightAttack = false;
        }

        else if (LightAttackNumber == 3)
        {

            IsLightAttack = true;
            if (MoveInput.x != 0) { transform.localScale = new Vector2(Mathf.Sign(MoveInput.x), transform.localScale.y); }

            yield return new WaitForSecondsRealtime(0.167f * PlayerController.animatorTimeVector * (1f / AttackSpeed));
            IsDuringAttack = true;
            CommandHandler.ResetNext();
            PlayerController.PlayerRB.WakeUp();
            lightAttackCollider.enabled = true;
            if (Time.timeScale == 1) PlayerController.PlayerRB.AddForce(PlayerController.forward * 25f, ForceMode2D.Impulse);

            else PlayerController.PlayerRB.AddForce(PlayerController.forward * 40f, ForceMode2D.Impulse);
            yield return new WaitForSecondsRealtime(0.25f * PlayerController.animatorTimeVector * (1f / AttackSpeed));
            lightAttackCollider.enabled = false;
            yield return new WaitForSecondsRealtime(0f * PlayerController.animatorTimeVector * (1f / AttackSpeed));
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
            yield return new WaitForSecondsRealtime(0.1f * PlayerController.animatorTimeVector);
            lightAttackCollider.enabled = false;
            yield return new WaitForSecondsRealtime(0f * PlayerController.animatorTimeVector);
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
        if (StopEverythingPlayer) return;
        if (IsGroundedPlayer && Stamina >= 5)
        {
            //Debug.Log("HeavyAttack");
            CommandHandler.HandleCommand(heavyAttackInput);
            //Debug.Log(heavyAttackInput);

        }


    }

    public void HeavyAttackExecution()
    {

        heavyAttackCoroutine = StartCoroutine(HeavyAttack(AttackNumber));

    }
    IEnumerator HeavyAttack(int count)
    {
        PlayerController.PlayerRB.velocity = Vector2.zero;
        //Debug.Log("In Coroutine");,
        if (ComboCounter < 10)
        {
            yield return new WaitForSecondsRealtime(0f * PlayerController.animatorTimeVector);
        }
        else if (ComboCounter < 20)
        {
            IsHeavyAttack = true;
            IsDuringAttack = true;
            AttackNumber = 1;
            CommandHandler.ResetNext();
            PlayerController.PlayerRB.WakeUp();
            HA1Collider.enabled = true;
            //Debug.Log("AttackNUmber1");

            yield return new WaitForSecondsRealtime(0.583f * PlayerController.animatorTimeVector);
            HA1Collider.enabled = false;
            IsDuringAttack = false;
            PlayerGrAttackState.sw = false;

            IsHeavyAttack = false;
            ComboCounter -= 10;
        }
        else if (ComboCounter < 30)
        {

            AttackNumber = 2;
            IsHeavyAttack = true;
            IsDuringAttack = false;
            yield return new WaitForSecondsRealtime(0.3335f * PlayerController.animatorTimeVector);
            IsDuringAttack = true;

            CommandHandler.ResetNext();
            PlayerController.PlayerRB.WakeUp();
            HA2Collider.enabled = true;
            PlayerController.PlayerRB.AddForce(PlayerController.forward * 300f * (1 / Time.timeScale), ForceMode2D.Impulse);

            yield return new WaitForSecondsRealtime(0.3335f * PlayerController.animatorTimeVector);
            HA2Collider.enabled = false;
            IsDuringAttack = false;
            PlayerGrAttackState.sw = false;

            IsHeavyAttack = false;
            ComboCounter -= 20;
        }

        else if (ComboCounter < 40)
        {
            AttackNumber = 3;
            IsHeavyAttack = true;

            IsDuringAttack = true;
            yield return new WaitForSecondsRealtime(0.24f * PlayerController.animatorTimeVector);
            PlayerController.PlayerRB.MovePosition(new Vector2(PlayerController.PlayerRB.transform.position.x + Mathf.Sign(PlayerController.forward.x) * 8, PlayerController.PlayerRB.transform.position.y));
            CommandHandler.ResetNext();
            PlayerController.PlayerRB.WakeUp();
            HA3Collider.enabled = true;

            yield return new WaitForSecondsRealtime(0.34f * PlayerController.animatorTimeVector);
            HA3Collider.enabled = false;
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
            isResettingAttack = true;
        }
    }
    IEnumerator ResettingLightAttack(int resetType)
    {

        if (resetType == 1)
        {
            yield return new WaitForSecondsRealtime(1f * PlayerController.animatorTimeVector);

            if (!IsLightAttack) LightAttackNumber = 1;
        }
        else if (resetType == 0)
        {
            float counter = 0;
            float resetTime = 8f;
            while (counter < resetTime)
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
    public static void IncreaseAttackNumber(int which)
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
        if (StopEverythingPlayer) return;
        if (IsGroundedPlayer && SpecialAttackBar >= 15)
        {
            CommandHandler.HandleCommand(new SpecialAttackInput());
        }
    }

    public void SpecialAttackExecution()
    {
        if (PlayerNeededValues.SpecialAttackBar >= 15)
        {
            StartCoroutine(SpecialAttack(SpecialAttackNumber));
        }
        else
        {
            CommandHandler.ResetNext();
        }
    }

    IEnumerator SpecialAttack(int number)
    {
        PlayerController.PlayerRB.velocity = Vector2.zero;
        SpecialAttackBar -= 15;
        IsSpecialAttack = true;
        IsDuringAttack = true;

        yield return new WaitForSecondsRealtime(0.5f * PlayerController.animatorTimeVector);
        CommandHandler.ResetNext();
        PlayerController.PlayerRB.WakeUp();

        SACollider.enabled = true;

        yield return new WaitForSecondsRealtime(0.75f * PlayerController.animatorTimeVector);
        SACollider.enabled = false;
        IsDuringAttack = false;
        PlayerGrAttackState.sw = false;
        IsSpecialAttack = false;




    }
    protected virtual void TakingDamage(GameObject receiver, GameObject sender, Collider2D otherCollider, int attakVer)
    {
        if (StopEverythingPlayer)
        {

            return;
        }
        if (attakVer == 3 && receiver == gameObject)
        {


            if (Stance > 0 && !lockCounter) Stance -= 1;


        }





        else if (receiver == gameObject)
        {
            isTakenDmg = true;
            if (!lockCounter) isTakenDmgCounter = 0f;
            if (HP > 0) HP--;
            if (Stance > 0 && !lockCounter) Stance--;
            if (ComboCounter <= 5)
            {
                ComboCounter = 0;
            }
            else
            {
                ComboCounter --;
            }
            //Debug.Log("PlayerHp:"+ HP+"  Player Stance:"+ Stance);
            if (attakVer == 1)
            {

                Debug.Log(Mathf.Sign(transform.position.x - otherCollider.transform.position.x));
                Vector2 newPos = new Vector2(transform.position.x + (Mathf.Sign(transform.position.x - otherCollider.transform.position.x) * 1.75f), transform.position.y);
                transform.localScale = new Vector2(-Mathf.Sign(transform.position.x - sender.transform.position.x), transform.localScale.y);
                PlayerController.PlayerRB.MovePosition(newPos);
                Stance = 0;

            }

            if (!IsKnocbacking)
            {

                //Debug.Log("Player Took Dmg");
                if (!IsDuringAttack)
                {


                    if (Stance <= 0)
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
                    if (Stance == 0)
                    {


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
            LightAttackNumber = 1;
        }
        if (IsAirborneAttack)
        {
            if (aaCombo != null) StopCoroutine(aaCombo);
            IsAirborneAttack = false;
            lightAttackCollider.enabled = false;
            AACounter = 0;
            PlayerGrAttackState.sw = false;
            AAInit = false;
            Gravity0 = false;
        }
        if (IsRolling)
        {
            if (rollingCoroutine != null) StopCoroutine(rollingCoroutine);
            IsRolling = false;
            extraRollingWait = false;
        }
        yield return new WaitForSeconds(knockbackDuration);
        IsKnocbacking = false;
        //Stance = 5;


    }


    void OnParry(InputValue input)
    {
        if (StopEverythingPlayer) return;

        if (input.Get<float>() == 1f && IsGroundedPlayer && CanParry > 0.15f)
        {

            //Debug.Log(input.Get<float>());

            CommandHandler.HandleCommand(parryInp);


        }

        else if (input.Get<float>() == 0f)
        {
            //Debug.Log(input.Get<float>());

            IsParrying = false;

        }



    }




    public void ParryExecution()
    {



        StartCoroutine(Parrying());
        //Debug.Log("Parrying");

    }

    float timeForParry;
    public static bool IsPerfectParry { get; private set; }
    public static bool ParryLock { get; private set; }
    IEnumerator Parrying()
    {
        timeForParry = 0f;
        IsParrying = true;
        IsPerfectParry = true;
        //getDmgCollider.enabled = false;
        parryHB.enabled = true;
        PlayerController.PlayerRB.velocity = Vector2.zero;

        int i = 0;

        while (IsParrying && IsGroundedPlayer && !IsRolling)
        {


            timeForParry += 0.015f;


            yield return new WaitForSecondsRealtime(0.015f * PlayerController.animatorTimeVector);
            i++;
            if (timeForParry > 0.2f)
            {
                IsPerfectParry = false;
            }
            if (i > 6)
            {
                ParryLock= true;
            }
            else
            {
                if(Mathf.Abs(MoveInput.x)>0.25)
                transform.localScale =new Vector2(Mathf.Sign(MoveInput.x), transform.localScale.y);
            }

        }

        //getDmgCollider.enabled = true;
        ParryLock =false;
        IsPerfectParry = false;
        parryHB.enabled = false;
        CommandHandler.ResetNext();

    }





}



