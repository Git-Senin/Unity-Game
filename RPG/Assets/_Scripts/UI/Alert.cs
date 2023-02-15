using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Alert : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    private void Awake()
    {
        textMeshProUGUI.text = string.Empty;
    }
    public IEnumerator Announce(string alert)
    {
        textMeshProUGUI.text = alert;
        yield return new WaitForSeconds(2);
        textMeshProUGUI.text = string.Empty;
    }
}
