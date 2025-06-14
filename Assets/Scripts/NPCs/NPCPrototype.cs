using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPrototype : MonoBehaviour, VerballyInteractable
{
    public IDCard id;
    public NPCLines lines; 
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        



    }

    public void Speak()
    {
        
        
        StartCoroutine(Speaking());


    }

    IEnumerator Speaking()
    {

        int lineNumber = lines.currentCheckpoint;
        
        while (lineNumber < lines.lines.Length)
        {
            string currentLine = lines.lines[lineNumber];
            if (!Dialogue.IsWriting)
            {
                GameEvents.gameEvents.OnDialogueManagement(gameObject, currentLine);
                lineNumber++;
            }
            yield return new WaitForSecondsRealtime(0.1f);


        }





    }



}
