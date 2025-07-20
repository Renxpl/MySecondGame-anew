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
    PlayerNeededValues values;
    void Start()
    {
        values = GetComponent<PlayerNeededValues>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)PlayerNeededValues.HP / values.hp;
        stanceBar.fillAmount = PlayerNeededValues.Stance / values.stance;
        if (PlayerNeededValues.SpecialAttackBar > 16)
        {
            sABar2.fillAmount = (PlayerNeededValues.SpecialAttackBar-16f) / 16f;
            sABar1.fillAmount = 1f;
        }
        else
        {
            sABar2.fillAmount = 0f;
            sABar1.fillAmount = PlayerNeededValues.SpecialAttackBar / 16f;
        }


    }
}
