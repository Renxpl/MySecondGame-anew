using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GeneralUIWork : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI comboText;
    
    


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        comboText.text = PlayerNeededValues.ComboCounter.ToString();        
    }
}
