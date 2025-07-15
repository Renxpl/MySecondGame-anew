using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningAtCertainLocs : MonoBehaviour
{
    EnemyStats testStat;
    EnemyStats mageStats;
    AttackCombo testCombo;
    AttackCombo mageCombo;
    ESpawnManager spawnManager;
    public Transform[] sPos;
    public Transform[] decisionPoints;
    List<GameObject> enemies = new List<GameObject>();
    List<GameObject> enemies2 = new List<GameObject>();
    public GameObject dialogueObj1;
    public GameObject dialogueObj2;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        GameEvents.gameEvents.onSpawnNotify += BeingNotified;
        spawnManager = GetComponent<ESpawnManager>();
        testStat = spawnManager.testStat;
        mageStats = spawnManager.mageStats;
        testCombo = spawnManager.testCombo;
        mageCombo = spawnManager.mageCombo;


      // enemies.Add(EFactory.SpawnTest(testStat, new ChaseMovementBehaviour(), sPos[0].position, testCombo, new CloseCombatBehaviour()).gameObject);
       //enemies.Add(EFactory.SpawnMage(mageStats, new ChaseMovementBehaviour(), sPos[1].position, mageCombo, new MageCombatBehaviour()).gameObject);

        enemies2.Add(EFactory.SpawnTest(testStat, new ChaseMovementBehaviour(), sPos[2].position, testCombo, new CloseCombatBehaviour()).gameObject);
        enemies2.Add(EFactory.SpawnMage(mageStats, new ChaseMovementBehaviour(), sPos[3].position, mageCombo, new MageCombatBehaviour()).gameObject);
        enemies2.Add(EFactory.SpawnTest(testStat, new ChaseMovementBehaviour(), sPos[4].position, testCombo, new CloseCombatBehaviour()).gameObject);
        enemies2.Add(EFactory.SpawnMage(mageStats, new ChaseMovementBehaviour(), sPos[5].position, mageCombo, new MageCombatBehaviour()).gameObject);

        dialogueObj1.SetActive(false);
        dialogueObj2.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(enemies.Count == 0)
        {
            PlayerController.PlayerRB.WakeUp();
            PlayerController.ConversationCounterpart = dialogueObj1;
            player.GetComponent<PlayerNeededValues>().ForceDialogue(dialogueObj1);
            dialogueObj1.SetActive(true);



        }
        if (enemies2.Count == 0)
        {
            PlayerController.PlayerRB.WakeUp();
            PlayerController.ConversationCounterpart = dialogueObj2;
            player.GetComponent<PlayerNeededValues>().ForceDialogue(dialogueObj2);
            dialogueObj2.SetActive(true);

        }







    }



    public void BeingNotified(GameObject sender)
    {
        enemies.Remove(sender);
        enemies2.Remove(sender);
    }


}
