using System;
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
                //if((PlayerNeededValues.IsParrying && PlayerController.PlayerRB.transform.localScale.x * collider.transform.parent.localScale.x == 1) || !PlayerNeededValues.IsParrying)
                

                float difX = collider.transform.parent.transform.parent.GetComponent<EnemyController>().GetPrePosition().x - player.transform.position.x;
                if(player.transform.localScale.x * difX < 0)
                {
                    GameEvents.gameEvents.OnGettingDmg(player, gameObject, collider, 0);
                }
                //GameEvents.gameEvents.OnGettingDmg(player, gameObject, collider, 0);
                else
                {
                    if (PlayerNeededValues.IsPerfectParry)
                    {

                        collider.transform.parent.transform.parent.GetComponent<EnemyController>().BeingParried();

                    } 

                    else if (PlayerNeededValues.IsParrying)
                    {

                        GameEvents.gameEvents.OnGettingDmg(player, gameObject, collider, 3);

                    }

                    else
                    {
                        GameEvents.gameEvents.OnGettingDmg(player, gameObject, collider, 0);
                    }


                }

            }

        }
        if (collider.gameObject.tag == "Fireball")
        {
            if (GameEvents.gameEvents != null)
            {
                // receiver, sender, otherCollider, AttackVersion
                GameEvents.gameEvents.OnGettingDmg(player, gameObject, collider, 0);

            }

        }

       
        if (collider.gameObject.tag == "MageThrowBackAttack")
        {
            if (GameEvents.gameEvents != null)
            {
                // receiver, sender, otherCollider, AttackVersion
                GameEvents.gameEvents.OnGettingDmg(player, gameObject, collider, 1);

            }

        }
        if (collider.gameObject.tag == "CommanderExp")
        {
            if (GameEvents.gameEvents != null)
            {
                // receiver, sender, otherCollider, AttackVersion
                GameEvents.gameEvents.OnGettingDmg(player, gameObject, collider, 2);

            }

        }
        if (collider.gameObject.tag == "A4P")
        {
            if (GameEvents.gameEvents != null)
            {
                // receiver, sender, otherCollider, AttackVersion
                GameEvents.gameEvents.OnGettingDmg(player, gameObject, collider, 0);

            }

        }

        if (collider.gameObject.tag == "FG")
        {
            if (GameEvents.gameEvents != null)
            {
                // receiver, sender, otherCollider, AttackVersion
                GameEvents.gameEvents.OnGettingDmg(player, gameObject, collider, 4);

            }

        }
        if (collider.gameObject.tag == "MC")
        {
            if (GameEvents.gameEvents != null)
            {
                // receiver, sender, otherCollider, AttackVersion
                GameEvents.gameEvents.OnGettingDmg(player, gameObject, collider, 5);

            }

        }


        if (collider.gameObject.tag == "BossGrA")
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
