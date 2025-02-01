using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyTypes
{
    Swordsman,
    Mage
}

public class EnemySpawner
{



}



public abstract class EnemyFactory : MonoBehaviour
{
    protected abstract void CreateProduct(Vector3 position);
   
}

