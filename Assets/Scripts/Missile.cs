using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private float xSpeed = 0f;
    private float ySpeed = 0f;
    private float lifetime = 0f;
    private string sourceTag; // Character's type that created the bullet

    public void SetSpeed(float x, float y, float speed, int range)
    {
        this.lifetime = (float)range / 7.5f; // 1 / 7.5 seconds is almost a unit in the world
        StartCoroutine(DestroyBullet());

        if (x != 0 && y != 0)
        {
            this.xSpeed = Mathf.Sin(Mathf.PI / 4) * speed * x;
            this.ySpeed = Mathf.Sin(Mathf.PI / 4) * speed * y;
        }
        else
        {
            this.xSpeed = x != 0 ? speed * x : 0;
            this.ySpeed = y != 0 ? speed * y : 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x += xSpeed;
        position.y += ySpeed;
        transform.position = position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // A bullet cannot deny another
        if (!collision.gameObject.CompareTag("missile"))
        {
            Destroy(gameObject);
            // Bullets do not deal damage over walls and also enemies do not hurt another enemy, but a player can damage another player
            if (!collision.gameObject.CompareTag("Wall") && (collision.gameObject.CompareTag("Player") || !collision.gameObject.CompareTag(sourceTag)))
            {
                collision.transform.gameObject.GetComponent<CharacterAttribute>().TakeDamage(1f);
            }
        }
    }

    public void SetSource(string tag) => this.sourceTag = tag;

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
