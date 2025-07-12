using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GeneralUIWork : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI comboText;
    Canvas canvas;

    GameObject player;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        player = GameObject.Find("Player");
        //canvas.pixelPerfect= true;
        
    }

    // Update is called once per frame
    void Update()
    {
        comboText.text = PlayerNeededValues.ComboCounter.ToString();
        transform.position = new Vector2( player.transform.position.x,player.transform.position.y + 3f);
    }
}
