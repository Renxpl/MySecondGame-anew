using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESpawnManager : MonoBehaviour
{
    public EnemyStats testStat;
    public AttackCombo testCombo;
    float timeGap=5f;
    float timeCounter = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        if(timeCounter > timeGap)
        {
            EFactory.SpawnTest(testStat, new ChaseMovementBehaviour(), Vector2.zero, testCombo, new CloseCombatBehaviour());

            timeCounter= 0f;
        }




    }
}
