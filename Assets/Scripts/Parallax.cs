using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
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

    Transform cameraTransform;

    int totalLength;
    //Parallax factor for y axis???
    void Start()
    {

        if (GameEvents.gameEvents != null)
        {
            GameEvents.gameEvents.onUpdateCamera += ParallaxUpdate;

        }

         cameraTransform = transform;
         parallaxMain = new GameObject[parallaxes.Length,3];
         movementInfos = new float[parallaxes.Length,2];
         totalLength = parallaxes.Length;    

       
       

        for(int i = 0; i < parallaxes.Length; i++)
        {
            
            //movement info[,0] is information about how many units to left or right position.
            movementInfos[i,0] = (float)parallaxWidths[i] / ((float)pixelCountToDisplayInAnUnit*2.0f);
            movementInfos[i, 1] = movementFactor[i];

            parallaxMain[i,1] = parallaxes[i];
            
            // if movement factor is 1 then do not instantiate it
            if (movementInfos[i, 1] == 1) continue; 

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
        AdjustParallaxes();



    }












    //Adjusting and Instantiating Parallaxes According to Camera Position 
    void AdjustParallaxes()
    {
        for(int i = 0; i < totalLength; i++)
        {
            // if movement factor is 1 then do not adjust it
            if (movementInfos[i, 1] == 1) continue;

            if (cameraTransform.position.x < parallaxMain[i, 0].transform.position.x + movementInfos[i,0])
            {
                //Align to left
                GameObject placeHolder = parallaxMain[i, 2];
                parallaxMain[i, 2] = parallaxMain[i, 1];
                parallaxMain[i, 1] = parallaxMain[i, 0];
                parallaxMain[i, 0] = Instantiate(parallaxMain[i, 1]);
                parallaxMain[i, 0].transform.position = new Vector2(parallaxMain[i, 1].transform.position.x - 2 * movementInfos[i, 0], parallaxMain[i, 1].transform.position.y);
                Destroy(placeHolder);
            }

            if (cameraTransform.position.x > parallaxMain[i, 2].transform.position.x - movementInfos[i, 0])
            {
                //Align to right
                GameObject placeHolder = parallaxMain[i, 0];
                parallaxMain[i, 0] = parallaxMain[i, 1];
                parallaxMain[i, 1] = parallaxMain[i, 2];
                parallaxMain[i, 2] = Instantiate(parallaxMain[i, 1]);
                parallaxMain[i, 2].transform.position = new Vector2(parallaxMain[i, 1].transform.position.x + 2 * movementInfos[i, 0], parallaxMain[i, 1].transform.position.y);
                Destroy(placeHolder);

            }

        }
       
    }



}
