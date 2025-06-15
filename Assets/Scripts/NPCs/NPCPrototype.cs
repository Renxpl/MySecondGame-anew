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
        convoTurn = 0;

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

        

        while (convoTurn < conversation.lines.Length)
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


        


    }

    public Conversation GetConversation() => conversation;
    public int GetConvoTurn() => convoTurn;

    public void IncreaseTurn() => convoTurn++;
    public void SetTurnToCp() => convoTurn = conversation.checkpoint;


    public void FinishConvo()
    {
        convoTurn = conversation.checkpoint;
    }

}
