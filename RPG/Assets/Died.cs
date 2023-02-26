using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Died : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(Countdown());
    }
    private IEnumerator Countdown()
    {
        Debug.Log("Countdown");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Menu");
    }
}
