using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCommander : MonoBehaviour
{
    
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


            GetComponent<SpriteRenderer>().enabled = false;
            Vector2 targetPos = new Vector2(other.transform.position.x - Mathf.Sign(transform.localScale.x) * 2, other.transform.position.y);
            transform.position = targetPos;
            StartCoroutine(Finishing());
            GetComponent<Animator>().Play("Explosion");
            GetComponent<SpriteRenderer>().enabled = true;



        }


    }

    IEnumerator Finishing()
    {

        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
    public void LaunchExp(Vector2 pos)
    {

        transform.localScale = pos;   


    }

}
