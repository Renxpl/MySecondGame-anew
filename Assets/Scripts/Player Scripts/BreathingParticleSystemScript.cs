using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingParticleSystemScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    ParticleSystem ps;
    ParticleSystem.EmissionModule emission;
    void Start()
    {
        player = transform.parent.gameObject;
        ps = GetComponent<ParticleSystem>();
        emission = ps.emission;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.localScale = player.transform.localScale;

        if(PlayerNeededValues.IsLightAttack || PlayerNeededValues.IsHeavyAttack || PlayerNeededValues.IsDuringAttack ||PlayerNeededValues.IsRolling || PlayerNeededValues.IsKnocbacking ||PlayerNeededValues.IsAirborneAttack ||PlayerNeededValues.IsRightWallClimbing || PlayerNeededValues.IsLeftWallClimbing)
        {

            if (!ps.isStopped)
            {
                ps.Stop();
            }

        }
        else
        {

            if (ps.isStopped)
            {
                ps.Play();
            }

        }

       

    }
}
