using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance { get; private set; }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        //SceneManager.LoadSceneAsync(Loader.Scene.Base.ToString(), LoadSceneMode.Additive);
    }
    public void NextLevel(Loader.Scene scene)
    {
        SceneManager.UnloadSceneAsync(Loader.Scene.Base.ToString());
        SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);
    }
    public int GetActiveSceneCount()
    {
        return SceneManager.sceneCount;
    }
}
