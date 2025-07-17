using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDecisionMaker : MonoBehaviour
{
    // Start is called before the first frame update
    BossTest mainScript;
    BlackboardForBoss bb;

    void Awake()
    {
       
    }
    void Start()
    {
        mainScript = GetComponent<BossTest>();
        bb = GetComponent<BlackboardForBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float timeForSS = 0;
    public void DecisionUpdate()
    {
        
        timeForSS += Time.deltaTime;


        switch (BlackboardForBoss.mode)
        {
            case BossMode.Idle:
                BlackboardForBoss.state = BossState.Idle;
                break;
            case BossMode.Heal:
                HealState();
                break;
            case BossMode.Flee:
                FleeState();
                break;

            case BossMode.Chase:
                ChaseState();
                break;
            case BossMode.Attack:
                AttackState();
                break;
            case BossMode.SpecialAttack:
                SpecialAttackState();
                break;

            default:
                Debug.Log("StateDecisionError");
                break;

        }






    }

    void HealState()
    {

    }

    void FleeState()
    {

    }

    void ChaseState()
    {
        if (Mathf.Abs(PlayerController.PlayerRB.position.x - BossTest.bossRb.position.x) > 5f)
        {
            BlackboardForBoss.state = BossState.Running;
        }
        else if (Mathf.Abs(PlayerController.PlayerRB.position.y - BossTest.bossRb.position.y) > 1.5f)
        {
            BlackboardForBoss.state = BossState.Jump;
        }
        






    }
    int rnd = 0;
    void AttackState()
    {

        
       BlackboardForBoss.state = BossState.Attack;

        if (timeForSS > 1f)
        {
            rnd = UnityEngine.Random.Range(1, 4);
            timeForSS = 0;
        }


       // float distanceX =PlayerController.PlayerRB.transform.position.x - BossTest.bossRb.transform.position.x;
       // float distanceY = PlayerController.PlayerRB.transform.position.y - BossTest.bossRb.transform.position.y;



        if (!BossTest.ISAStarted && PlayerNeededValues.IsLightAttack && rnd == 1 && BossTest.groundCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            BlackboardForBoss.state = BossState.ShadowStep;
        }
        
        

    }

    void SpecialAttackState()
    {

    }





}
