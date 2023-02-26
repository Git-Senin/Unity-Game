using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Bar : MonoBehaviour
{
    #pragma warning disable CS0414
    [SerializeField] private FloatVariable currentVar;
    [SerializeField] private FloatVariable maximumVar;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TextMeshProUGUI barName;
    [SerializeField] private TextMeshProUGUI TMPText;
    [SerializeField] private Image outline;
    [SerializeField] private Image gauge;
    [SerializeField] private Image fill;
    [SerializeField] private Color gaugeColor;
    [SerializeField] private Color fillColor;
    [SerializeField] private float minExpansion = 30f;
    [SerializeField] private float maxExpansion = 500f;
    [SerializeField] private string barHolder = "Bar";

    [SerializeField] private bool allowExpand;
    [SerializeField] private bool valueFoldout = true;
    [SerializeField] private bool useConst = false;
    #pragma warning restore CS0414

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        SetName();
        SetGaugeColor();
        SetFillColor();
    }
    private void Update()
    {
        // Expansion
        if (allowExpand) 
            FitExpansion(maximumVar.value);

        // Fill %
        SetFill(currentVar.value, maximumVar.value);

        // Value
        SetValues(currentVar.value, maximumVar.value);
    }
    public void AllowExpand(bool expand)
    {
        allowExpand = expand;
    }
    public bool HasVariables() 
    {
        if (currentVar == null) 
            return false;
        if (maximumVar == null) 
            return false;

        return true;
    }
    public void SetName()
    {
        transform.name = barHolder;
        barName.text = barHolder;
    }
    public void SetName(string name)
    {
        SetName();
        barHolder = name;
    }
    public void FitExpansion(float width)
    {
        // Set Expansion
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
            Mathf.Clamp(width, minExpansion, maxExpansion));
    }
    public void FitExpansion()
    {
        FitExpansion(maximumVar.value);
    }
    public void SetFill(float current, float max)
    {
        fill.fillAmount = current / max;
    }
    public void SetFill()
    {
        SetFill(currentVar.value, maximumVar.value);
    }
    public void SetValues(float current, float max)
    {
        // 0 sets no decimal
        TMPText.text = current.ToString("0") + '/' + max.ToString("0");
    }
    public void SetValues()
    {
        SetValues(currentVar.value, maximumVar.value);
    }
    public void SetFillColor()
    {
        fill.color = fillColor;
    }
    public void SetFillColor(Color color)
    {
        fillColor = color;
        SetFillColor();
    }
    private void SetGaugeColor()
    {
        gauge.color = gaugeColor;
    }
    public void SetGaugeColor(Color color)
    {
        gaugeColor = color;
        SetGaugeColor();
    }
}
