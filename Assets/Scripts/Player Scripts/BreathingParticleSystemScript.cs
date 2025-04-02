using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingParticleSystemScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    ParticleSystem particleSystem;
    void Start()
    {
        player = transform.parent.gameObject;
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = player.transform.localScale;
        


    }
}
