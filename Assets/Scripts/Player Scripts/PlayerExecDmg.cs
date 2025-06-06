using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExecDmg : MonoBehaviour
{

    [SerializeField] float dmg;
    
    static float timeToPassedBetweenComboIncrement;
    bool increaseComboCount;
    void Start()
    {
        
        increaseComboCount = false;
        timeToPassedBetweenComboIncrement= 0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        other.transform.parent.GetComponent<IDamageable>().TakeDamage(dmg);
        if (timeToPassedBetweenComboIncrement > 0.5f && PlayerNeededValues.IsLightAttack) increaseComboCount = true;






    }



    void Update()
    {
        
        if (increaseComboCount)
        {
            GameEvents.gameEvents.OnPlayerComboIncrement();
            increaseComboCount= false;
            timeToPassedBetweenComboIncrement= 0f;

        }
        timeToPassedBetweenComboIncrement += Time.deltaTime;

    }



}
