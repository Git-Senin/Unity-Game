using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    
    public EnemyData enemyData;

    private Transform target;

    [SerializeField] protected string enemyName;
    [SerializeField] protected string description;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float health;
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    [SerializeField] protected float knockback = 10f;
    [SerializeField] private float distance;

    [SerializeField] private bool faceRight = true;

    protected virtual void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    protected virtual void Start()
    {
        GetData();
    }

    protected virtual void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Player.instance.TakeDamageOverTime(damage, 100000, 1, true));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
        }
    }

    protected virtual void GetData()
    {
        enemyName = enemyData.enemyName;
        description = enemyData.description;

        maxHealth = enemyData.health;
        health = maxHealth;

        speed = enemyData.speed;
        damage = enemyData.damage;

        distance = enemyData.distance;
    }
    public virtual void TakeDamage(float _damage)
    {
        health = Mathf.Clamp(health - _damage, 0, maxHealth);

        if (health > 0)
        {

        }
        else
        {
            Destroy(gameObject);
        }
    }
    public virtual void Heal(float _health)
    {
        health = Mathf.Clamp(health + _health, 0, maxHealth);
    }
    protected virtual void Move()
    {
        if (target.transform.position.x < gameObject.transform.position.x && faceRight) 
            Flip();
        else if (target.transform.position.x > gameObject.transform.position.x && !faceRight) 
            Flip();

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
    protected virtual void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0f);
    }

}
