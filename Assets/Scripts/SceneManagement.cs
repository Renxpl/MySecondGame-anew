using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] bool transitionHandle;

    public GameObject Panel;
    

    void Start()
    {

       if(transitionHandle) StartCoroutine(Waiting());



    }

    void Update()
    {
        
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
