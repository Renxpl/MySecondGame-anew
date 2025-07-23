using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public TutorialNotify notify;
    GameObject child;
    void Start()
    {
        GameEvents.gameEvents.onUpdateCamera += Moving;
        child = this.transform.GetChild(0).gameObject;
        child.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        child.transform.position = transform.position;
        //Debug.Log(new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y));
        for (int i = 0; i < notify.tutorials.Length; i++)
        {
            Vector2 targetPos = notify.tutorials[i].location;
            if (!notify.tutorials[i].isShown && !notify.tutorials[i].justOnce)
            {


                if (notify.tutorials[i].isGreater)
                {

                    if (Camera.main.transform.position.x > targetPos.x && Camera.main.transform.position.y > targetPos.y)
                    {
                        child.transform.position = transform.position;
                        child.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = notify.tutorials[i].text;
                        child.SetActive(true);
                        notify.tutorials[i].justOnce = true;

                    }


                }

                else
                {

                    if (Camera.main.transform.position.x < targetPos.x && Camera.main.transform.position.y < targetPos.y)
                    {
                        child.transform.position = transform.position;
                        child.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = notify.tutorials[i].text;
                        child.SetActive(true);
                        notify.tutorials[i].justOnce = true;
                    }


                }
            }
    
        }


        for (int i = 0; i < notify.tutorials.Length; i++)
        {
            Vector2 targetPos = notify.tutorials[i].finishLocation;
            
            if (!notify.tutorials[i].isShown && child.activeSelf)
            {


                if (notify.tutorials[i].isGreater)
                {

                    if (Camera.main.transform.position.x > targetPos.x && Camera.main.transform.position.y > targetPos.y)
                    {
                        notify.tutorials[i].isShown = true;
                        //child.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = notify.tutorials[i].text;
                        child.SetActive(false);
                        //notify.tutorials[i].isShown = true;


                    }


                }

                else
                {

                    if (Camera.main.transform.position.x < targetPos.x && Camera.main.transform.position.y < targetPos.y)
                    {
                        notify.tutorials[i].isShown = true;
                        //child.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = notify.tutorials[i].text;
                        child.SetActive(false);
                        //notify.tutorials[i].isShown = true;
                    }


                }
            }


        }


    }

    public void Moving()
    {
        transform.position = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y+7);


    }

}
