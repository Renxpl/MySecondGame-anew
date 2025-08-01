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


    [Header("Parallax Setup")]
    public GameObject[] parallaxes;
    public float[] movementFactor;
    public int[] parallaxWidths;
   

    [Header("Parallax Settings")]
    public float[] yXMovementRatio;
    public bool[] isLocked;
    public Transform[] point1;
    public Transform[] point2;
        

    Vector2 cameraTransform;
    int totalLength;

    void Start()
    {

        if (GameEvents.gameEvents != null)
        {
            GameEvents.gameEvents.onUpdateCamera += ParallaxUpdate;

        }
       
        if(parallaxes.Length != movementFactor.Length || parallaxes.Length != parallaxWidths.Length)
        {
            Debug.LogError("Parallax Input Lengths Are Not Equal");

        }
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

            
            //Arraying Parallaxes
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
    //Debugged
    void MovingParallaxes()
    {
        
        Vector2 distance = new Vector2(transform.position.x-cameraTransform.x,transform.position.y - cameraTransform.y);
        for (int i = 0; i < totalLength; i++)
        {
            if (isLocked[i])
            {
                if (transform.position.x < point1[i].position.x || transform.position.x > point2[i].position.x)
                {
                    continue;
                }

            }
            if (movementInfos[i, 1] == 1)
            {
                Vector2 currentPosition = parallaxMain[i, 1].transform.position;
                parallaxMain[i, 1].transform.position = new Vector2(transform.position.x,transform.position.y);
                continue;
            }
            for (int j = 0; j< 3; j++)
            {
                Vector2 currentPosition = parallaxMain[i, j].transform.position;

                parallaxMain[i, j].transform.position = new Vector2(currentPosition.x + distance.x * movementInfos[i, 1], currentPosition.y + distance.y * (movementInfos[i, 1]* yXMovementRatio[i]));
          
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
            if (isLocked[i])
            {
                if (transform.position.x < point1[i].position.x || transform.position.x > point2[i].position.x)
                {
                    continue;
                }

            }
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
