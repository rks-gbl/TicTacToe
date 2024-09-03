using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraSizeUpdater))]
public class CameraSizeUpdaterEditor : Editor
{
    CameraSizeUpdater obj;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Reset"))
        {
            obj.Reset();
        }
    }

    void OnEnable()
    {
        obj = target as CameraSizeUpdater;
    }
}
