using UnityEditor;
using UnityEngine;

namespace com.damphat.MetaUI.Editor
{
    public class MetaUIWindow : EditorWindow
    {
        [MenuItem("Window/Meta UI")]
        public static void ShowWindow()
        {
            GetWindow(typeof(MetaUIWindow));
        }

        public void OnGUI()
        {
            if (GUILayout.Button("Say Hello"))
            {
                UI.Toast("Hello");
            }

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Label 1");
                EditorGUILayout.LabelField("Label 2");
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Label 1");
                EditorGUILayout.LabelField("Label 2");
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}