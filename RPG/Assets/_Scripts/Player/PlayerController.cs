using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance { get; private set; }
    private PlayerControls playerControls;
    public PlayerInput playerInput { get; private set; }
    public InputAction move { get; private set; }
    public InputAction look { get; private set; }
    public InputAction fire { get; private set; }
    public InputAction interract { get; private set; }
    public InputAction menu { get; private set; }
    public InputAction inventory { get; private set; }

    private void Awake()
    {
        // Singleton
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;

        playerControls = new PlayerControls();  
        playerInput = GetComponent<PlayerInput>();
    }
    private void OnEnable()
    {
        // Set Actions
        move        = playerControls.Player.Move;
        look        = playerControls.Player.Look;
        fire        = playerControls.Player.Fire;
        interract   = playerControls.Player.Interract;
        menu        = playerControls.Player.Menu;
        inventory   = playerControls.Player.Inventory;

        // Enable Controller and Events
        EnablePlayerController(true);
        SubscribeEvents(true);
    }
    private void OnDisable()
    {
        // Disable Controller and Events
        EnablePlayerController(false);
        SubscribeEvents(false);
    }

    private void OpenInventory(InputAction.CallbackContext context)
    {
        // Open Inventory Window
    }
    private void Menu(InputAction.CallbackContext context)
    {
        EnablePlayerController(false);
        SubscribeEvents(false);
        UIManager.instance.MenuInterface.gameObject.SetActive(true);
    }
    public void SubscribeEvents(bool subscribe)
    {
        if(subscribe)
        {
            menu.performed += Menu;
        }
        else
        {
            menu.performed -= Menu;
        }
    }
    public void EnablePlayerController(bool enable)
    {
        if (enable)
        {
            move.Enable();
            look.Enable();
            fire.Enable();
            interract.Enable();
            menu.Enable();
            inventory.Enable();
        }
        else
        {
            move.Disable();
            look.Disable();
            fire.Disable();
            interract.Disable();
            menu.Disable();
            inventory.Disable();
        }
    }
    
}
