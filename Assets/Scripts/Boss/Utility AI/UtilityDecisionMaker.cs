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
    string[] keys = new string[Enum.GetValues(typeof(BossPurpose)).Length];
    float[] values = new float[Enum.GetValues(typeof(BossPurpose)).Length];

    void Awake()
    {
        mainScript = GetComponent<BossTest>();
        bb = GetComponent<BlackboardForBoss>();
        
    }


    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {

        Array.Sort(values, keys);
        Array.Reverse(values);
        Array.Reverse(keys);












    }


    float ScoreByDistance(Vector2 selfPos, Vector2 playerPos, float maxRange)
    {
        float dist = Vector2.Distance(selfPos, playerPos);
        // 1 – (dist / maxRange) ile 0–1 aralýðýna indirgeriz, sonra clamp
        return Mathf.Clamp01(1f - (dist / maxRange));
    }

    float ScoreFromHp(Vector2 selfPos, Vector2 playerPos, float hp, float maxHp)
    {
        float dist = Vector2.Distance(selfPos, playerPos);
        // 1 – (dist / maxRange) ile 0–1 aralýðýna indirgeriz, sonra clamp
        float value = (0.8f - (dist) * hp) + 0.2f * (dist < 5 ? 0 : 1);
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
