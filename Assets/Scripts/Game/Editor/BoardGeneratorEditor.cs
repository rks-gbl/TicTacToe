using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoardGenerator))]
public class BoardGeneratorEditor : Editor
{
    BoardGenerator obj;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("WARNING ***DONT GENERATE IN EDIT MODE***");

        if(GUILayout.Button("Generate"))
        {
            obj.Generate(obj.size);
        }
    }

    void OnEnable()
    {
        obj = target as BoardGenerator;
    }
}
