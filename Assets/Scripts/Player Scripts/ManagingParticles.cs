using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagingParticles : MonoBehaviour
{
    [SerializeField] GameObject breathingParticleObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(PlayerNeededValues.IsLightAttack || PlayerNeededValues.IsHeavyAttack || PlayerNeededValues.IsDuringAttack || PlayerNeededValues.IsRolling && PlayerNeededValues.IsKnocbacking)
        {
            breathingParticleObject.SetActive(false);


        }
        else
        {
            breathingParticleObject.SetActive(true);
        }


    }
}
