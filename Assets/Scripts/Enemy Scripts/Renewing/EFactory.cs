using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EFactory
{
   //will take paramaters Init + position 
    public static EnemyController SpawnTest(EnemyStats stats, IMovementBehaviour mov, Vector2 pos, AttackCombo combo, IAttackBehaviour attack, float HP)
    {
        var go = Object.Instantiate(stats.enemyPrefab,pos, Quaternion.identity);
        var ctrl = go.GetComponent<EnemyController>();
        for(int i = 0; i < 3; i++)
        {
            combo.steps[i].hitbox = go.transform.Find("AttackHitboxes").transform.Find("Attack" + (i+1)).GetComponent<PolygonCollider2D>();

        }
       
        ctrl.Init(stats, mov,combo,attack, HP);
        return ctrl;

        
    }



   




}
