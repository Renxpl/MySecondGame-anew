using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] int targetFps = 30;
    [SerializeField] GameObject toFollow;
    public float smoothTime = 0.3f;
    Vector3 velocity = Vector3.zero;
    Vector3 targetPosition= Vector3.zero;
    Camera cam;
    PixelPerfectCamera pixelPerfectCamera;
    int a = 0;
    SpawningAtCertainLocs spawn1;
    // Start is called before the first frame update

    void Awake()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
    }
    void Start()
    {
        if (GameObject.Find("EFacTest").GetComponent<SpawningAtCertainLocs>() != null)
            spawn1 = GameObject.Find("EFacTest").GetComponent<SpawningAtCertainLocs>();
        Application.targetFrameRate = targetFps;
        cam = GetComponent<Camera>();
        float aspectRatio= (float)Screen.width / Screen.height;
        float tolerance = 0.02f;
        //Debug.Log(aspectRatio);
        pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
        if (Mathf.Abs(aspectRatio-16f / 9f) <= tolerance)
        {
            //GameObject.Find("CanvasFor16:9").SetActive(true);
            //GameObject.Find("CanvasFor16:10").SetActive(false);
        }
        else if (Mathf.Abs(aspectRatio - 16f / 10f) <= tolerance)
        {
            pixelPerfectCamera.refResolutionY = 300;
            //GameObject.Find("CanvasFor16:9").SetActive(false);
            //GameObject.Find("CanvasFor16:10").SetActive(true);
        }
        else
        {
            //GameObject.Find("CanvasFor16:9").SetActive(true);
            //GameObject.Find("CanvasFor16:10").SetActive(false);
        }



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



        if (a == 0) 
        {
            targetPosition = new Vector3(toFollow.transform.position.x, toFollow.transform.position.y + 3, this.transform.position.z); a++;
        }
        
        else
            //targetPosition = new Vector3(toFollow.transform.position.x, transform.position.y, this.transform.position.z);

            targetPosition = new Vector3(toFollow.transform.position.x, toFollow.transform.position.y + 3, this.transform.position.z);
        transform.position = targetPosition;
        
        if(GetComponent<MovementLimiter>() != null)
        {
            if (PlayerNeededValues.BossFightStarted)
            {
                GetComponent<MovementLimiter>().LimiterUpdate();
            }
            


        }
        if (GetComponent<MovementLimiter2>() != null)
        {
            if (GameObject.Find("EFacTest").GetComponent<ESM2>().enemyList.Count != 0 || GameObject.Find("EFacTest").GetComponent<SACL2>().enemies.Count != 0)
            {
                GetComponent<MovementLimiter2>().LimiterUpdate();
            }



        }
        if (GetComponent<MovementLimiter3>() != null && (!MovementLimiter3.deactivateLimiter || MovementLimiter3.spawning)) GetComponent<MovementLimiter3>().LimiterUpdate();
        GameEvents.gameEvents.OnUpdateCamera();



    }

    
    



}
