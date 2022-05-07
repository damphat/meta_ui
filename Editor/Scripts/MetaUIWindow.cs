#region using

using UnityEditor;
using UnityEngine;

#endregion

namespace MetaUI.Editor
{
    public class MetaUIWindow : EditorWindow
    {
        public void OnGUI()
        {
            if (GUILayout.Button("Say Hello"))
            {
                Toast.Info("Hello");
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

        [MenuItem("Window/Meta UI")]
        public static void ShowWindow()
        {
            GetWindow(typeof(MetaUIWindow));
        }
    }
}