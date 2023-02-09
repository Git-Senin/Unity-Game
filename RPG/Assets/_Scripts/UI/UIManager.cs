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

    private InputAction cancel;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
        GetReferences();
        cancel = PlayerController.instance.playerControls.UI.Cancel;
    }
    private void OnEnable()
    {
        cancel.Enable();
        cancel.performed += CloseMenu;
    }
    private void OnDisable()
    {
        cancel.Disable();
        cancel.performed -= CloseMenu;
    }
    private void GetReferences()
    {
        PlayerInterface     = GetComponentInChildren<PlayerInterface>(true);
        MenuInterface       = GetComponentInChildren<MenuInterface>(true);
        DialogueBox         = GetComponentInChildren<DialogueBox>(true);
    }
    public void CloseMenu(InputAction.CallbackContext context)
    {
        MenuInterface.Resume();
    }
}