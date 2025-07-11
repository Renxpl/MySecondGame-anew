using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTest : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject player;
    float speed = 9f;
    public static StateMachine bossSM;
    public static Rigidbody2D bossRb;
    public static BossGroundedState bossGroundedState;
    public static BossIdleState bossIdleState;
    public static BossRunningState bossRunningState;
    




    void Awake()
    {
        bossGroundedState= new BossGroundedState();
        bossRunningState= new BossRunningState();
        bossIdleState= new BossIdleState();
        bossSM = new StateMachine();
        bossRb = GetComponent<Rigidbody2D>();
        

    }
    void Start()
    {
       
        //transform.localScale = new Vector2(-1, 1);
        
        player = GameObject.Find("Player");
        bossSM?.ChangeState(bossGroundedState);





    }
 
    // Update is called once per frame
    void Update()
    {
        bossSM?.Update();

       
    }

    static string currentAnim;
    public static string idleAnim = "Idle";
    public static string runningAnim = "Running";






    public static void ChangeAnimation(string newAnim)
    {
        if (currentAnim == newAnim) return;


    }


    





}
