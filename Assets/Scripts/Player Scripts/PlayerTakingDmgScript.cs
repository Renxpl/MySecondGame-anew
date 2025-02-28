using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakingDmgScript : MonoBehaviour
{
    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private void OnTriggerStay2D(Collider2D collider)
    {
        Debug.Log("OnTriggerStay Started");

        if(collider.gameObject.tag == "EnemyDodgeableAttack")
        {
            if (GameEvents.gameEvents != null)
            {
                Debug.Log("OnEnemyAttack Started");
                // receiver, sender, otherCollider, AttackVersion
                GameEvents.gameEvents.OnGettingDmg(player, gameObject, collider, 0);

            }

        }

        
    }




}
