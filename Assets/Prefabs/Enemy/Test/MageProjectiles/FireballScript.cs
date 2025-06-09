using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
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

    public void LaunchFireball(Vector2 direction)
    {

        rb.velocity = direction.normalized * speed;
        rb.transform.localScale = new Vector2(direction.x,1f);
        Destroy(gameObject, lifetime);
        GetComponent<Animator>().Play("Fireball");



    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb.velocity = Vector2.zero;
        Destroy(gameObject);



    }





}
