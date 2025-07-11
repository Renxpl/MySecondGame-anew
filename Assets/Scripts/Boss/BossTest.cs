using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTest : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject player;
    float speed = 9f;
    void Start()
    {
        GetComponent<Animator>().Play("Running");
        //transform.localScale = new Vector2(-1, 1);
        player = GameObject.Find("Player");

    }
 
    // Update is called once per frame
    void Update()
    {
        float distance = player.transform.position.x - transform.position.x;
        if (Mathf.Abs(player.transform.position.x - transform.position.x) > 1)
        {
            GetComponent<Animator>().Play("Running");
            if (distance > 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
                transform.localScale = new Vector2(1, 1);

            }
            else if(distance < 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0f);
                transform.localScale = new Vector2(-1, 1);

            }


           


        }


        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Animator>().Play("Idle");
        }

       
    }
}
