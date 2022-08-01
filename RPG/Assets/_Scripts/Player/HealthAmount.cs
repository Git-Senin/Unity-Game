using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthAmount : MonoBehaviour
{
    public FloatVariable health; 
    public Text healthText;

    void Update()
    {
        healthText.text = health.value.ToString();
    }
}
