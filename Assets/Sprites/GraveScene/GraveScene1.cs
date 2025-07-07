using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveScene1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Sprite[] graveChanges;
    GameObject player;
    public static bool CanDig { get; private set; }
    public static float DigCount { get; private set; }
    void Start()
    {
        player = PlayerController.PlayerRB.gameObject;
        isDigged= false;
        activateDiggin = false;
        PlayerNeededValues.DigginScene = true;
        DigCount = 0;
    }
    bool activateDiggin;
    bool isDigged;


    // Update is called once per frame
    void Update()
    {



        if(Mathf.Abs(transform.position.y - player.transform.position.y) < 1.5f && Mathf.Abs(transform.position.x - player.transform.position.x) < 1f) 
        {
            if (!isDigged)
            {
                activateDiggin = true;
                CanDig= true;

            }
            else
            {
                activateDiggin = false;
                CanDig = false;
            }


        }
        else
        {
            activateDiggin = false;
            CanDig = false;
        }

        if (DigCount <= 5)
        {
            for (int i = 0; i <= DigCount; i++)
            {
                GetComponent<SpriteRenderer>().sprite = graveChanges[i];


            }
        }
       
        
        if(DigCount>=5)   isDigged = true;
        


    }


    public static void Digged()
    {

        DigCount+=0.5f;

        





    }


}
