using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private FloatVariable currentVar;
    [SerializeField] private FloatVariable maximumVar;

    [Header("Bar Properties")]
    [SerializeField] private Image outline;
    [SerializeField] private Image gauge;
    [SerializeField] private bool allowExpand;

    private RectTransform rectTransform;
    private Image mask;
    private TextMeshProUGUI TMPText;
    private float expansion;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        mask = gauge.transform.GetChild(0).GetComponent<Image>();
        TMPText = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        if (!allowExpand) return;

        // Set Expansion and position
        expansion = Mathf.Clamp(maximumVar.value, 30, 500);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, expansion);
    }
    private void Update()
    {
        // Fill % and Value
        mask.fillAmount = currentVar.value / maximumVar.value;
        TMPText.text = currentVar.value.ToString("0"); // 0 sets no decimal
    }
}
