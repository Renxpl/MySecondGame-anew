using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScene : MonoBehaviour
{
    public GameObject Panel;
    GameObject player;
    [SerializeField] Transform endOfTheWay;




    bool justOnce;

    private void Awake()
    {

    }
    void Start()
    {

      
        player = GameObject.Find("Player");
        justOnce = false;
        PlayerNeededValues.StopForTheWay = true;
        BossTest.IsInDialogue = true;

    }
    public static bool beingThrown = false;
    void Update()
    {

        if (!justOnce && PlayerNeededValues.StopForTheWay && beingThrown)
        {
            justOnce = true;
            player.GetComponent<PlayerNeededValues>().StopTheWay();
        }


    }

}
