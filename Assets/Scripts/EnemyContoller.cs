using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContoller : MonoBehaviour
{

    Rigidbody2D enemyRigidbody;
    PlayerController player;
    public float xSpeed, ySpeed;

    public GameObject bullet;
    public float timeBetweenAttackLow = 0.5f;
    public float timeBetweenAttackHigh = 2f;

    float attackCools;

    public int maxEnemyHealth;
    int enemyHealth;

    GameController cont;
    public int amount;

    Vector2 bounds;

    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();

        attackCools = Random.Range(timeBetweenAttackLow, timeBetweenAttackHigh);
        enemyHealth = maxEnemyHealth;
        cont = FindObjectOfType<GameController>();
        bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        float x = 0f;
        if (player != null)
            if (player.transform.position.x > transform.position.x) // enemy is on the left side of player
                x = xSpeed;
            else if (player.transform.position.x < transform.position.x)//right
                x = -xSpeed;

        enemyRigidbody.AddForce(new Vector2(x, -ySpeed) * Time.deltaTime);

        if (attackCools > 0)
            attackCools -= Time.deltaTime;
        else
            Attack();

        if (transform.position.y < -bounds.y)
        {
            cont.AddScore(-amount);
            Destroy(gameObject);
        }
    }

    void Attack()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        attackCools = Random.Range(timeBetweenAttackLow, timeBetweenAttackHigh);
    }

    public void TakeDamage(int dmg)
    {
        enemyHealth -= dmg;
        if (enemyHealth <= 0) Die();

    }

    void Die()
    {
        cont.AddScore(amount);
        Instantiate(explosion, transform.position, Quaternion.Euler(0,0,0));
        Destroy(gameObject);
    }
}
