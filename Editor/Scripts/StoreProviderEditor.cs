using MetaUI.Sigos;
using UnityEditor;
using UnityEngine;

    [CustomEditor(typeof(StoreProvider))]
    public class StoreProviderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var storeBehavior = (StoreProvider)target; 
            GUILayout.Label(storeBehavior.store?.ToString());
        }
    }
