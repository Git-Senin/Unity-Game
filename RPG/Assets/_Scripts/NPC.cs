using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;

public class NPC : MonoBehaviour
{
    [Header("NPC DATA")]
    [SerializeField] private NPCData data;

    private string Name;
    private Sprite sprite;
    private DialogueBox dialogueBox;

    private Indicator indicator;
    private bool interactable = false;
    private bool playingDialogue = false;

    private void Awake()
    {
        GetData();
        gameObject.name = Name;

        indicator = transform.Find("Indicator").GetComponent<Indicator>();
    }

    private void Start()
    {
        dialogueBox = UIManager.instance.DialogueBox;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private void Update()
    {
        
        // In range and Interract
        if (interactable && Input.GetKeyDown(KeyCode.F))
        {
            // Player Busy
            if (Player.instance.interacting) return;
            
            // Interact
            dialogueBox.StartInteraction(data);
            Player.instance.interacting = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            indicator.gameObject.SetActive(true);
            indicator.SetExclamation();
            indicator.SetGreen();
            interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            indicator.gameObject.SetActive(false);
            interactable = false;
        }
    }


    private void GetData()
    {
        Name = data.Name;
        sprite = data.sprite;
    }
}
 
