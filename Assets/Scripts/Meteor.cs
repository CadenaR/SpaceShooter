using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : EnemyAttribute
{
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-2,0);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.gameObject.GetComponent<CharacterAttribute>().TakeDamage(1f);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}