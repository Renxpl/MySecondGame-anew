using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;
using System.Linq;

public class UtilityDecisionMaker : MonoBehaviour
{
    // Start is called before the first frame update
    BossTest mainScript;
    BlackboardForBoss bb;
    ModeDecisionMaker mD;
    string[] keys = new string[Enum.GetValues(typeof(BossPurpose)).Length];
    float[] values = new float[Enum.GetValues(typeof(BossPurpose)).Length];

    void Awake()
    {
        
        
        
    }


    void Start()
    {
        mainScript = GetComponent<BossTest>();
        bb = GetComponent<BlackboardForBoss>();
        mD = GetComponent<ModeDecisionMaker>();

    }

    // Update is called once per frame
    void Update()
    {
        keys[0] = "Idle";
        values[0] = ScoreForDialogue(false);
        keys[1] = "Heal";
        values[1] = ScoreFromHp(transform.position, PlayerController.PlayerRB.position, 9f, 9f);
        keys[2] = "Attack";
        values[2] = ScoreForAttack(transform.position, PlayerController.PlayerRB.position,5f ,9f, 9f);
        keys[3] = "SpecialAttack";
        values[3] = ScoreForSpecialAttack(transform.position, PlayerController.PlayerRB.position, 9f, 9f);


        Array.Sort(values, keys);
        Array.Reverse(values);
        Array.Reverse(keys);


        if (keys[0] == "Idle")
        {
            BlackboardForBoss.purpose = BossPurpose.Idle;
        }
        else if(keys[0] == "Heal")
        {
            BlackboardForBoss.purpose = BossPurpose.Heal;
        }
        else if(keys[0] == "Attack")
        {
            BlackboardForBoss.purpose = BossPurpose.Attack;
        }
        else
        {
            BlackboardForBoss.purpose = BossPurpose.SpecialAttack;
        }



        mD.DecisionUpdate();





    }


    float ScoreByDistance(Vector2 selfPos, Vector2 playerPos, float maxRange)
    {
        float dist = Vector2.Distance(selfPos, playerPos);
        // 1 – (dist / maxRange) ile 0–1 aralýðýna indirgeriz, sonra clamp
        return Mathf.Clamp01(1f - (dist / maxRange));
    }

    float ScoreForDialogue(bool isInDialogue)
    {
       
        // 1 – (dist / maxRange) ile 0–1 aralýðýna indirgeriz, sonra clamp
        return Mathf.Clamp01( isInDialogue ? 1 : 0);
    }

    float ScoreFromHp(Vector2 selfPos, Vector2 playerPos, float hp, float maxHp)
    {
        float dist = Vector2.Distance(selfPos, playerPos);
        // 1 – (dist / maxRange) ile 0–1 aralýðýna indirgeriz, sonra clamp
        float value = (0.8f - (dist) * hp / maxHp) + 0.2f * (dist < 5 ? 0 : 1);
        return Mathf.Clamp01(value);
    }

    float ScoreForAttack(Vector2 selfPos, Vector2 playerPos, float maxRange, float hp, float maxHp)
    {

        float dist = Vector2.Distance(selfPos, playerPos);
      

        float value = (maxRange / dist)* 0.8f + hp * 0.2f / maxHp;
        return Mathf.Clamp01(value);

    }

    float ScoreForSpecialAttack(Vector2 selfPos, Vector2 playerPos, float hp, float maxHp)
    {

        float dist = Vector2.Distance(selfPos, playerPos);


        float value = 1f - hp/maxHp;
        return Mathf.Clamp01(value);


    }




}
