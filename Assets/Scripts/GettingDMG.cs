using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingDMG : MonoBehaviour
{
    // Start is called before the first frame update
    //here will implement event or interface system here
    [SerializeField] PlayerMovement script;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("LightAttack") && script.IsLightAttacking)
        {
            Destroy(transform.parent.gameObject);

        }

    }
    

}
