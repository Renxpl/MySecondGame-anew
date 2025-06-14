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
    public delegate void UpdateThingsAboutGettingDmg(GameObject receiver, GameObject sender, Collider2D otherCollider, int attackVer);
    public event UpdateThingsAboutGettingDmg onGettingDmg;
    public delegate void TimeSlowing();
    public event TimeSlowing onTimeSlow;
    public delegate void DisableAttackCollider(GameObject sender);
    public event DisableAttackCollider onDisablingAC;
    public delegate void PlayerComboIncrement();
    public event PlayerComboIncrement onComboIncrement;
    public delegate void RegisteringEnemiesToManager(GameObject sender, string type, float amount);
    public event RegisteringEnemiesToManager onRegisteringEnemiesToManager;

    public delegate void DialogueManagement(GameObject sender, string line);
    public event DialogueManagement onDialogueManagement;
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


    public void OnTimeSlow()
    {
       onTimeSlow?.Invoke();
        

    }



    public void OnUpdateCamera()
    {

        onUpdateCamera?.Invoke();

    }

    public void OnGettingDmg(GameObject receiver, GameObject sender, Collider2D otherCollider,int attackVer)
    {
        onGettingDmg?.Invoke(receiver,sender,otherCollider,attackVer);
    }

    public void OnDisablingAttackCollider(GameObject sender)
    {
        onDisablingAC?.Invoke(sender);
    }

    public void OnPlayerComboIncrement()
    {
        onComboIncrement?.Invoke();
    }
    public void OnRegisteringEnemiesToManager(GameObject sender, string type, float amount)
    {

        onRegisteringEnemiesToManager?.Invoke(sender, type, amount);

    }


    public void OnDialogueManagement(GameObject sender, string line)
    {

        onDialogueManagement?.Invoke(sender, line);


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