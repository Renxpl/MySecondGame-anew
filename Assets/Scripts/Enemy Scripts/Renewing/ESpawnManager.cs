using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESpawnManager : MonoBehaviour
{
    public EnemyStats testStat;
    public AttackCombo testCombo;
    float timeGap=15f;
    float timeCounter = 100f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        if(timeCounter > timeGap)
        {
            EFactory.SpawnTest(testStat, new ChaseMovementBehaviour(), Vector2.zero, testCombo, new CloseCombatBehaviour(), 5f);//the last one is HP

            timeCounter= 0f;
        }




    }
}
