using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2;
    public float speed = 20f;
    public float damage = 40f;
    public Rigidbody2D rb;
    public GameObject explosion;
    
    void Start()
    {
        // Velocity
        rb.velocity = transform.right * speed;

        // Nothing Hit
        StartCoroutine(BulletLifeSpan());
    }

    // On Trig
    private void OnTriggerEnter2D (Collider2D collision)    
    {
        // Enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
            enemy.TakeDamage(damage);
            Explode();
        }

        // Terrain
        if (collision.gameObject.CompareTag("Terrain"))
            Explode();

    }
    private void Explode()
    {
        // Explosion
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    private IEnumerator BulletLifeSpan()
    {
        // Wait n Seconds before exploding
        yield return new WaitForSeconds(lifeTime);
        Explode();
    }
}
