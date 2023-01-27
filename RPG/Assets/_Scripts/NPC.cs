using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private NPCData data;

    private string Name;
    private Sprite sprite;
    private Indicator indicator;
    private bool interactable = false;

    private void Awake()
    {
        indicator = transform.Find("Indicator").GetComponent<Indicator>();
    }

    private void Start()
    {
        GetData();
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Interacted");
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
 
