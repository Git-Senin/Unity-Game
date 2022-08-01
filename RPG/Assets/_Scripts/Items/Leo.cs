using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leo : MonoBehaviour
{
    [SerializeField] private float life = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Player.instance.gainHealth(life);
            Destroy(gameObject);
        }
    }
}
