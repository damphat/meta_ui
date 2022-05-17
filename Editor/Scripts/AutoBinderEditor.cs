using MetaUI.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AutoBinder))]
public class AutoBinderEditor : Editor
{
    private void Control(Entry<string> entry)
    {
        if (entry.IsNull == false) entry.Set(EditorGUILayout.TextField(entry.Name, entry.Get()));
    }

    private void Control(Entry<bool> entry)
    {
        if (entry.IsNull == false) entry.Set(EditorGUILayout.Toggle(entry.Name, entry.Get()));
    }

    private void Control(Entry<float> entry)
    {
        if (entry.IsNull == false) entry.Set(EditorGUILayout.Slider(entry.Name, entry.Get(), 0, 1));
    }

    private void Control(Entry<int> entry)
    {
        if (entry.IsNull == false)
        {
            var value = EditorGUILayout.TextField(entry.Name, entry.Get().ToString());
            if (int.TryParse(value, out var n)) entry.Set(n);
        }
    }

    private void Control(Entry<Sprite> entry)
    {
        if (entry.IsNull == false)
        {
            var label = new GUIContent(entry.Name);
            var content = new GUIContent(entry.Get()?.texture);
            EditorGUILayout.LabelField(label, content);
        }
    }


    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical();
        var binder = (target as AutoBinder)!;
        // GUILayout.Label(binder.ToString());

        Control(binder.Title);
        Control(binder.Bool);
        Control(binder.Int);
        Control(binder.Float);
        Control(binder.String);
        Control(binder.Background);
        Control(binder.Interactable);
        GUILayout.EndVertical();
    }
}