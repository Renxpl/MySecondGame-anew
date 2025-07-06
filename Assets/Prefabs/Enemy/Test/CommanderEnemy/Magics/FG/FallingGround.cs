using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGround : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 5f;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LaunchProjectile(Vector2 direction)
    {
        StartCoroutine(Starting(direction));
        rb.velocity = direction.normalized * speed;
        rb.transform.localScale = new Vector2(direction.x, 1f);
        Destroy(gameObject, lifetime);
        GetComponent<Animator>().Play("FG");



    }

    IEnumerator Starting(Vector2 direction)
    {


        yield return new WaitForSeconds(0.2f);
        rb.velocity = direction.normalized * speed;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb.velocity = Vector2.zero;
        Destroy(gameObject);



    }



}
