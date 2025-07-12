using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class UtilityDecisionMaker : MonoBehaviour
{
    // Start is called before the first frame update
    BossTest mainScript;
    BlackboardForBoss bb;
    
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

       










    }


    float ScoreByDistance(Vector2 selfPos, Vector2 playerPos, float maxRange)
    {
        float dist = Vector2.Distance(selfPos, playerPos);
        // 1 – (dist / maxRange) ile 0–1 aralýðýna indirgeriz, sonra clamp
        return Mathf.Clamp01(1f - (dist / maxRange));
    }

    float ScoreFromHp(Vector2 selfPos, Vector2 playerPos, float hp)
    {
        float dist = Vector2.Distance(selfPos, playerPos);
        // 1 – (dist / maxRange) ile 0–1 aralýðýna indirgeriz, sonra clamp
        float value = (0.8f - (dist) * hp) + 0.2f * (dist < 5 ? 0 : 1);
        return Mathf.Clamp01(value);
    }

    float ScoreForAttack(Vector2 selfPos, Vector2 playerPos, float maxRange, float hp)
    {

        float dist = Vector2.Distance(selfPos, playerPos);
      

        float value = 1f - (dist / maxRange) ;
        return Mathf.Clamp01(value);

    }

    float ScoreForSpecialAttack(Vector2 selfPos, Vector2 playerPos, float hp)
    {

        float dist = Vector2.Distance(selfPos, playerPos);


        float value = 0f;
        return Mathf.Clamp01(value);


    }




}
