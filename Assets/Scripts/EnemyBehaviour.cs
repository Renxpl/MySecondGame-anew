using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    Animator enemyAnimator;
    [SerializeField]GameObject player;
    [SerializeField] bool finisher;
    void Start()
    {
        enemyAnimator= GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        float distance = transform.position.x - player.transform.position.x;
        if (finisher)
        {
            if (distance < 2)
            {
                player.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
                if (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Finisher1"))
                    enemyAnimator.Play("Finisher1");


            }
            else
            {
                player.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                enemyAnimator.Play("Idle");
            }
        }
    }

}
