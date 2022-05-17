using MetaUI.Sigos;
using UnityEditor;
using UnityEngine;

    [CustomEditor(typeof(StoreBehavior))]
    public class StoreBehaviorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var storeBehavior = (StoreBehavior)target; 
            GUILayout.Label(storeBehavior.store?.ToString());
        }
    }
