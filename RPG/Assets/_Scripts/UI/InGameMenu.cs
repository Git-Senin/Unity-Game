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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (DisplayMenu)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        player?.SetActive(true);
        menuInterface.SetActive(false);
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
}
