using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public void PauseTime(bool pause)
    {
        if(pause) Time.timeScale = 0.0f;
        else Time.timeScale = 1.0f;
    }
}