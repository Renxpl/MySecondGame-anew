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
    public GameObject dialogueObj3;
    public GameObject dialogueObj4;
    public GameObject dialogueObj5;
    public GameObject dialogueObj0;
    public float floatXForD3;
    public float floatYForD3;
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


       enemies.Add(EFactory.SpawnTest(testStat, new ChaseMovementBehaviour(), sPos[0].position, testCombo, new CloseCombatBehaviour()).gameObject);
       enemies.Add(EFactory.SpawnMage(mageStats, new ChaseMovementBehaviour(), sPos[1].position, mageCombo, new MageCombatBehaviour()).gameObject);

        enemies2.Add(EFactory.SpawnTest(testStat, new ChaseMovementBehaviour(), sPos[2].position, testCombo, new CloseCombatBehaviour()).gameObject);
        enemies2.Add(EFactory.SpawnMage(mageStats, new ChaseMovementBehaviour(), sPos[3].position, mageCombo, new MageCombatBehaviour()).gameObject);
        enemies2.Add(EFactory.SpawnTest(testStat, new ChaseMovementBehaviour(), sPos[4].position, testCombo, new CloseCombatBehaviour()).gameObject);
        enemies2.Add(EFactory.SpawnMage(mageStats, new ChaseMovementBehaviour(), sPos[5].position, mageCombo, new MageCombatBehaviour()).gameObject);

        dialogueObj1.SetActive(false);
        dialogueObj2.SetActive(false);
        dialogueObj3.SetActive(false);
        dialogueObj4.SetActive(false);
        dialogueObj5.SetActive(false);
        //
        dialogueObj5.SetActive(true);
        dialogueObj0.SetActive(false);
    }
    bool justOnce1 = false;
    bool justOnce2 = false;
    bool justOnce3 = false;
    bool justOnce4 = false;

    // Update is called once per frame
    void Update()
    {
        if(enemies.Count == 0 && !justOnce1)
        {
            PlayerController.PlayerRB.WakeUp();
            PlayerController.ConversationCounterpart = dialogueObj1;
            player.GetComponent<PlayerNeededValues>().ForceDialogue(dialogueObj1);
           
            justOnce1 = true;



        }
        if (enemies2.Count == 0 && !justOnce2)
        {
            PlayerController.PlayerRB.WakeUp();
            PlayerController.ConversationCounterpart = dialogueObj2;
            player.GetComponent<PlayerNeededValues>().ForceDialogue(dialogueObj2);
            justOnce2= true;

        }



        if(player.transform.position.x > floatXForD3 && player.transform.position.y < floatYForD3 && !justOnce3)
        {
            PlayerController.PlayerRB.WakeUp();
            PlayerController.ConversationCounterpart = dialogueObj3;
            player.GetComponent<PlayerNeededValues>().ForceDialogue(dialogueObj3);
            
            justOnce3 = true;
        }

        if(justOnce3 && !dialogueObj3.activeSelf)
        {
            GetComponent<ESpawnManager>().startSpawn = true;
        }

        if(dialogueObj5.GetComponent<VerballyInteractable>().GetConvoTurn() == dialogueObj5.GetComponent<VerballyInteractable>().GetConversation().checkpoint && !PlayerNeededValues.StopEverythingPlayer && !justOnce4)
        {
            dialogueObj5.GetComponent<BoxCollider2D>().enabled= false;
            PlayerNeededValues.StopForTheWay = true;
            justOnce4= true;
        }

    }

    public void Dialogue4()
    {
        dialogueObj5.SetActive(true);
        dialogueObj0.SetActive(false);
        PlayerController.PlayerRB.WakeUp();
        PlayerController.ConversationCounterpart = dialogueObj4;
        player.GetComponent<PlayerNeededValues>().ForceDialogue(dialogueObj4);

    }

    public void BeingNotified(GameObject sender)
    {
        enemies.Remove(sender);
        enemies2.Remove(sender);
    }


}
