using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] int targetFps = 30;
    [SerializeField] GameObject toFollow;
    public float smoothTime = 0.3f;
    Vector3 velocity = Vector3.zero;
    Vector3 targetPosition= Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = targetFps;

    }

    // Update is called once per frame
    void Update()
    {

       
    }
    void FixedUpdate()
    {
        //this.transform.position = new Vector3(toFollow.transform.position.x + 2, toFollow.transform.position.y + 3, this.transform.position.z);
        //targetPosition = new Vector3(toFollow.transform.position.x, toFollow.transform.position.y, transform.position.z);
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        //Vector3 newPosition = Vector3.Lerp(transform.position,targetPosition, 1f * Time.fixedDeltaTime);
        //transform.position = newPosition;
    }

    //Moving First Camera and then Parallax Background
    void LateUpdate()
    {
        
        targetPosition = new Vector3(toFollow.transform.position.x, toFollow.transform.position.y + 3, this.transform.position.z);

        transform.position = targetPosition;

        GameEvents.gameEvents.OnUpdateCamera();


    }

    
    



}
