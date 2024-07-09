using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents gameEvents { get; private set;}
    public delegate void PlayerHealthDepleted();
    public  event PlayerHealthDepleted onDepleted;


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






}
