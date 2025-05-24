using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EFactory
{
   //will take paramaters Init + position 
    public static EnemyController SpawnTest(EnemyStats stats, IMovementBehaviour mov, Vector2 pos)
    {
        var go = Object.Instantiate(stats.enemyPrefab,pos, Quaternion.identity);
        var ctrl = go.GetComponent<EnemyController>();
        ctrl.Init(stats, mov);
        return ctrl;

        
    }



   




}
