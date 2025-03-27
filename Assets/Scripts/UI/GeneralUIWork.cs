using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GeneralUIWork : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI comboText;
    Canvas canvas;
    


    void Start()
    {
        canvas = GetComponent<Canvas>();
        //canvas.pixelPerfect= true;
        
    }

    // Update is called once per frame
    void Update()
    {
        comboText.text = PlayerNeededValues.ComboCounter.ToString();        
    }
}
