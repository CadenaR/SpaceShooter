using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAttribute : MonoBehaviour
{
    // Easier balancing of attributes using the editor
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float shootRate;
    [SerializeField]
    protected float damage;
    [SerializeField]
    protected float maxLife;
    [SerializeField]
    public int shootRange; // In world units
    // Life is set to maxLife at the beginning of the game
    protected float life;
    private Color characterColor;
    protected readonly float shootSpeed = 0.145f; // This sets the corret value to calculate the range
    protected float nextShoot = 0.0f;

    public Animator animator;
    public GameObject shoot;
    public GameObject shootPoint;
    public GameObject leftShootPoint;
    public GameObject upShootPoint;
    public GameObject downShootPoint;

    public int audiosource;

    protected void Awake()
    {
        characterColor = gameObject.GetComponent<SpriteRenderer>().color;
        life = maxLife;
    }

    // Player dies in a different way
    protected virtual void Die()
    {
        Destroy(gameObject);
    }


    public void ChangeSpeed(float increment) => this.speed += increment;
    public void ChangeDamage(float increment) => this.damage += increment;
    public void ChangeshootRange(int increment) => this.shootRange += increment;
    public void ChangeshootRate(float increment) => this.shootRate += increment;
    public void ChangeLife(float increment)
    {
        life += increment;
        maxLife += increment;
    }

    public void DealDamage(GameObject target) => target.GetComponent<CharacterAttribute>().TakeDamage(damage);

    public void TakeDamage(float damage)
    {
        StartCoroutine(RestoreColor());
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        life -= damage;
        if (life <= 0) Die();
    }

    IEnumerator RestoreColor()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.GetComponent<SpriteRenderer>().color = characterColor;
    }

    protected void Shoot(GameObject shootPoint, params Vector2[] directions)
    {
        // These define shoot rate between shots
        if (Time.time <= nextShoot)
            return;
        nextShoot = Time.time + shootRate;

        List<GameObject> previousShots = new List<GameObject>();

       

        foreach (Vector2 direction in directions)
        {
            GameObject shot = Instantiate(shoot, shootPoint.GetComponent<Transform>().position, Quaternion.identity) as GameObject;
            shot.GetComponent<Missile>().SetSource(gameObject.tag);
            Physics2D.IgnoreCollision(shot.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            foreach (GameObject shot2 in previousShots)
            {
                Physics2D.IgnoreCollision(shot.GetComponent<Collider2D>(), shot2.GetComponent<Collider2D>());
            }
            previousShots.Add(shot);
            shot.GetComponent<Missile>().SetSpeed(StandarizeDirection(direction.x), StandarizeDirection(direction.y), this.shootSpeed, this.shootRange);
        }
    }

    protected void Shoot(params Vector2[] directions)
    {
        // These define shoot rate between shots
        if (Time.time <= nextShoot) // Defines shoot rate
            return;
        nextShoot = Time.time + shootRate;

        List<GameObject> previousShots = new List<GameObject>();
        

        foreach (Vector2 direction in directions)
        {
            GameObject shot = (GameObject)Instantiate(shoot, SelectShotOrigin(direction).GetComponent<Transform>().position, Quaternion.identity);
            shot.GetComponent<Missile>().SetSource(gameObject.tag);
            Physics2D.IgnoreCollision(shot.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            foreach (GameObject shot2 in previousShots)
            {
                Physics2D.IgnoreCollision(shot.GetComponent<Collider2D>(), shot2.GetComponent<Collider2D>());
            }
            previousShots.Add(shot);
            shot.GetComponent<Missile>().SetSpeed(StandarizeDirection(direction.x), StandarizeDirection(direction.y), this.shootSpeed, this.shootRange);
        }
    }

    protected float StandarizeDirection(float direction)
    {
        return direction > 0 ? 1 : direction < 0 ? -1 : 0;
    }

    private GameObject SelectShotOrigin(Vector2 direction)
    {
        if ((direction.x != 0.0f && direction.y != 0.0f) || (direction.x != 0.0f && direction.y == 0.0f))
        {
            return direction.x > 0.0f ? shootPoint : leftShootPoint;
        }
        else if (direction.x == 0.0f && direction.y != 0.0f)
        {
            return direction.y > 0.0f ? upShootPoint : downShootPoint;
        }
        else
        {
            return upShootPoint;
        }
    }

    protected Vector2 NormalizeSpeed(float x, float y)
    {
        float xSpeed;
        float ySpeed;
        if (x != 0 && y != 0)
        {
            xSpeed = Mathf.Sin(Mathf.PI / 4) * speed * x;
            ySpeed = Mathf.Sin(Mathf.PI / 4) * speed * y;
        }
        else
        {
            xSpeed = x != 0 ? speed * x : 0;
            ySpeed = y != 0 ? speed * y : 0;
        }

        return new Vector2(xSpeed, ySpeed);
    }


}

