using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    [SerializeField] private Sprite[] indicators;
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Set Indicators
    public void SetExclamation()
    {
        spriteRenderer.sprite = indicators[0];
    }
    public void SetQuestion()
    {
        spriteRenderer.sprite = indicators[1];
    }
    public void SetEllipsis()
    {
        spriteRenderer.sprite = indicators[2];
    }
    // Colors
    public void SetGreen()
    {
        spriteRenderer.color = Color.green;
    }
    public void SetRed()
    {
        spriteRenderer.color = Color.red;
    }
    public void SetBlue()
    {
        spriteRenderer.color = Color.blue;
    }
}
