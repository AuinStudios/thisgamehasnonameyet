using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogController)), CanEditMultipleObjects]
public class DialogControllerEditor : Editor
{
    DialogController dialogController;

    Vector2 ScrollPos;

    void OnEnable()
    {
        dialogController = target as DialogController;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        dialogController.dialogDirectory = EditorGUILayout.TextField("Dialog directory: ", dialogController.dialogDirectory, GUILayout.MaxHeight(50));
        dialogController.dialogFileName = EditorGUILayout.TextField("Dialog file: ", dialogController.dialogFileName);

        ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos, GUILayout.Height(100));
        EditorGUILayout.TextField("Dialog content: ", dialogController.dialogContent, GUILayout.Height(50));
        EditorGUILayout.EndScrollView();

        //dialogContent.stringValue = EditorGUILayout.TextArea(dialogContent.stringValue, GUILayout.MaxHeight(75));

        serializedObject.ApplyModifiedProperties();
    }
}
