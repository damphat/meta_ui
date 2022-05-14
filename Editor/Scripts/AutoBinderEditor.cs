using System;
using MetaUI.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AutoBinder))]
public class AutoBinderEditor : Editor
{
    void Control(Entry<String> entry)
    {
        if (entry != null)
        {
            entry.Set(EditorGUILayout.TextField(entry.Name, entry.Get()));
        }
    }
    
    void Control(Entry<bool> entry)
    {
        if (entry != null)
        {
            entry.Set(EditorGUILayout.Toggle(entry.Name, entry.Get()));
        }
    }
    void Control(Entry<float> entry)
    {
        if (entry != null)
        {
            entry.Set(EditorGUILayout.Slider(entry.Name, entry.Get(), 0, 1));
        }
    }
    
    void Control(Entry<int> entry)
    {
        if (entry != null)
        {
            var value = EditorGUILayout.TextField(entry.Name, entry.Get().ToString());
            if (int.TryParse(value, out var n))
            {
                entry.Set(n);    
            }
            
        }
    }
    
    void Control(Entry<Sprite> entry)
    {
        if (entry != null)
        {
            var label = new GUIContent(entry.Name);
            var content = new GUIContent(entry.Get().texture);
            EditorGUILayout.LabelField(label, content);
        }
    }
    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical();
        var binder = (target as AutoBinder)!;
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
