using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D bulletRigidbody;
    public float speed;
    int damage = 1;
    // Start is called before the first frame update
    void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        bulletRigidbody.AddForce(Vector2.up * speed);
        Invoke("Disable", 5f);
    }

    private void Disable()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyContoller>().TakeDamage(damage);
            Invoke("Disable", 0.001f);
        }
    }
}
