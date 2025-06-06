using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GettingDMG : MonoBehaviour
{
    // Start is called before the first frame update
    //here will implement event or interface system here
    PlayerMovement script;
    public bool isGetMoved = false;
    [SerializeField] int health = 3;
    float timer;
    


    void Start()
    {
        script = GameObject.Find("Player").GetComponent<PlayerMovement>();


    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime; 


    }


   

    private void OnTriggerStay2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("LightAttack") && script.IsLightAttacking && timer > 0.15f)
        {
            timer = 0;
            //Destroy(transform.parent.gameObject);
            StartCoroutine(GetMoved());
            health-= 1;
            Debug.Log("Enemy Health" + health);
            GameEvents.gameEvents.OnEnemyHealthDepleted(transform.parent.gameObject,health);
            

        }

    }
    
    IEnumerator GetMoved()
    {
        isGetMoved =true;
        yield return new WaitForSecondsRealtime(0.15f);
        isGetMoved=false;


    }
}
