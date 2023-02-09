using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInterface : MonoBehaviour
{
    // Controls inGameMenus

    [SerializeField] private Transform menuInterface;

    public void Pause()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }
    public void Resume()
    {
        gameObject.SetActive(false);
        foreach (Transform child in menuInterface)
            child.gameObject.SetActive(false);
        Time.timeScale = 1f;
        PlayerController.instance.EnablePlayerController(true);
        PlayerController.instance.SubscribeEvents(true);
    }
    public void ToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
