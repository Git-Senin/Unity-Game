using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.UnloadSceneAsync(Loader.Scene.Level.ToString());
            SceneManager.LoadSceneAsync(Loader.Scene.Home.ToString());
        }
    }
}
