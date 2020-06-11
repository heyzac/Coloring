using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditorInspector : Editor
{
    public LevelEditor Current
    {
        get
        {
            return (LevelEditor)target;
        }
    }

    public override void OnInspectorGUI()
    {
        Debug.Log("ㅗ디ㅣ");

        DrawDefaultInspector();

        if (GUILayout.Button("Save"))
            Current.MapSave();
        if (GUILayout.Button("Load"))
            Current.MapLoad();
    }
}
