using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 40;
    public Rigidbody2D rb;
    public GameObject explosion;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D (Collider2D collision)    // If Hit
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
            enemy.TakeDamage(damage);
        }

        Instantiate(explosion, transform.position, transform.rotation);

        Destroy(gameObject);                // Destroy
    }

}
