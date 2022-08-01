using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathCurrent : MonoBehaviour
{

    public FloatVariable maxHealth;
    private float health;
    [SerializeField] private Image currenthealthbar;

    private void Start()
    {
        health = maxHealth.value;
    }

    void Update()
    {
        currenthealthbar.fillAmount = (maxHealth.value / health);
    }
}
