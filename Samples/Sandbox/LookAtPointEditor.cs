//C# Example (LookAtPointEditor.cs)
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LookAtPoint))]
[CanEditMultipleObjects]
public class LookAtPointEditor : Editor
{
    SerializedProperty lookAtPoint;

    void OnEnable()
    {
        lookAtPoint = serializedObject.FindProperty("lookAtPoint");
        Debug.Log(lookAtPoint);

    }

    public override void OnInspectorGUI()
    {
        //serializedObject.Update();
        EditorGUILayout.PropertyField(lookAtPoint);
        EditorGUILayout.PropertyField(lookAtPoint);
        EditorGUILayout.PropertyField(lookAtPoint);
        EditorGUILayout.PropertyField(lookAtPoint);
        EditorGUILayout.PropertyField(lookAtPoint);
        EditorGUILayout.PropertyField(lookAtPoint);
        EditorGUILayout.PropertyField(lookAtPoint);
        EditorGUILayout.PropertyField(lookAtPoint);
        EditorGUILayout.PropertyField(lookAtPoint);
        EditorGUILayout.PropertyField(lookAtPoint);
        EditorGUILayout.PropertyField(lookAtPoint);
        EditorGUILayout.PropertyField(lookAtPoint);
        EditorGUILayout.PropertyField(lookAtPoint);
        EditorGUILayout.PropertyField(lookAtPoint);
        EditorGUILayout.PropertyField(lookAtPoint);
        serializedObject.ApplyModifiedProperties();
    }
}