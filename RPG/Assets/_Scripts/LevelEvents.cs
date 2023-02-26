using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEvents : MonoBehaviour
{
    public static LevelEvents instance { get; private set; }
    public event Action OnBananaPickup;
    public event Action OnBananaComplete;
    public int bananaCount { get; private set; }
    private void Awake()
    {
        instance = this;
        bananaCount = 0;
    }

    public void BananaPickup()
    {
        if (bananaCount >= 10)
        {
            OnBananaComplete?.Invoke();
            StartCoroutine(UIManager.instance.Alert.Announce("GOOD NOW DO IT AGAIN"));
            SceneManager.UnloadSceneAsync(Loader.Scene.Level.ToString());
            SceneManager.LoadSceneAsync(Loader.Scene.Home.ToString());
        }
        else
        {
            OnBananaPickup?.Invoke();
            bananaCount++;
        }
    }


}
 