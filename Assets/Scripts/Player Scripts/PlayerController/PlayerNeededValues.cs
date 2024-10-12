using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNeededValues : MonoBehaviour
{


    public static LayerMask groundLayer;





    public static Vector2 MoveInput { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMove(InputValue input)
    {
        MoveInput = input.Get<Vector2>();
        //Debug.Log("MoveInput Debug Display " + moveInput);
    }



}
