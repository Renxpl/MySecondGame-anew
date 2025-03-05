using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerValuesManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Image healthBar;
    public Image stanceBar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = PlayerNeededValues.HP / 10f;
        stanceBar.fillAmount = PlayerNeededValues.Stance / 5f;


    }
}
