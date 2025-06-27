using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EFactory
{
   //will take paramaters Init + position 
    public static EnemyController SpawnTest(EnemyStats stats, IMovementBehaviour mov, Vector2 pos, AttackCombo combo, IAttackBehaviour attack)
    {
        var go = Object.Instantiate(stats.enemyPrefab,pos, Quaternion.identity);
        var ctrl = go.GetComponent<EnemyController>();
        var comboCopy = Object.Instantiate(combo);
        for (int i = 0; i < 3; i++)
        {
            comboCopy.steps[i].hitbox = go.transform.Find("AttackHitboxes").transform.Find("Attack" + (i+1)).GetComponent<Collider2D>();

        }
       
        ctrl.Init(stats, mov,comboCopy,attack);
        return ctrl;

        
    }


    public static EnemyController SpawnMage(EnemyStats stats, IMovementBehaviour mov, Vector2 pos, AttackCombo combo, IAttackBehaviour attack)
    {
        var go = Object.Instantiate(stats.enemyPrefab, pos, Quaternion.identity);
        var ctrl = go.GetComponent<EnemyController>();
        
        var comboCopy = Object.Instantiate(combo);
        comboCopy.steps[1].hitbox = go.transform.Find("AttackHitboxes").transform.Find("Attack" + 2).GetComponent<Collider2D>();
        /*
        for (int i = 0; i < 3; i++)
        {
            comboCopy.steps[i].hitbox = go.transform.Find("AttackHitboxes").transform.Find("Attack" + (i + 1)).GetComponent<PolygonCollider2D>();

        }
        */
        ctrl.Init(stats, mov, comboCopy, attack);
        return ctrl;


    }
    public static EnemyController SpawnCom(EnemyStats stats, IMovementBehaviour mov, Vector2 pos, AttackCombo combo, IAttackBehaviour attack)
    {
        var go = Object.Instantiate(stats.enemyPrefab, pos, Quaternion.identity);
        var ctrl = go.GetComponent<EnemyController>();
        var comboCopy = Object.Instantiate(combo);
        int childCount = go.transform.Find("AttackHitboxes").childCount;
        for (int i = 0; i < childCount; i++)
        {
            comboCopy.steps[i].hitbox = go.transform.Find("AttackHitboxes").transform.Find("Attack" + (i + 1)).GetComponent<Collider2D>();

        }

        ctrl.Init(stats, mov, comboCopy, attack);
        return ctrl;


    }




}
