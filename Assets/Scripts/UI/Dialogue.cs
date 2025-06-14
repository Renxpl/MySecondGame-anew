using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public static Dialogue instance;
    public GameObject test;
    public Vector2 offset;
    public static bool IsWriting { get; private set; }
    TextMeshProUGUI text;
    string currentLine;
    Coroutine writer;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;


    }
    void Start()
    {
        this.transform.position = new Vector2(test.transform.position.x+ offset.x,test.transform.position.y+ offset.y);
        GameEvents.gameEvents.onDialogueManagement += DialogueManagement;
        text = transform.Find("test").GetComponent<TextMeshProUGUI>();
    }

   
    void Update()
    {
        
        
        
        // this.transform.position = new Vector2(test.transform.position.x + offset.x, test.transform.position.y + offset.y);



        

    }

    void DialogueManagement(GameObject sender, string line)
    {
        text.text = "";
        currentLine= line;
        this.transform.position = new Vector2(sender.transform.position.x + offset.x, sender.transform.position.y + offset.y);
        
        if(writer == null)writer = StartCoroutine(Writer());


    }



    IEnumerator Writer()
    {
        
        IsWriting= true;
        for (int i = 0; i < currentLine.Length; i++)
        {

            text.text += currentLine[i];
            yield return new WaitForSecondsRealtime(0.1f);

        }
        
        IsWriting= false;
        writer = null;
        
    }


    public void FlushOut()
    {
        StopCoroutine(writer);
        text.text = "";
        text.text = currentLine;


        
        IsWriting = false;
        writer = null;
    }



}
