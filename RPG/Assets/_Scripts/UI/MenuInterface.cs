using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuInterface : MonoBehaviour
{
    // Controls inGameMenus

    [SerializeField] private Transform menuInterface;
    private PlayerInput playerInput;

    private void OnEnable()
    {
        Pause();

        // Set Player Controls to UI
        playerInput = PlayerController.instance.playerInput;
        playerInput.SwitchCurrentActionMap("UI");
        playerInput.actions["Cancel"].performed += Cancel;

        // Open InGame Menu
        menuInterface.Find("InGameMenu").gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        // Set Player Controls to Player
        // Enable Player Controller
        playerInput.SwitchCurrentActionMap("Player");
        PlayerController.instance.EnablePlayerController(true);
        PlayerController.instance.SubscribeEvents(true);
        playerInput.actions["Cancel"].performed -= Cancel;
    }

    private void Cancel(InputAction.CallbackContext context)
    {
        Resume();
    }

    public void Pause()    
    {
        // Pause Time
        UIManager.instance.PauseTime(true);
    }
    public void Resume()
    {
        // Resume Time
        UIManager.instance.PauseTime(false);

        // Disable Other Menus
        foreach (Transform child in menuInterface)
            child.gameObject.SetActive(false);

        // Return to Player
        gameObject.SetActive(false);
    }
    public void ToMainMenu()
    {
        // Reset Time
        Time.timeScale = 1f;

        // Load Main Menu
        SceneManager.LoadScene("Menu");
    }
}
