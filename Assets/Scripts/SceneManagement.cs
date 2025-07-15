using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] bool transitionHandle;

    public GameObject Panel;
    GameObject player;
    [SerializeField] Transform endOfTheWay;




    bool justOnce;

    private void Awake()
    {
        
    }
    void Start()
    {

       if(transitionHandle) StartCoroutine(Waiting());
        player = GameObject.Find("Player");
        justOnce= false;

    }

    void Update()
    {
       
        if(player.transform.position.x < endOfTheWay.position.x && !justOnce && PlayerNeededValues.StopForTheWay)
        {
            
            SceneManager.LoadScene(1);
            justOnce = true;
            player.GetComponent<PlayerNeededValues>().StopTheWay();
        }


    }

    
    IEnumerator Waiting()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) 
        {
            yield return new WaitForSecondsRealtime(3f);
            Panel.SetActive(true);
            SceneManager.LoadScene(1);
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            yield return new WaitForSecondsRealtime(3f);
            Panel.SetActive(false);

        }

    }


}
