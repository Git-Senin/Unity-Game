using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpDisplay : MonoBehaviour
{
    public FloatVariable exp;
    public Text expText;

    private void Update()
    {
        expText.text = exp.value.ToString();
    }
}
