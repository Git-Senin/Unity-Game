using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    private bool DisplayMenu = false;
    [SerializeField] private GameObject menuInterface;
    [SerializeField] private GameObject player;
    void Update()
    {
        // On Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Close Other UI
            if (Player.instance.interacting) 
            {
                CloseUI();
                return;
            }

            // Open Menu
            if (DisplayMenu)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        // Enable Player
        player?.SetActive(true);
        menuInterface.SetActive(false);

        // Resume Time
        Time.timeScale = 1f;
        DisplayMenu = false;
    }

    void Pause()
    {
        // Disables Player
        player?.SetActive(false);

        // Disables all menus
        foreach (Transform child in menuInterface.transform)
            child.gameObject.SetActive(false);

        // Opens interface and main ingame menu
        menuInterface.SetActive(true);
        menuInterface.transform.Find("InGameMenu").gameObject.SetActive(true);
        
        // Pauses Time
        Time.timeScale = 0f;
        DisplayMenu = true;   
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1f;
        DisplayMenu = false;
        SceneManager.LoadScene("Menu");
    }

    private void CloseUI()
    {
        // Close Dialogue
        UIManager.instance.DialogueBox.EndInteraction();
    }
}
