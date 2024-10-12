using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static Rigidbody2D PlayerRB { get; private set; }
    public static StateMachine playerSM;
    bool isGrounded;
    void Start()
    {
        Rigidbody2D playerRb = gameObject.GetComponent<Rigidbody2D>();
        playerSM = new StateMachine();


    }

    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, PlayerNeededValues.groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * 1f, isGrounded ? Color.green : Color.red);

        /*if (PlayerNeededValues.MoveInput.x > 0.75f || 0.75f < PlayerNeededValues.MoveInput.x)
        {

            playerSM.ChangeState(playerSM.walkState);



        }


         */
        playerSM.Update();
        

    }

    




}
