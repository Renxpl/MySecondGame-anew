using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTest : MonoBehaviour
{
    // Start is called before the first frame update

    public BoxCollider2D speechColl;
    public static StateMachine bossSM;
    public static Rigidbody2D bossRb;
    public static BossGroundedState bossGroundedState;
    public static BossIdleState bossIdleState;
    public static BossRunningState bossRunningState;
    public static BossAirborneState bossAirborneState;
    public static BossJumpState bossJumpState;
    public static BossFirstJump bossFirstJumpState;
    public static ScriptedState scriptedState;
    static Animator animator;
    public Conversation convo2;

    public static Collider2D groundCollider;

    public float hp;
    public int potions;
    public AttackCombo bossCombo;
    public static bool IsInDialogue { get; set; }
    public static bool ForceDialogue { get; set; }
    public static bool ChangeDialogue { get; set; }
    public static bool IsSAStarted { get; set; }
    public static bool ISAStarted { get; set; }

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
        scriptedState = new ScriptedState();
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
    bool justOnceEnd = false;
    // Update is called once per frame
    void Update()
    {
        bossSM?.Update();

        ChangeSprite();


        if (ForceDialogue)
        {

            GameObject.Find("Player").GetComponent<PlayerNeededValues>().ForceDialogue(gameObject);
            ForceDialogue= false;


        }

        if (ChangeDialogue)
        {

            GetComponent<NPCPrototype>().conversation = convo2;
            ChangeDialogue= false;
        }

        if(hp <= 0 && !justOnceEnd)
        {
            GetComponent<NPCPrototype>().convoTurn = 0;
            GameObject.Find("Player").GetComponent<PlayerNeededValues>().ForceDialogue(gameObject);
            justOnceEnd= true;

        }

       
    }

    public static bool isSpriteLocked = false;
    void ChangeSprite()
    {
        if (isSpriteLocked) return;

        if(bossRb.velocity.x > 0)
        {
            transform.localScale = new Vector2(1f,1f);
        }

        else if (bossRb.velocity.x < 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }


    }




    static string currentAnim;
    public static string idleAnim = "Idle";
    public static string idleAnim1 = "IdleAnim";
    public static string runningAnim = "Running";
    public static string ju = "JU";
    public static string jd = "JD";
    public static string ss = "SS";
    public static string sa1 = "SA1";
    public static string sa2 = "SA2";
    public static string ga1 = "GA1";
    public static string ga2 = "GA2";
    public static string ga3 = "GA3";
    public static string ga4 = "GA4";
    public static string aa1 = "AA1";
    public static string aa2 = "AA2";






    public static void ChangeAnimation(string newAnim)
    {
        if (currentAnim == newAnim) return;


        animator.Play(newAnim);

    }


    





}



