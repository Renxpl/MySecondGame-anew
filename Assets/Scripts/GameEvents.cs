using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents gameEvents { get; private set;}
    public delegate void PlayerHealthDepleted();
    public  event PlayerHealthDepleted onDepleted;
    public delegate void EnemyHealthDepleted(GameObject sender,HealthCount e);
    public event EnemyHealthDepleted onEHDepleted;
    public delegate void EnemySoldier(GameObject sender, EnemySoldierInfos e);
    public event EnemySoldier onEnemyInteraction;
    public delegate void UpdateThingsAboutCameraBehaviour();
    public event UpdateThingsAboutCameraBehaviour onUpdateCamera;

    void Awake()
    {
        if(gameEvents == null)
        {
            gameEvents = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(gameEvents != this)
        {
            Debug.Log("gameEvents already exists, destroying object!");
            Destroy(this);
        }


    }

    public void OnPlayerHealthDepleted()
    {
        if(onDepleted!= null) onDepleted?.Invoke();

    }

    public void OnEnemyHealthDepleted(GameObject sender,int health)
    {
        if (onEHDepleted != null) onEHDepleted?.Invoke(sender, new HealthCount(health));
    }


    public void OnTimeSlow(GameObject sender, float distance, bool isAttacking)
    {

        if(onEnemyInteraction != null)
        {
            onEnemyInteraction?.Invoke(sender, new EnemySoldierInfos(distance, isAttacking));
        }

    }



    public void OnUpdateCamera()
    {

        onUpdateCamera?.Invoke();

    }


}











//EventArgs
public class HealthCount : EventArgs
{
    public int Health { get; private set; }

    public HealthCount(int health)
    {
        Health = health;
    }
}


public class EnemySoldierInfos : EventArgs
{
    public float Distance { get; private set; }
    public bool IsAttacking { get; private set; }

    public EnemySoldierInfos(float distance, bool isAttacking)
    {
        Distance = distance;
        IsAttacking = isAttacking;
    }
}