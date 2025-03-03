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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("LightAttack"))
        {

            if (GameEvents.gameEvents != null)
            {
                // receiver, sender, otherCollider, AttackVersion
                GameEvents.gameEvents.OnGettingDmg(transform.parent.gameObject, gameObject, collider, 0);

            }
            
        }
        else if (collider.gameObject.CompareTag("AirborneAttack"))
        {
            if (GameEvents.gameEvents != null)
            {
                // receiver, sender, otherCollider, AttackVersion
                GameEvents.gameEvents.OnGettingDmg(transform.parent.gameObject, gameObject, collider, 1);

            }
        }
        else if (collider.gameObject.CompareTag("HeavyAttack1"))
        {
            if (GameEvents.gameEvents != null)
            {
                // receiver, sender, otherCollider, AttackVersion
                GameEvents.gameEvents.OnGettingDmg(transform.parent.gameObject, gameObject, collider, 10);

            }
        }
        else if (collider.gameObject.CompareTag("HeavyAttack2"))
        {
            if (GameEvents.gameEvents != null)
            {
                // receiver, sender, otherCollider, AttackVersion
                GameEvents.gameEvents.OnGettingDmg(transform.parent.gameObject, gameObject, collider, 11);

            }
        }
        else if (collider.gameObject.CompareTag("HeavyAttack3"))
        {
            if (GameEvents.gameEvents != null)
            {
                // receiver, sender, otherCollider, AttackVersion
                GameEvents.gameEvents.OnGettingDmg(transform.parent.gameObject, gameObject, collider, 12);

            }
        }
        else if (collider.gameObject.CompareTag("SpecialAttack1"))
        {
            if (GameEvents.gameEvents != null)
            {
                // receiver, sender, otherCollider, AttackVersion
                GameEvents.gameEvents.OnGettingDmg(transform.parent.gameObject, gameObject, collider, 20);

            }
        }
        else
        {
            Debug.Log("Unidentified Trigger Interaction Detected");
        }


    }
    void OnTriggerStay2D(Collider2D collider)
    {
        //Debug.Log("TriggerStay");
       
        
        
        



    }
    void OnTriggerExit2D(Collider2D collision)
    {
        
    }


    void EndAttack(GameObject sender)
    {
       

    }



}
