using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public static bool sceneTransition;
    void Update()
    {
        if (sceneTransition) 
        {
            StartCoroutine(Waiting());
            sceneTransition = false;
        }
        

    }


    IEnumerator Waiting()
    {
       
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Panel.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
            Panel.SetActive(true);
            SceneManager.LoadScene(1);
            player.GetComponent<PlayerNeededValues>().StopTheWay();
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Panel.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
            Panel.SetActive(true);
            SceneManager.LoadScene(2);
            player.GetComponent<PlayerNeededValues>().StopTheWay();

        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Panel.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
            
            Panel.SetActive(true);
            SceneManager.LoadScene(3);
            player.GetComponent<PlayerNeededValues>().StopTheWay();

        }


    }

}
