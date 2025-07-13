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


    public void DecisionUpdate()
    {



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
        if (Mathf.Abs(PlayerController.PlayerRB.position.x - BossTest.bossRb.position.x) > 3f)
        {
            BlackboardForBoss.state = BossState.Running;
        }
        else if (Mathf.Abs(PlayerController.PlayerRB.position.y - BossTest.bossRb.position.y) > 1.5f)
        {
            BlackboardForBoss.state = BossState.Jump;
        }
        






    }

    void AttackState()
    {
        BlackboardForBoss.state = BossState.Idle;
    }

    void SpecialAttackState()
    {

    }





}
