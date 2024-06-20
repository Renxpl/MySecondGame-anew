using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleTest : MonoBehaviour
{
    TextMeshProUGUI textMesh;

    [SerializeField] List<string> textToWrite;
    [SerializeField] int textSelection;
    int counter = 0;
    float timer = 0;
    float typingSpeed = 0.1f;
    public bool toWrite = false;
    void Start()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
    
    }

    // Update is called once per frame
    void Update()
    {
        if (toWrite)
        {
            if (counter < textToWrite[textSelection].Length)
            {
                timer += Time.deltaTime;
                if (timer >= typingSpeed)
                {

                    textMesh.text += textToWrite[textSelection][counter];
                    counter++;
                    timer = 0;

                }
            }
            else { counter = 0; toWrite = false; }

        }


    }
    void FixedUpdate()
    {
        

    }


}
