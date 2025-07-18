using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapHandle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mm;
    public GameObject hpBoss;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerNeededValues.BossFightStarted)
        {
            mm.SetActive(false);
            hpBoss.SetActive(true);
        }
        else
        {
            mm.SetActive(true);
            hpBoss.SetActive(false);
        }



    }
}
