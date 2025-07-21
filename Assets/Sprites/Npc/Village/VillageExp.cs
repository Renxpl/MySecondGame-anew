using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VillageExp : MonoBehaviour
{

    public static bool WorkItOut {  get;  set; }
    GameObject player;
    GameObject cameraa;

    // Start is called before the first frame update
    Vector2 lastPos;
    void Start()
    {
        GameEvents.gameEvents.onUpdateCamera += CamUp;
        player = GameObject.Find("Player");
        cameraa = GameObject.Find("Main Camera");
       transform.position = new Vector2(player.transform.position.x - 20f, player.transform.position.y + 1 );

        lastPos = new Vector2( player.transform.position.x, player.transform.position.y + 3f);
    }

    // Update is called once per frame

  
    void Update()
    {

        

        if (WorkItOut)
        {
            StartCoroutine(Animate());
            WorkItOut = false;

        }


    }


    IEnumerator Animate()
    {

        GetComponent<Animator>().Play("Exp");
        yield return new WaitForSeconds(0.15f);
        GetComponent<Animator>().Play("LaterMain");


    }



    public void CamUp()
    {
        if(cameraa.transform.position.x < -398f && cameraa.transform.position.y > 30f)
        {
            float distanceX = cameraa.transform.position.x - lastPos.x;
            float distanceY = cameraa.transform.position.y - lastPos.y;
            transform.position = new Vector2(cameraa.transform.position.x - 20 - distanceX * 0.1f, cameraa.transform.position.y - 2f - distanceY * 0.5f);



        }
    }





}
