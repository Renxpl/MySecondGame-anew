using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentEdgeDetector : MonoBehaviour
{
    public EdgeDetector leftUpEdgeDetector;
    public EdgeDetector leftDownEdgeDetector;
    public EdgeDetector rightUpEdgeDetector;
    public EdgeDetector rightDownEdgeDetector;

    bool leftUpWorking;
    bool leftDownWorking;
    bool rightUpWorking;
    bool rightDownWorking;


    public bool isEdgeClimbWork;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isEdgeClimbWork)
        {
            return;
        }




    }

    public void ChildEdgeTriggered(int id, string type)
    {
        if(id == 0)
        {
            if(type == "in")
            {

            }
            if (type == "out")
            {

            }




        }
        if (id == 1)
        {
            if (type == "in")
            {

            }
            if (type == "out")
            {

            }
        }
        if (id == 2)
        {
            if (type == "in")
            {

            }
            if (type == "out")
            {

            }
        }
        if (id == 3)
        {
            if (type == "in")
            {

            }
            if (type == "out")
            {

            }
        }


    }



}
