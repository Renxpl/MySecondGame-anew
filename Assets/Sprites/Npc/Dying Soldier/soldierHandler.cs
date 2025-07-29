using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soldierHandler : MonoBehaviour
{
    // Start is called before the first frame update
    Coroutine coroutine;
    void Start()
    {
        coroutine = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<NPCPrototype>().convoTurn > 0 && coroutine == null)
        {

            coroutine = StartCoroutine(coRo());

        } 
        


    }

    IEnumerator coRo()
    {
        GetComponent<Animator>().Play("up");
        while (GetComponent<NPCPrototype>().convoTurn != GetComponent<NPCPrototype>().conversation.checkpoint)
        {
            yield return new WaitForSeconds(0.1f);
        }
        
       
        GetComponent<Animator>().Play("down");
    }



    

}
