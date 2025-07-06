using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCommander : MonoBehaviour
{

    [SerializeField] Collider2D coll1;

    [SerializeField] Collider2D coll2;



    
    void Start()
    {
        
        




    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("PlayerGetDmgLayer"))
        {

            StopCoroutine(exp);
            GetComponent<SpriteRenderer>().enabled = false;
            Vector2 targetPos = new Vector2(other.transform.position.x - Mathf.Sign(transform.localScale.x) * 6f, other.transform.position.y);
            transform.position = targetPos;
            StartCoroutine(Finishing());
            GetComponent<Animator>().Play("Explosion");
            GetComponent<SpriteRenderer>().enabled = true;



        }


    }

    IEnumerator Finishing()
    {

        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }

    Coroutine exp;
    public void LaunchExp(Vector2 pos)
    {

        transform.localScale = pos;
        exp = StartCoroutine(Starting());


    }

    IEnumerator Starting()
    {

        coll1.enabled= true;
        GetComponent<Rigidbody2D>().WakeUp();
        yield return new WaitForSeconds(0.1f);
        coll1.enabled = false;
        coll2.enabled= true;
        GetComponent<Rigidbody2D>().WakeUp();
        yield return new WaitForSeconds(0.33f);
        Destroy(gameObject);

    }

}
