using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal.ShaderGraph;
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

    public static bool IsSpeacialAttack { get; private set; }


    public static LayerMask groundLayer;
    public static GameObject player;


    public AnimationCurve jumpCurve;



    public static Vector2 MoveInput { get; private set; }


    static float jumpInput;
    bool isLightningCoroutineStarted = false;
    bool isWindCoroutineStarted = false;
    bool extraRollingWait = false;

    public static HeavyAttackInput heavyAttackInput;
    public static int AttackNumber { get; private set; }    
    //next input will be handled by bools

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
        heavyAttackInput = new HeavyAttackInput();
    }

    // Start is called before the first frame update
    void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
        player = PlayerController.PlayerRB.gameObject;
        IsLightningAura= false;
       
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


        CommandHandler.HandleCommand(new RollInput());




    }
    public void RollExecute()
    {
        
        if (!IsRolling && !extraRollingWait)
        {

            StartCoroutine(RollingCoroutine());

        }


    }
    

    IEnumerator RollingCoroutine()
    {
        IsRolling = true;
        yield return new WaitForSeconds(0.33f); 
        IsRolling= false;
        extraRollingWait = true;
        yield return new WaitForSeconds(0.165f);
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
        


    }

    void OnHeavyAttack()
    {
        
        if (IsGroundedPlayer)
        {
            //Debug.Log("HeavyAttack");
            CommandHandler.HandleCommand(heavyAttackInput);
        }

    }

    public void HeavyAttackExecution()
    {
        HeavyAttack(AttackNumber);
    }
    IEnumerator HeavyAttack(int count)
    {
        
        if (AttackNumber == 1)
        {
            IsHeavyAttack= true;
            yield return new WaitForSeconds(0.25f);
            IsHeavyAttack= false;
        }
        else if (AttackNumber == 2)
        {
            IsHeavyAttack = true;
            yield return new WaitForSeconds(0.25f);
            IsHeavyAttack = false;
        }

        else if (AttackNumber == 3)
        {
            IsHeavyAttack = true;
            yield return new WaitForSeconds(0.5f);
            IsHeavyAttack = false;
        }
        
    }


    public static void ResetAttackNumber()
    {
        AttackNumber= 0;
    }

    public  static void IncreaseAttackNumber()
    {
        AttackNumber++;
    }

}



