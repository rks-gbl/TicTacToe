using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(UIManager))]
public class UIManagerEditor : Editor
{
    UIManager obj;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Refresh"))
        {
            obj.Refresh();
        }
    }

    private void OnEnable()
    {
        obj = target as UIManager;
    }
}
