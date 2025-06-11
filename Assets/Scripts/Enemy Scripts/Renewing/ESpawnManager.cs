using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESpawnManager : MonoBehaviour
{
    public EnemyStats testStat;
    public EnemyStats mageStats;
    public AttackCombo testCombo;
    public AttackCombo mageCombo;
    float timeGap=100f;
    float timeCounter = 100f;
    float timeCounterM = 100f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        timeCounterM += Time.deltaTime;
        if (timeCounter > timeGap && 1<0)
        {
            EFactory.SpawnTest(testStat, new ChaseMovementBehaviour(), Vector2.zero, testCombo, new CloseCombatBehaviour());

            timeCounter= 0f;
        }

        if (timeCounterM > timeGap)
        {
            EFactory.SpawnMage(mageStats, new ChaseMovementBehaviour(), Vector2.zero, mageCombo, new MageCombatBehaviour());

            timeCounterM = 0f;
        }



    }
}
