using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class VillageSM : MonoBehaviour
{
    public GameObject Panel;
    GameObject player;
    [SerializeField] Transform endOfTheWay;




    bool justOnce;

    private void Awake()
    {

    }
    void Start()
    {


        player = GameObject.Find("Player");
        player.GetComponent<PlayerNeededValues>().StopTheWay();


    }
    
    void Update()
    {

       

    }

}
