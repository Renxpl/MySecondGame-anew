using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{

    public GameObject test;
    public Vector2 offset;


    void Start()
    {
        this.transform.position = new Vector2(test.transform.position.x+ offset.x,test.transform.position.y+ offset.y);
    }

   
    void Update()
    {
        this.transform.position = new Vector2(test.transform.position.x + offset.x, test.transform.position.y + offset.y);
    }
}
