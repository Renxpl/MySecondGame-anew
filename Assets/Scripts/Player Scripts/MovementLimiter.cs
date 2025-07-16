using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLimiter : MonoBehaviour
{
    public Transform p1;
    public Transform p2;
    public bool dontRoll;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LimiterUpdate()
    {
        dontRoll = false;
        if (transform.position.x >= p2.position.x)
        {
            if (gameObject.name == "Player")//for player
            {
                if (PlayerNeededValues.MoveInput.x > 0) GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                dontRoll = true;
            }

            else//for camera
            {
               
            }

        }

        if(transform.position.x <= p1.position.x)
        {
            if (gameObject.name == "Player")
            {
                if (PlayerNeededValues.MoveInput.x < 0) GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                dontRoll = true;
            }
            else
            {
               
            }
        }


        if(gameObject.name != "Player")
        {

            if (transform.position.x >= p2.position.x - 14f)
            {
                transform.position = new Vector3(p2.position.x - 14f, transform.position.y, transform.position.z);
            }


            if (transform.position.x <= p1.position.x + 14f)
            {
                transform.position = new Vector3(p1.position.x + 14f, transform.position.y, transform.position.z);
            }

        }



    }


}
