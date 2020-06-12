using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AdditionalSpriteData)),CanEditMultipleObjects]
public class NewBehaviourScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AdditionalSpriteData Mine = (AdditionalSpriteData)target;
        if (GUILayout.Button("Set Bounds"))
        {
            Mine.LoadBounds();
        }
        if (GUILayout.Button("Print Bounds"))
        {
            Mine.PrintBounds();
        }
    }
}
