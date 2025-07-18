using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SACL2 : MonoBehaviour
{
    EnemyStats testStat;
    EnemyStats mageStats;
    AttackCombo testCombo;
    AttackCombo mageCombo;
    ESM2 spawnManager;
    public Transform[] sPos;
    public Transform[] decisionPoints;
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject dialogueObj1;
    public GameObject dialogueObj2;
    public GameObject dialogueObj3;
    public GameObject dialogueObj4;
    public float floatXForD3;
    public float floatYForD3;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        GameEvents.gameEvents.onSpawnNotify += BeingNotified;
        spawnManager = GetComponent<ESM2>();
        testStat = spawnManager.testStat;
        mageStats = spawnManager.mageStats;
        testCombo = spawnManager.testCombo;
        mageCombo = spawnManager.mageCombo;


        enemies.Add(EFactory.SpawnTest(testStat, new ChaseMovementBehaviour(), sPos[0].position, testCombo, new CloseCombatBehaviour()).gameObject);
        enemies.Add(EFactory.SpawnTest(testStat, new ChaseMovementBehaviour(), sPos[1].position, testCombo, new CloseCombatBehaviour()).gameObject);


        dialogueObj1.transform.position = sPos[0].position;
        dialogueObj2.transform.position = sPos[1].position;
        dialogueObj1.SetActive(false);
        dialogueObj2.SetActive(false);
        dialogueObj3.SetActive(false);
        dialogueObj4.SetActive(false);
        //

    }
    bool justOnce1 = false;
    bool justOnce2 = false;
    bool justOnce3 = false;
    bool justOnce4 = false;
    bool justOnce5 = false;
    float count;
    // Update is called once per frame
    void Update()
    {
        /*
        if (enemies.Count == 0 && !justOnce1)
        {
            PlayerController.PlayerRB.WakeUp();
            PlayerController.ConversationCounterpart = dialogueObj1;
            player.GetComponent<PlayerNeededValues>().ForceDialogue(dialogueObj1);

            justOnce1 = true;



        }*/
        count += Time.deltaTime;
        if (enemies.Count != 0)
        {
            if (Mathf.Abs(enemies[0].transform.position.x - player.transform.position.x) < 13f && !justOnce1)
            {
                dialogueObj1.transform.position = enemies[0].transform.position;
                dialogueObj2.transform.position = enemies[1].transform.position;
                enemies[0].GetComponent<EnemyController>().PlayCutscene();
                enemies[1].GetComponent<EnemyController>().doNothing = true;
                PlayerController.PlayerRB.WakeUp();
                PlayerController.ConversationCounterpart = dialogueObj1;
                player.GetComponent<PlayerNeededValues>().ForceDialogue(dialogueObj1);
                count = 0;
                justOnce1 = true;

            }
        }

        if (justOnce1 && !dialogueObj1.activeSelf && count >= 1.5f && !justOnce2 && !PlayerController.IsInteractable)
        {

            dialogueObj1.transform.position = enemies[0].transform.position;
            dialogueObj2.transform.position = enemies[1].transform.position;
            PlayerController.PlayerRB.WakeUp();
            PlayerController.ConversationCounterpart = dialogueObj2;
            player.GetComponent<PlayerNeededValues>().ForceDialogue(dialogueObj2);
            count = 0;
            justOnce2 = true;

        }


        if (justOnce2 && !dialogueObj2.activeSelf && count >= 1.5f && !justOnce3 && !PlayerController.IsInteractable)
        {
            enemies[0].GetComponent<EnemyController>().doNothing = false;
            enemies[1].GetComponent<EnemyController>().doNothing = false;
            justOnce3 = true;
        }

        if(enemies.Count == 0 && !justOnce4)
        {

            GetComponent<ESM2>().startSpawn = true;
            justOnce4 = true;

        }


        if(player.transform.position.x < -750f && !justOnce5)
        {
            PlayerController.PlayerRB.WakeUp();
            PlayerController.ConversationCounterpart = dialogueObj4;
            player.GetComponent<PlayerNeededValues>().ForceDialogue(dialogueObj4);
            justOnce5 = true;
            

        }

        if (!dialogueObj4.activeSelf && justOnce5)
        {
            VillageSM.sceneTransition = true;
        }






    }

    public void Dialogue3()
    {
        PlayerController.PlayerRB.WakeUp();
        PlayerController.ConversationCounterpart = dialogueObj3;
        player.GetComponent<PlayerNeededValues>().ForceDialogue(dialogueObj3);
    }

    public void BeingNotified(GameObject sender)
    {
        enemies.Remove(sender);
    }

}
