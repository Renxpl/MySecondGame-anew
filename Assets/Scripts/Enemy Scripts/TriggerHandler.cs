using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    GameObject parent;
    bool HasTriggered { get; set; } 
    
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        GameEvents.gameEvents.onDisablingAC += EndAttack;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("LightAttack"))
        {

            if (GameEvents.gameEvents != null)
            {
                // receiver, sender, otherCollider, AttackVersion
                GameEvents.gameEvents.OnGettingDmg(transform.parent.gameObject, gameObject, collider, 0);

            }
            
        }

    }
    void OnTriggerStay2D(Collider2D collider)
    {
        //Debug.Log("TriggerStay");
       
        
        
        



    }

   

    void EndAttack(GameObject sender)
    {
        if (sender.tag == "Enemy" && parent.gameObject.tag=="Player")
        {
            HasTriggered = false;
        }

        if(sender.tag == "Player" && parent.gameObject.tag == "Enemy")
        {
            HasTriggered= false;
        }


    }



}
