using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        Menu,
        Home,
        Base,
        Level,
    }
    public static void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
    public static void Quit()
    {
        Debug.Log("Game has Quit");
        Application.Quit();
    }
}
