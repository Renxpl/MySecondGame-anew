using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface VerballyInteractable
{

    Conversation GetConversation();
    int GetConvoTurn();
    void IncreaseTurn();

    void SetTurnToCp();
    void Speak();

    void FinishConvo();


}
