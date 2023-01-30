using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/NPC")]
public class NPCData : ScriptableObject
{
    public string Name;
    public Sprite sprite;
    [Header("Dialogue")]
    public TextAsset inkJSON;

    // Add npc function options
    public List<string> options;
    public Dictionary<string, bool> optionsDict;
}
