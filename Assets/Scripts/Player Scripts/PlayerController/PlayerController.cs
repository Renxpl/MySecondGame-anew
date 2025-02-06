using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static Rigidbody2D PlayerRB { get; private set; }
    public static StateMachine playerSM;
    public static string currentAnimation = "";
    static Animator playerAnimator;
    public static Vector2 forward;

    void Awake()
    {
        PlayerRB = gameObject.GetComponent<Rigidbody2D>();
        playerSM = new StateMachine();
        playerAnimator = gameObject.GetComponent<Animator>();
    }
    void Start()
    {
       
        playerSM?.ChangeState(PlayerNeededValues.GroundedStateForPlayer);
        Debug.Log("Ground state started");
        forward = new Vector2(transform.localScale.x,0f);

    }

    void Update()
    {
        

        /*if (PlayerNeededValues.MoveInput.x > 0.75f || 0.75f < PlayerNeededValues.MoveInput.x)
        {

            playerSM.ChangeState(playerSM.walkState);



        }


         */
        playerSM?.Update();
        //Debug.Log("state machine updated");



        if (PlayerNeededValues.MoveInput.x > 0 && PlayerRB.velocity.x > 0)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (PlayerNeededValues.MoveInput.x < 0 && PlayerRB.velocity.x < 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        forward = new Vector2(transform.localScale.x, 0f);
    }


    public static void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        currentAnimation = newAnimation;

        playerAnimator.Play(currentAnimation);

    }
    


}
