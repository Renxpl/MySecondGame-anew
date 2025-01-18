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
    
    
    Vector2 cameraTransform;

    int totalLength;
    //Parallax factor for y axis???
    void Start()
    {

        if (GameEvents.gameEvents != null)
        {
            GameEvents.gameEvents.onUpdateCamera += ParallaxUpdate;

        }
        // need to throw an error when these counts are not equal
         cameraTransform = transform.position;
         parallaxMain = new GameObject[parallaxes.Length,3];
         movementInfos = new float[parallaxes.Length,2];
         totalLength = parallaxes.Length;    

       
       

        for(int i = 0; i < totalLength; i++)
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

        MovingParallaxes();



    }
    //Moving Parallaxes
    //In Progress
    void MovingParallaxes()
    {
        
        Vector2 distance = new Vector2(transform.position.x-cameraTransform.x,transform.position.y - cameraTransform.y);
        for (int i = 0; i < totalLength; i++)
        {
            if (movementInfos[i, 1] == 1)
            {
                Vector2 currentPosition = parallaxMain[i, 1].transform.position;
                parallaxMain[i, 1].transform.position = new Vector2(currentPosition.x + distance.x * movementInfos[i, 1], currentPosition.y + distance.y * (movementInfos[i, 1]));
                continue;
            }
            for (int j = 0; j< 3; j++)
            {
                Vector2 currentPosition = parallaxMain[i, j].transform.position;
                parallaxMain[i, j].transform.position = new Vector2(currentPosition.x + distance.x * movementInfos[i, 1], currentPosition.y + distance.y * (movementInfos[i, 1]/10f) );
            }
           


        }

        cameraTransform = transform.position;
    }










    //Adjusting and Instantiating Parallaxes According to Camera Position 
    //Debugged
    void AdjustParallaxes()
    {
        for(int i = 0; i < totalLength; i++)
        {
            // if movement factor is 1 then do not adjust it
            if (movementInfos[i, 1] == 1) continue;

            if (transform.position.x < parallaxMain[i, 0].transform.position.x + movementInfos[i,0])
            {
                //Align to left
                GameObject placeHolder = parallaxMain[i, 2];
                parallaxMain[i, 2] = parallaxMain[i, 1];
                parallaxMain[i, 1] = parallaxMain[i, 0];
                parallaxMain[i, 0] = Instantiate(parallaxMain[i, 1]);
                parallaxMain[i, 0].transform.position = new Vector2(parallaxMain[i, 1].transform.position.x - 2 * movementInfos[i, 0], parallaxMain[i, 1].transform.position.y);
                Destroy(placeHolder);
            }

            if (transform.position.x > parallaxMain[i, 2].transform.position.x - movementInfos[i, 0])
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
