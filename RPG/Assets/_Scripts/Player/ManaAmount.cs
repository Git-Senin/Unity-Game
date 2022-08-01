using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaAmount : MonoBehaviour
{
    [SerializeField]
    private FloatVariable mana;
    public Text manaText;

    void Update()
    {
        manaText.text = "" + (int)mana.value;
    }
}
