using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyAttribute
{
    // Update is called once per frame

    void Update()
    {
        playerInY = player.transform.position.y;
        Move();
        Shoot(Vector2.left);
    }

    void Move()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-2, playerInY - transform.position.y);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}