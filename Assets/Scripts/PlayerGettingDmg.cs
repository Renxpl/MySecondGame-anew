using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGettingDmg : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] EnemyBehaviour script;
    public bool isGetMoved = false;
    [SerializeField] int health = 15;
    float timer;


    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;


    }




    private void OnTriggerStay2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("EnemyDodgeableAttack") && script.IsAttacking && timer > 1.5f)
        {
            timer = 0;
            //Destroy(transform.parent.gameObject);
            StartCoroutine(GetMoved());
            health--;
            Debug.Log("Player Health" + health);
            if (health <= 0)
            {
                Destroy(transform.parent.gameObject);
            }

        }

    }

    IEnumerator GetMoved()
    {
        isGetMoved = true;
        transform.parent.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 141, 141, 255);
        yield return new WaitForSecondsRealtime(0.15f);
        transform.parent.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        isGetMoved = false;


    }
}
