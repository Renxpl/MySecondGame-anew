using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GeneralUIWork : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI comboText;
    Canvas canvas;

    GameObject player;
    GameObject camera;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        player = GameObject.Find("Player");
        camera = GameObject.Find("Main Camera");
        GameEvents.gameEvents.onUpdateCamera += MoveCanvasWorld;
        //canvas.pixelPerfect= true;
        
    }

    // Update is called once per frame
    void Update()
    {
        comboText.text = PlayerNeededValues.ComboCounter.ToString();
        //transform.position = new Vector2( player.transform.position.x,player.transform.position.y + 3f);
    }

    public void MoveCanvasWorld()
    {
        transform.position = new Vector2(camera.transform.position.x, camera.transform.position.y );
    }


}
