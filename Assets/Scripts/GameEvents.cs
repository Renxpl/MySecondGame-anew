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

    public void onEnemyHealthDepleted(GameObject sender,int health)
    {
        if (onEHDepleted != null) onEHDepleted?.Invoke(sender, new HealthCount(health));
    }




}

public class HealthCount : EventArgs
{
    public int Health { get; private set; }

    public HealthCount(int health)
    {
        Health = health;
    }
}