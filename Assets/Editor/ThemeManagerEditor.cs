using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(ThemeManager))]
[CanEditMultipleObjects]
public class ThemeManagerEditor : Editor
{

    void OnEnable()
    {
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ThemeManager myScript = (ThemeManager)target;
        if (GUILayout.Button("Update"))
        {
            myScript.SetupAll();
        }
        if (GUILayout.Button("Update Selection"))
        {
            myScript.SetupSelection();
        }
        if (GUILayout.Button("line"))
        {
            myScript.libne();
        }
    }
    
}
