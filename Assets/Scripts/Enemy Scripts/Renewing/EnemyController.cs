using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
// attack implementations
public class EnemyController : MonoBehaviour, IDamageable
{
    public EnemyStats Stats{get; private set;}
    public AttackCombo Combo { get; private set;}
    public IMovementBehaviour ChaseMov { get; private set; }
    public IAttackBehaviour AttackBehaviour { get; private set; }
    public Transform PlayerTransform { get; private set; }
    IStateEnemy currentState;

    public int AttackStep { get; private set; }

    public bool IsLockedEnemySprite { get; private set; }

    public float CurrentHealth { get; private set; }
    public float CurrentStance { get; private set; }
    //method which allows child states to run couroutines
    
    public Coroutine Run(IEnumerator routine) => StartCoroutine(routine);
    public void StopCo(Coroutine routine) => StopCoroutine(routine);




    bool isDead = false;

    float timeToBePassedBetweenHits;
    float timeToBePassedForStanceRegen;

    Color baseColor;
    [SerializeField]Color onDmgColor;


    public void Init(EnemyStats stats,IMovementBehaviour mov, AttackCombo combo, IAttackBehaviour attack)
    {
        Stats = stats;
        ChaseMov= mov;
        Combo = combo;
        AttackBehaviour = attack;
        AttackStep = 0;
        CurrentHealth = stats.maxHealth; // setting HP for first time
        CurrentStance = stats.maxStance;// setting Stance for first time

    }

    void Start()
    {
        PlayerTransform = GameObject.FindWithTag("Player").transform;
        ChangeState(new EnemyMovState());
        timeToBePassedBetweenHits = 0;
        timeToBePassedForStanceRegen = 0;
        GameEvents.gameEvents.OnRegisteringEnemiesToManager(gameObject, Stats.enemyType, Stats.maxHealth);
        baseColor = GetComponent<SpriteRenderer>().color;
       

    }


    void Update()
    {
        if (CurrentHealth <= 0)
        {
            this.gameObject.SetActive(false);
        }
        if (isDead) return;

        timeToBePassedBetweenHits += Time.deltaTime;
        timeToBePassedForStanceRegen+= Time.deltaTime;

        if (!IsLockedEnemySprite)
        {

            Vector2 dir = new Vector2(PlayerTransform.position.x - transform.position.x, 0).normalized;
            transform.localScale = new Vector2(dir.x, 1f);//Sprite Adjustment


        }


        StanceRegen();



        currentState.Update(this);

    }

    void StanceRegen()
    {
        if (timeToBePassedForStanceRegen > 2.5f && CurrentStance < Stats.maxStance)
        {
            CurrentStance += Time.deltaTime;



        }
        if(CurrentStance > Stats.maxStance)
        {


            CurrentStance = Stats.maxStance;
        }



    }



    public void ChangeState(IStateEnemy newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);


    }

    public void IncreaseAttackStep()
    {
        AttackStep++;


    }

    public void ResetAttackStep()
    {
        AttackStep= 0;

    }


    public void LockEnemySprite()
    {

        IsLockedEnemySprite = true;


    }
    public void UnlockEnemySprite()
    {

        IsLockedEnemySprite = false;


    }

    public void TakeDamage(float dmg)
    {
        if(timeToBePassedBetweenHits >= 0.25f)
        {
            if (CurrentStance > 0)
            {
                CurrentHealth -= dmg;
            }
            else
            {
                CurrentHealth -= dmg * 2;
            }
            if(CurrentStance>0)CurrentStance--;
            //Debug.Log("enemyhp " + CurrentHealth);
            if (CurrentHealth <= 0) isDead = true;
            timeToBePassedBetweenHits = 0f;
            timeToBePassedForStanceRegen = 0f;
            StartCoroutine(DmgedSpriteChange());
           


        }
        

    }

    IEnumerator DmgedSpriteChange()
    {

        GetComponent<SpriteRenderer>().color = onDmgColor;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = baseColor;


    }

    public void ReplenishingStance()
    {

        CurrentStance = Stats.maxStance;


    }




}
