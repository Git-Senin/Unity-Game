using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;

public class NPC : MonoBehaviour
{
    [Header("NPC DATA")]
    public NPCData data;

    private string Name;
    private Sprite sprite;
    private SpriteRenderer spriteRenderer;
    private Indicator indicator;

    private void Awake()
    {
        GetData();
        gameObject.name = Name;

        indicator = transform.Find("Indicator").GetComponent<Indicator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        spriteRenderer.sprite = sprite;

        indicator.gameObject.SetActive(true);
        indicator.SetExclamation();
        indicator.SetGreen();
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
 
