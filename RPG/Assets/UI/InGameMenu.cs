using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    private bool DisplayMenu = false;
    [SerializeField] private GameObject menu;
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
        menu.SetActive(false);
        Time.timeScale = 1f;
        DisplayMenu = false;
    }

    void Pause()
    {
        player?.SetActive(false);
        menu.SetActive(true);
        Time.timeScale = 0f;
        DisplayMenu = true;   
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
