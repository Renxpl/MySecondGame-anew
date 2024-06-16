using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] GameObject toFollow;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        this.transform.position = new Vector3(toFollow.transform.position.x, toFollow.transform.position.y, this.transform.position.z);
    }
}
