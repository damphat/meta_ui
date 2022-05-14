using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LookAtPoint))]
[CanEditMultipleObjects]
public class LookAtPointEditor : Editor
{
    private SerializedProperty lookAtPoint;

    private void OnEnable()
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