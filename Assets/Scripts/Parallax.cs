using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{

    GameObject[,] parallaxMain;
    float[,] movementInfos;
    [Header("General Settings")]
    public int pixelCountToDisplayInAnUnit;


    [Header("Parallax Settings")]
    public GameObject[] parallaxes;
    public float[] movementFactor;
    public int[] parallaxWidths;

   


    //Parallax factor for y axis???
    void Start()
    {

        if (GameEvents.gameEvents != null)
        {
            GameEvents.gameEvents.onUpdateCamera += ParallaxUpdate;

        }

        
         parallaxMain = new GameObject[parallaxes.Length,3];
         movementInfos = new float[parallaxes.Length,2];

       
       

        for(int i = 0; i < parallaxes.Length; i++)
        {
            //movement info[,0] is information about how many units to left or right position.
            movementInfos[i,0] = (float)parallaxWidths[i] / ((float)pixelCountToDisplayInAnUnit*2.0f);
            movementInfos[i, 1] = movementFactor[i];

            parallaxMain[i,1] = parallaxes[i];
            parallaxMain[i,0] = Instantiate(parallaxes[i]);
            parallaxMain[i, 2] = Instantiate(parallaxes[i]);
            for (int j = 0; j< 3; j += 2)
            {
                parallaxMain[i, j].transform.position = new Vector2(parallaxMain[i, 1].transform.position.x - 2f*(movementInfos[i, 0]-(j* movementInfos[i, 0])), parallaxMain[i, 1].transform.position.y);
            }
            
            

        }




    }

    
   //Update method for parallax script for making it work after camera behaviour
    void ParallaxUpdate()
    {





    }
}
