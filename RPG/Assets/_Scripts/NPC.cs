using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using enums;
using TMPro;

public class NPC : MonoBehaviour
{
    [Header("NPC DATA")]
    public NPCData data;

    private string Name;
    private Sprite sprite;
    private SpriteRenderer spriteRenderer;
    private Indicator indicator;
    private Collider2D triggerCollider;

    private void Awake()
    {
        GetData();
        gameObject.name = Name;

        indicator = transform.Find("Indicator").GetComponent<Indicator>();
        triggerCollider = transform.Find("Trigger").GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        spriteRenderer.sprite = sprite;

        indicator.gameObject.SetActive(true);
        indicator.SetExclamation();
        indicator.SetGreen();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleTrigger2D(collision, Handle.Enter);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        HandleTrigger2D(collision, Handle.Exit);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        HandleTrigger2D(collision, Handle.Stay);
    }
    private void HandleTrigger2D(Collider2D collision, Handle option) 
    {
        if(collision.transform.CompareTag("Player"))
        {
            switch (option)
            {
                case Handle.Enter:
                    Player.instance.Select(this);
                    break;
                case Handle.Stay:
                    Player.instance.Select(this);
                    break;
                case Handle.Exit:
                    Player.instance.Select(this, false);
                    break;

                default:
                    Debug.Log(collision.name + " cannot handle Trigger option.");
                    break;
            }
        }
    }
    private void GetData()
    {
        Name = data.Name;
        sprite = data.sprite;
    }
    public void EnableOutline(bool outline)
    {
        if (outline)
            spriteRenderer.material.SetFloat("_Outline", 1);
        else
            spriteRenderer.material.SetFloat("_Outline", 0);
    }
}
 
