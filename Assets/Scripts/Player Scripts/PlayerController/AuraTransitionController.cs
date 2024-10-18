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

        if (PlayerNeededValues.IsLightningAura && !isCoroutineStarted)
        {
            StartCoroutine(PlayTransition());


        }


    }

    IEnumerator PlayTransition()
    {
        isCoroutineStarted= true;
        AnimatorForAuraTransition.Play("LT for Idle");
        yield return new WaitForSeconds(0.25f);
        AnimatorForAuraTransition.Play("Nothing");
        isCoroutineStarted= false;

    }

}
