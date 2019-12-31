using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelCreator))]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LevelCreator levelCreator = (LevelCreator)target;

        if (DrawDefaultInspector())
        {
            if (levelCreator.autoUpdate)
            {
                levelCreator.GenerateMap();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            levelCreator.GenerateMap();
        }
    }
}