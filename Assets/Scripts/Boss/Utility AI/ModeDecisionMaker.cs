using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeDecisionMaker : MonoBehaviour
{

    BossTest mainScript;
    BlackboardForBoss bb;
    StateDecisionMaker sD;
    /* will use here later on
    var modeTransitions = new Dictionary<(BossMode, BossMode), Func<bool>> {
  { (BossMode.Chase, BossMode.Idle), () => distanceToTarget < attackRange },
  { (BossMode.Idle, BossMode.Chase), () => distanceToTarget > attackRange },
  // … diðer geçiþler
};*/
    Dictionary<(BossMode, BossMode), Func<bool>> modeTransitions = new Dictionary<(BossMode, BossMode), Func<bool>> {
  { (BossMode.Chase, BossMode.Idle), () => 5 < 3 },
  { (BossMode.Idle, BossMode.Chase), () => 3 > 5},
  // … diðer geçiþler
};
    void Awake()
    {
        
        
    }
    // Start is called before the first frame update
    void Start()
    {
        mainScript = GetComponent<BossTest>();
        bb = GetComponent<BlackboardForBoss>();
        sD = GetComponent<StateDecisionMaker>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecisionUpdate()
    {

        

        switch (BlackboardForBoss.purpose)
        {
            case BossPurpose.Idle:
                BlackboardForBoss.mode = BossMode.Idle;
                break;
            case BossPurpose.Heal:
                HealMode();
                break;
            case BossPurpose.Attack:
                AttackMode();
                break;

            case BossPurpose.SpecialAttack:
                SpecialAttackMode();
                break;

            default:
                Debug.Log("ModeDecisionError");
                break;

        }


        sD.DecisionUpdate();



    }

    
    void HealMode()
    {





    }

    void AttackMode()
    {
        if((Mathf.Abs(PlayerController.PlayerRB.position.x - BossTest.bossRb.position.x) < 5f && Mathf.Abs(PlayerController.PlayerRB.position.y - BossTest.bossRb.position.y) < 1.5f) || BossTest.ISAStarted)
        {
            BlackboardForBoss.mode = BossMode.Attack;
        }
        else
        {
            BlackboardForBoss.mode = BossMode.Chase;
        }


        //BossTest.bossComboo.steps[BossTest.AttackStep % 6].range

    }

    void SpecialAttackMode()
    {
        Transform currentP;
        if (BossTest.CurrentHealth / mainScript.hp > 1 / 3f)
            currentP = mainScript.sA1P;
        else
            currentP = mainScript.sA2P;

        if (Mathf.Abs(currentP.position.x - BossTest.bossRb.position.x) > 0.05f)
        {
            BlackboardForBoss.mode = BossMode.Flee;
        }
        if (BossTest.groundCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && Mathf.Abs(currentP.position.x - BossTest.bossRb.position.x) < 0.1f)
        {
            BlackboardForBoss.mode = BossMode.SpecialAttack;
        }

        if (BossTest.IsSAStarted)
        {
            BlackboardForBoss.mode = BossMode.SpecialAttack;
        }



    }







}
