using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExecDmg : MonoBehaviour
{

    [SerializeField] float dmg;
    [SerializeField] float timeToBePassed;
    float timePassed;

    void Start()
    {
        timePassed = 0f;    
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (timePassed>timeToBePassed)
        {
            other.transform.parent.GetComponent<IDamageable>().TakeDamage(dmg);
            timePassed = 0f;
        }

       




    }



    void Update()
    {
        timePassed += Time.deltaTime;
    }



}
