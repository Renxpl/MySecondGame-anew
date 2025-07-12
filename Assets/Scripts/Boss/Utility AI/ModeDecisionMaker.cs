using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeDecisionMaker : MonoBehaviour
{

    BossTest mainScript;
    BlackboardForBoss bb;

    void Awake()
    {
        mainScript = GetComponent<BossTest>();
        bb = GetComponent<BlackboardForBoss>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
