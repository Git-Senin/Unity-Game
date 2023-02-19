using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        Loader.LoadScene(Loader.Scene.Home);
    }
    public void Quit()
    {
        Loader.Quit();
    }
}
