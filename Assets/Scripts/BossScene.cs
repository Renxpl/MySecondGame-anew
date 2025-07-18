using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScene : MonoBehaviour
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
        justOnce = false;
        PlayerNeededValues.StopForTheWay = true;
        BossTest.IsInDialogue = true;
        PlayerController.ChangeAnimationState("Idle");
        PlayerController.ChangeAnimationState("Speedy Run");
        PlayerNeededValues.cannotAttack = true;
      

    }
    public static bool beingThrown = false;
    bool justOnce1 = false;
    void Update()
    {

        if (!justOnce && PlayerNeededValues.StopForTheWay && beingThrown)
        {
            justOnce = true;
            player.GetComponent<PlayerNeededValues>().StopTheWay();
           
        }

        if (player.transform.position.x < -550f && !justOnce1)
        {
            StartCoroutine(Waiting());
            justOnce1 = true;

           

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
