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

    GameObject newlyCreatedOne;
    void CreateEnemy(EnemyTypes type,Vector3 position)
    {
        switch (type)
        {
            case EnemyTypes.Swordsman:
                newlyCreatedOne = Instantiate(swordsmanPrefab);
                newlyCreatedOne.transform.position = position;
                break;
            case EnemyTypes.Mage:
                newlyCreatedOne = Instantiate(magePrefab);
                newlyCreatedOne.transform.position = position;
                break;


            default:
                
                break;
        }


    }
   
}

