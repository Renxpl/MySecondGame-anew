using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject toBack;
    [SerializeField] GameObject[] toGo;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        FindAnyObjectByType<UIManagement>().OnResumeButton();


    }




    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

    public void Go(string name)
    {

       

        foreach (GameObject go in toGo)
        {
            if(go.name == name)
            {
                go.SetActive(true);
                gameObject.SetActive(false);
            }

        }



    } 




    public void Back()
    {
        toBack.SetActive(true);
        gameObject.SetActive(false);



    }


}
