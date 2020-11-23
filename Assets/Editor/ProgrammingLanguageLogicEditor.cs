using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProgrammingLanguageLogic))]
[CanEditMultipleObjects]
public class ProgrammingLanguageLogicEditor : Editor
{
    void OnEnable()
    {
    }

    public override void OnInspectorGUI()
    {
        ProgrammingLanguageLogic myScript = (ProgrammingLanguageLogic)target;

        if (GUILayout.Button("Compile"))
        {
            myScript.ConvertToCompiled();
        }
        if (GUILayout.Button("Calculate"))
        {
            myScript.clac = 0;
            myScript.Calculate(myScript.output[myScript.debugindex].parts);
        }
        if (GUILayout.Button("Eval"))
        {
           Debug.Log(JsonUtility.ToJson( myScript.Evaler(myScript.evaltest)));
        }
        DrawDefaultInspector();

    
       
    }
}
