using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyTypes
{
    Swordsman,
    Mage
}



public class EnemyFactory :MonoBehaviour
{

    [SerializeField] GameObject swordsmanPrefab;
    [SerializeField] GameObject magePrefab;

    public GameObject CreateEnemy(EnemyTypes type)
    {

        switch (type)
        {
            case EnemyTypes.Swordsman:
                return Instantiate(swordsmanPrefab);
               
                break;
            case EnemyTypes.Mage:
                return Instantiate(magePrefab);
                
                break;


            default:
                return null;
                break;
        }


    }
   
}

