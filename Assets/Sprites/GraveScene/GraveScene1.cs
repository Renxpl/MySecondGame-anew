using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveScene1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Sprite[] graveChanges;
    GameObject player;
    void Start()
    {
        player = PlayerController.PlayerRB.gameObject;
        isDigged= false;
        activateDiggin = false;
        PlayerNeededValues.DigginScene = true;

    }
    bool activateDiggin;
    bool isDigged;

    // Update is called once per frame
    void Update()
    {



        if(Mathf.Abs(transform.position.y - player.transform.position.y) < 0.5f && Mathf.Abs(transform.position.x - player.transform.position.x) < 1.5f) 
        {
            if (!isDigged)
            {
                activateDiggin = true;


            }


        }




    }
}
