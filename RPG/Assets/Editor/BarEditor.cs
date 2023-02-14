using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Codice.CM.Client.Differences.Graphic;

[CustomEditor(typeof(Bar))]
public class BarEditor : Editor
{
    private SerializedProperty currentVar;
    private SerializedProperty maximumVar;
    private SerializedProperty rectTransform;

    private SerializedProperty outline;
    private SerializedProperty gauge;
    private SerializedProperty fill;

    private SerializedProperty gaugeColor;
    private SerializedProperty fillColor;

    private SerializedProperty minExpansion;
    private SerializedProperty maxExpansion;
    private SerializedProperty barHolder;

    private float currentConst = 10.0f;
    private float maximumConst = 100.0f;
    private float borderSliderMin = 0.82f;
    private float borderSliderMax = 2.0f;
    
    private SerializedProperty allowExpand;
    private SerializedProperty valueFoldout;
    private SerializedProperty useConst;

    private void OnEnable()
    {
        currentVar = serializedObject.FindProperty("currentVar");
        maximumVar = serializedObject.FindProperty("maximumVar");
        rectTransform = serializedObject.FindProperty("rectTransform");

        outline = serializedObject.FindProperty("outline");
        gauge = serializedObject.FindProperty("gauge");
        fill = serializedObject.FindProperty("fill");

        gaugeColor = serializedObject.FindProperty("gaugeColor");
        fillColor = serializedObject.FindProperty("fillColor");

        minExpansion = serializedObject.FindProperty("minExpansion");
        maxExpansion = serializedObject.FindProperty("maxExpansion");
        barHolder = serializedObject.FindProperty("barHolder");

        allowExpand = serializedObject.FindProperty("allowExpand");
        valueFoldout = serializedObject.FindProperty("valueFoldout");
        useConst = serializedObject.FindProperty("useConst");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Base Editor
        //base.OnInspectorGUI();

        // Editor
        Bar bar = (Bar)target;
        if (bar.HasVariables())
            NewEditor();
        else
        {
            EditorGUILayout.LabelField("Values", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.ObjectField(currentVar);
            EditorGUILayout.ObjectField(maximumVar);
            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();    
    }
    private void NewEditor()
    {
        Bar _bar = (Bar)target;
        Image _outline = outline.objectReferenceValue as Image;
        Image _gauge = gauge.objectReferenceValue as Image;

        // Name
        barHolder.stringValue = EditorGUILayout.TextField("Name", barHolder.stringValue);

        // Float Variables
        valueFoldout.boolValue = EditorGUILayout.Foldout(valueFoldout.boolValue, "Values");
        if (valueFoldout.boolValue)
        {
            // Float Variables
            EditorGUI.indentLevel++;
            EditorGUI.BeginDisabledGroup(useConst.boolValue);
            EditorGUILayout.ObjectField(currentVar);
            EditorGUILayout.ObjectField(maximumVar);
            EditorGUI.EndDisabledGroup();

            // Const Option
            useConst.boolValue = EditorGUILayout.Toggle("Use Local Const", useConst.boolValue);
            if (useConst.boolValue)
            {
                EditorGUI.indentLevel++;
                currentConst = EditorGUILayout.FloatField("Current", currentConst);
                maximumConst = EditorGUILayout.FloatField("Maximum", maximumConst);
                _bar.FitExpansion(maximumConst);
                _bar.SetFill(currentConst, maximumConst);
                _bar.SetValues(currentConst, maximumConst);
                EditorGUI.indentLevel--;
            }
            // Use Float Variables
            else
            {
                _bar.FitExpansion();
                _bar.SetFill();
                _bar.SetValues();
            }
            EditorGUI.indentLevel--;
        }

        // Gauge Color
        gaugeColor.colorValue = EditorGUILayout.ColorField("Gauge", gaugeColor.colorValue);
        _bar.SetGaugeColor(new Color(gaugeColor.colorValue.r, gaugeColor.colorValue.g, gaugeColor.colorValue.b));

        // Fill Color
        fillColor.colorValue = EditorGUILayout.ColorField("Fill", fillColor.colorValue);
        _bar.SetFillColor(new Color(fillColor.colorValue.r, fillColor.colorValue.g, fillColor.colorValue.b));

        // Pixels Per Multiplier Slider
        float multiplier = EditorGUILayout.Slider("Border", _outline.pixelsPerUnitMultiplier, borderSliderMin, borderSliderMax);
        _outline.pixelsPerUnitMultiplier = multiplier;
        _gauge.pixelsPerUnitMultiplier = multiplier;
        
        // Expansion 
        EditorGUILayout.LabelField("Expansion", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        allowExpand.boolValue = EditorGUILayout.Toggle("Allow Expand", allowExpand.boolValue);
        if (allowExpand.boolValue)
        {
            minExpansion.floatValue = EditorGUILayout.FloatField("Minimum", minExpansion.floatValue < 30 ? 30 : minExpansion.floatValue);
            maxExpansion.floatValue = EditorGUILayout.FloatField("Maximum", maxExpansion.floatValue);
        }
        EditorGUI.indentLevel--;

    }
}
