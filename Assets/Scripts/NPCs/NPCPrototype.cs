using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPrototype : MonoBehaviour, VerballyInteractable
{
    public IDCard id;
    public NPCLines lines; 
    public Conversation conversation;
    public int convoTurn;
    void Start()
    {
        convoTurn= 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        



    }

    public void Speak()
    {

        GameEvents.gameEvents.OnDialogueManagement(gameObject, conversation.lines[convoTurn].text);
        IncreaseTurn();
       // StartCoroutine(Speaking());


    }

    IEnumerator Speaking()
    {

        int lineNumber = lines.currentCheckpoint;
        
        while (lineNumber < lines.lines.Length)
        {
           
            if (!Dialogue.IsWriting)
            {
                GameEvents.gameEvents.OnDialogueManagement(gameObject, conversation.lines[convoTurn].text);
                convoTurn++;
            }
            else
            {

            }
            yield return new WaitForSecondsRealtime(0.1f);


        }


        lines.currentCheckpoint = lines.checkpoints[0];


    }

    public Conversation GetConversation() => conversation;
    public int GetConvoTurn() => convoTurn;

    public void IncreaseTurn() => convoTurn++;

}
