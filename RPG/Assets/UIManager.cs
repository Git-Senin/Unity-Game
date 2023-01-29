using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }
    public PlayerInterface PlayerInterface { get; private set; }
    public MenuInterface MenuInterface { get; private set; }
    public DialogueBox DialogueBox { get; private set; }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
        GetReferences();
    }

    private void GetReferences()
    {
        PlayerInterface     = GetComponentInChildren<PlayerInterface>(true);
        MenuInterface       = GetComponentInChildren<MenuInterface>(true);
        DialogueBox         = GetComponentInChildren<DialogueBox>(true);
    }
}
