using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTest : MonoBehaviour
{
    // Start is called before the first frame update

    
    public static StateMachine bossSM;
    public static Rigidbody2D bossRb;
    public static BossGroundedState bossGroundedState;
    public static BossIdleState bossIdleState;
    public static BossRunningState bossRunningState;
    public static BossAirborneState bossAirborneState;
    public static BossJumpState bossJumpState;
    public static BossFirstJump bossFirstJumpState;
    static Animator animator;

    public static Collider2D groundCollider;

    public float hp;
    public int potions;
    

    //moving horizontal in airborne,, following player character with a adapting speed
    //general ai implementation, deciding what to do
    //




    void Awake()
    {
        bossGroundedState= new BossGroundedState();
        bossRunningState= new BossRunningState();    
        bossIdleState= new BossIdleState();
        bossJumpState= new BossJumpState();
        bossAirborneState= new BossAirborneState();
        bossFirstJumpState = new BossFirstJump();
        bossSM = new StateMachine();

        bossRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCollider = GetComponent<BoxCollider2D>();

    }
    void Start()
    {
        hp = 9;
        potions = 3;
        //transform.localScale = new Vector2(-1, 1);
        
    
        bossSM?.ChangeState(bossGroundedState);



        

    }
 
    // Update is called once per frame
    void Update()
    {
        bossSM?.Update();




       
    }

    static string currentAnim;
    public static string idleAnim = "Idle";
    public static string idleAnim1 = "IdleAnim";
    public static string runningAnim = "Running";
    public static string ju = "JU";
    public static string jd = "JD";






    public static void ChangeAnimation(string newAnim)
    {
        if (currentAnim == newAnim) return;


        animator.Play(newAnim);

    }


    





}



