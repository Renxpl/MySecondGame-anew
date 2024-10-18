using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraTransitionController : MonoBehaviour
{
    
    public static Animator AnimatorForAuraTransition { get;private set; }
    bool isCoroutineStarted;

    void Awake()
    {
        AnimatorForAuraTransition = gameObject.GetComponent<Animator>();
        isCoroutineStarted = false;
    }

    void Update()
    {



    }

  

}
