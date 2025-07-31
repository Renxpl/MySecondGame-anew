using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESM2 : MonoBehaviour
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
    public bool startSpawn;
    public float timeGap = 100f;
    float timeCounter = 100f;
    float timeCounterM = 100f;
    float timeCounterC = 100f;
    public Vector2 sSpawnLoc;
    public Vector2 mSpawnLoc;
    public Vector2 cSpawnLoc;
    Vector2 spawnLocR;
    GameObject player;
    public List<GameObject> enemyList;


    public int concurrentEnemyCount;
    int realtimeEnemyCount;
    int rndInt;
    int totalEnemyCount;
    public int randomDownLim;
    public int randomUpLim;

    public Transform[] wayOfEnemySpawnLocs;
    public List<GameObject> enemiesOnTheWay;
    bool isSpawnedForWay;
    public int totalEnemy;


    void Start()
    {
        isSpawnedForWay = false;
        totalEnemyCount = 0;
        realtimeEnemyCount = 0;
        player = GameObject.Find("Player");
        GameEvents.gameEvents.onSpawnNotify += BeingNotified;
        enemyList = new List<GameObject>();
        enemiesOnTheWay = new List<GameObject>();
    }
    bool isCommanderSpawned = false;
    // Update is called once per frame
    void Update()
    {


        if (concurrentEnemyCount > enemyList.Count && totalEnemyCount < totalEnemy && startSpawn)
        {
            rndInt = UnityEngine.Random.Range(randomDownLim, randomUpLim);
            float rndX = UnityEngine.Random.Range(-650f, -500f);
            float locY = 28f;
            if (rndX < player.transform.position.x + 15 && rndX > player.transform.position.x - 15)
            {
                return;
            }
            spawnLocR = new Vector2(rndX, locY);
            if (rndInt == 1)
            {
                enemyList.Add(EFactory.SpawnTest(testStat, new ChaseMovementBehaviour(), spawnLocR, testCombo, new CloseCombatBehaviour()).gameObject);
            }
            else if (rndInt == 2)
            {
                enemyList.Add(EFactory.SpawnMage(mageStats, new ChaseMovementBehaviour(), spawnLocR, mageCombo, new MageCombatBehaviour()).gameObject);
            }
            else
            {
                enemyList.Add(EFactory.SpawnCom(comStats, new ChaseMovementBehaviour(), spawnLocR, comCombo, new CommanderCombatBehav()).gameObject);
            }

            totalEnemyCount++;

        }

        else if (totalEnemyCount == totalEnemy && enemyList.Count == 0 && startSpawn && !isCommanderSpawned)
        {
            GetComponent<SACL2>().Dialogue3();

            isCommanderSpawned = true;
            
        }

        




        /*
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

        */
        if (SceneManager.GetActiveScene().name != "BossFight")
        {

            if (PlayerNeededValues.StopForTheWay && !isSpawnedForWay)
            {
                isSpawnedForWay = true;

                for (int i = 0; i < wayOfEnemySpawnLocs.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        enemiesOnTheWay.Add(EFactory.SpawnTest(testStat, new ChaseMovementBehaviour(), wayOfEnemySpawnLocs[i].position, testCombo, new CloseCombatBehaviour()).gameObject);
                    }

                    else
                    {
                        enemiesOnTheWay.Add(EFactory.SpawnMage(mageStats, new ChaseMovementBehaviour(), wayOfEnemySpawnLocs[i].position, mageCombo, new MageCombatBehaviour()).gameObject);
                    }


                }



            }


            else if (!PlayerNeededValues.StopForTheWay && isSpawnedForWay)
            {


                for (int i = 0; i < enemiesOnTheWay.Count; i++)
                {
                    enemiesOnTheWay[i].SetActive(false);


                }


            }
        }



    }




    public void BeingNotified(GameObject sender)
    {
        enemyList.Remove(sender);
    }
}
