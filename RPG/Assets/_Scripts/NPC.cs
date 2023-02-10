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
    private Indicator indicator;

    private void Awake()
    {
        GetData();
        gameObject.name = Name;

        indicator = transform.Find("Indicator").GetComponent<Indicator>();
    }
    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;

        indicator.gameObject.SetActive(true);
        indicator.SetExclamation();
        indicator.SetGreen();
    }

    private void GetData()
    {
        Name = data.Name;
        sprite = data.sprite;
    }
}
 
