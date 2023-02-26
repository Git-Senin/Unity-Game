using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(UIManager.instance.Alert.Announce("Pick Up 11 Bananas"));
    }
    private void PickUp()
    {
        Player.instance.gainHealth(10);
        LevelEvents.instance.BananaPickup();
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(UIManager.instance.Alert.Announce($"{LevelEvents.instance.bananaCount + 1} bananas have been picked up"));
            PickUp();
        }
    }
}
