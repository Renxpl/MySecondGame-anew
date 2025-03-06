using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerValuesManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Image healthBar;
    public Image stanceBar;
    public Image sABar1;
    public Image sABar2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = PlayerNeededValues.HP / 10f;
        stanceBar.fillAmount = PlayerNeededValues.Stance / 5f;
        if (PlayerNeededValues.SpecialAttackBar > 15)
        {
            sABar2.fillAmount = (PlayerNeededValues.SpecialAttackBar-15f) / 15f;
            sABar1.fillAmount = 1f;
        }
        else
        {
            sABar2.fillAmount = 0f;
            sABar1.fillAmount = PlayerNeededValues.SpecialAttackBar / 15f;
        }


    }
}
