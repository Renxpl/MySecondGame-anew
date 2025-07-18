using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        //player.GetComponent<PlayerNeededValues>().StopTheWay();
        PlayerNeededValues.beSad = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
