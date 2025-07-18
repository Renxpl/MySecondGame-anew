using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossValuesManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Image healthBar;
    BossTest bT;
    void Start()
    {
        bT = GetComponent<BossTest>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = BossTest.CurrentHealth / bT.hp;
    
        
        
    }
}
