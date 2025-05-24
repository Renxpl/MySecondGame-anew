using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCombatBehaviour : IAttackBehaviour
{
    
    public void Attack(EnemyController self, Rigidbody2D enemyRB, Transform target)
    {
        self.Run(PerformAttack());



        



    }


    IEnumerator PerformAttack()
    {
        yield return new WaitForSeconds(0.5f);
    }
   

    
}
