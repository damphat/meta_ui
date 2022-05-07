#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace MetaUI
{
    [DisallowMultipleComponent]
    internal class Manager : MonoBehaviour
    {
        private static Manager _instance;

        private readonly HashSet<GameObject> _showDictMarked = new HashSet<GameObject>();
        private readonly Dictionary<GameObject, Func<bool>> _showDict = new Dictionary<GameObject, Func<bool>>();

        public static Manager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("MetaUI.Manager");
                    _instance = go.AddComponent<Manager>();
                    DontDestroyOnLoad(go);
                }

                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }

        public void Show(GameObject go, Func<bool> provider)
        {
            if (provider != null)
                _showDict[go] = provider;
            else
                _showDict.Remove(go);
        }

        private void Update()
        {
            foreach (var show in _showDict)
                if (!show.Key)
                    _showDictMarked.Add(show.Key);
                else
                    show.Key.SetActive(show.Value());

            foreach (var o in _showDictMarked) _showDict.Remove(o);
        }
    }
}