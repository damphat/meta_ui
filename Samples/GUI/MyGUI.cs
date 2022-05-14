using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [SerializeField]
    private string str1 = "xxxxx x x x";
    private string str2 = "xxxxx x x x";
    private string str3 = "xxxxx x x x";
    private void OnGUI()
    {
        GUILayout.BeginVertical(GUILayout.Width(300));
        // GUI.Box(Rect.MinMaxRect(0,0,200,200), "box" );
        // GUILayout.BeginVertical();
        if (str2.Length % 2 == 0)
        {
            str1 = GUILayout.TextField(str1, GUILayout.ExpandWidth(true));
        }
        else
        {
            // if (GUILayout.Button("click"))
            // {
            //     str2 += '!';
            // }
        }
        // GUILayout.EndVertical();
        str2 = GUILayout.TextField(str2, GUILayout.ExpandWidth(true));
        str3 = GUILayout.TextField(str3, GUILayout.ExpandWidth(true));
        GUILayout.EndVertical();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
