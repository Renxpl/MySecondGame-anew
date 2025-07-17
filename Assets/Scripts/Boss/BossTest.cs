using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTest : MonoBehaviour, IDamageable
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
    public static GrAttackState grAttackState;
    public static BossAirborneAttack aAState;
    public static BossShadowStep sSState;
    static Animator animator;
    public Conversation convo2;

    public static Collider2D groundCollider;

    public float hp;
    public int potions;
    public AttackCombo bossCombo;
    public static AttackCombo bossComboo;
    public static int AttackStep;
    public static bool IsInDialogue { get; set; }
    public static bool ForceDialogue { get; set; }
    public static bool ChangeDialogue { get; set; }
    public static bool IsSAStarted { get; set; }
    public static bool ISAStarted { get; set; }

    public static Collider2D[] attackHitboxes = new Collider2D[8];

    //moving horizontal in airborne,, following player character with a adapting speed
    //general ai implementation, deciding what to do
    //
    public static float CurrentStance;
    public static float CurrentHealth;
    float timeToBePassedBetweenHits;
    public static bool alreadyStepped;
    public static bool AttackOnceAirborne {  get; set; }
    public void TakeDamage(float dmg, float staDmg)
    {






        /*

        if (timeToBePassedBetweenHits >= 0.05f)
        {
            if (CurrentStance > 0)
            {
                CurrentHealth -= dmg;
                if (PlayerNeededValues.StopForTheWay)
                {
                    CurrentHealth = 0;
                }
            }
            else
            {
                CurrentHealth -= dmg * 2;
            }
            if (CurrentStance > 0) CurrentStance -= staDmg;

            //Debug.Log("enemyhp " + CurrentHealth);
            if (CurrentHealth <= 0) isDead = true;
            timeToBePassedBetweenHits = 0f;
            timeToBePassedForStanceRegen = 0f;
            StartCoroutine(DmgedSpriteChange());



        }

        */
    }


    void Awake()
    {
        bossGroundedState= new BossGroundedState();
        bossRunningState= new BossRunningState();    
        bossIdleState= new BossIdleState();
        bossJumpState= new BossJumpState();
        bossAirborneState= new BossAirborneState();
        bossFirstJumpState = new BossFirstJump();
        scriptedState = new ScriptedState();
        grAttackState = new GrAttackState();
        aAState = new BossAirborneAttack();
        sSState = new BossShadowStep();
        bossSM = new StateMachine();
        


        bossRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCollider = GetComponent<BoxCollider2D>();
        AttackStep = 0;
        bossComboo = bossCombo;
        

    }
    void Start()
    {
        hp = 9;
        potions = 3;
        //transform.localScale = new Vector2(-1, 1);
        
    
        bossSM?.ChangeState(bossGroundedState);
        GameObject aH = transform.Find("AttackHitboxes").gameObject;
        for(int i = 0; i < 4; i++)
        {
            int a = i + 1;
            attackHitboxes[i] = aH.transform.Find(a.ToString()).GetComponent<Collider2D>();
            attackHitboxes[i].enabled = false;

        }

        

    }
    bool justOnceEnd = false;
    // Update is called once per frame
    void Update()
    {
        bossSM?.Update();
        timeToBePassedBetweenHits += Time.deltaTime;
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




    public Coroutine Run(IEnumerator routine) => StartCoroutine(routine);
    public void StopCo(Coroutine routine) => StopCoroutine(routine);



}



