using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESpawnManager : MonoBehaviour
{
    public EnemyStats testStat;
    public EnemyStats mageStats;
    public EnemyStats comStats;
    public AttackCombo testCombo;
    public AttackCombo mageCombo;
    public AttackCombo comCombo;
    public bool sSpawn;
    public bool mSpawn;
    public bool cSpawn;
    public float timeGap=100f;
    float timeCounter = 100f;
    float timeCounterM = 100f;
    float timeCounterC = 100f;
    public Vector2 sSpawnLoc;
    public Vector2 mSpawnLoc;
    public Vector2 cSpawnLoc;

    public int concurrentEnemyCount;
    int realtimeEnemyCount;
    int rndInt;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (concurrentEnemyCount > realtimeEnemyCount)
        {
            rndInt = UnityEngine.Random.Range(1, 2);
            if (rndInt == 1)
            {

            }
            else if(rndInt == 2)
            {

            }
            else
            {

            }
        }
       





        timeCounter += Time.deltaTime;
        timeCounterM += Time.deltaTime;
        timeCounterC += Time.deltaTime;
        if (timeCounter > timeGap && sSpawn)
        {

            EFactory.SpawnTest(testStat, new ChaseMovementBehaviour(), sSpawnLoc, testCombo, new CloseCombatBehaviour());

            timeCounter= 0f;
        }

        if (timeCounterM > timeGap && mSpawn)
        {
            EFactory.SpawnMage(mageStats, new ChaseMovementBehaviour(), mSpawnLoc, mageCombo, new MageCombatBehaviour());

            timeCounterM = 0f;
        }
        if (timeCounterC > timeGap && cSpawn)
        {
            EFactory.SpawnCom(comStats, new ChaseMovementBehaviour(), cSpawnLoc, comCombo, new CommanderCombatBehav());

            timeCounterC= 0f;
        }


    }
}
