using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//will handle position and count of enemies
public class EnemySpawner : MonoBehaviour
{
    public GameObject swordsman;
    float timeCounter = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        timeCounter+= Time.deltaTime;

        if(timeCounter > 5f)
        {
            Instantiate(swordsman);
            swordsman.transform.position = Vector2.zero;

            timeCounter = 0;
        }


    }
}
