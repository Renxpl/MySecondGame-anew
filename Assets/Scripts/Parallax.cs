using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    GameObject[][] parallaxMain;
    public GameObject[] instancedParallax;
    GameObject[] instances;





    void Start()
    {
        if (GameEvents.gameEvents != null)
        {
            GameEvents.gameEvents.onUpdateParallax += ParallaxUpdate;

        }


    }

    
   //Update method for parallax script for making it work after camera behaviour
    void ParallaxUpdate()
    {









    }
}
