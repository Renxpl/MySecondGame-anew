using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakingDmgScript : MonoBehaviour
{
    GameObject player;
    bool takeDmg;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.gameObject;
        takeDmg = false;
    }

    // Update is called once per frame
    void Update()
    {

      
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("Enter");
        if (collider.gameObject.tag == "EnemyDodgeableAttack")
        {
            if (GameEvents.gameEvents != null)
            {
                // receiver, sender, otherCollider, AttackVersion
                GameEvents.gameEvents.OnGettingDmg(player, gameObject, collider, 0);

            }

        }
        takeDmg = true;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {

        //ebug.Log("Taking dmg");
        

        
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
     
    }
   



}
