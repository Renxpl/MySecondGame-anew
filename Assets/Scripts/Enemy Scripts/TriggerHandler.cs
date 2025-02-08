using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
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
                GameEvents.gameEvents.OnGettingDmg(transform.parent.gameObject,gameObject,collider,0);

            }

        }
        if (collider.gameObject.CompareTag("EnemyDodgeableAttack"))
        {

            if (GameEvents.gameEvents != null)
            {
                // receiver, sender, otherCollider, AttackVersion
                GameEvents.gameEvents.OnGettingDmg(transform.parent.gameObject, gameObject, collider, 0);

            }

        }


    }



}
