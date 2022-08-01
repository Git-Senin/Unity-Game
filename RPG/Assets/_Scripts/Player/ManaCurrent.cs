using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaCurrent : MonoBehaviour
{
    public FloatVariable maxMana;
    private float mana;
    [SerializeField] private Image currentmanabar;

    private void Start()
    {
        mana = maxMana.value;
    }

    void Update()
    {
        currentmanabar.fillAmount = (maxMana.value / mana);
    }
}
