using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(Bar))]
public class BarEditor : Editor
{
    private SerializedProperty outline;
    private SerializedProperty gauge;
    private float sliderMin = 0.82f;
    private float sliderMax = 2.0f;

    private void OnEnable()
    {
        outline = serializedObject.FindProperty("outline");
        gauge = serializedObject.FindProperty("gauge");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        Image outlineImage = outline.objectReferenceValue as Image;
        Image gaugeImage = gauge.objectReferenceValue as Image;
        if (outline != null && gaugeImage != null)
        {
            float multiplier = EditorGUILayout.Slider("Border", outlineImage.pixelsPerUnitMultiplier, sliderMin, sliderMax);
            outlineImage.pixelsPerUnitMultiplier = multiplier;
            gaugeImage.pixelsPerUnitMultiplier = multiplier;
        }

        serializedObject.ApplyModifiedProperties();    
    }
}
