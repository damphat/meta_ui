#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace com.damphat.MetaUI
{
    [DisallowMultipleComponent]
    internal class MetaUIManager : MonoBehaviour
    {
        private static MetaUIManager _instance;

        private readonly HashSet<GameObject> _showDictMarked = new HashSet<GameObject>();
        private readonly Dictionary<GameObject, Func<bool>> _showDict = new Dictionary<GameObject, Func<bool>>();

        public static MetaUIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("MetaUIManager");
                    _instance = go.AddComponent<MetaUIManager>();
                    DontDestroyOnLoad(go);
                }

                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }

        // TODO: wrap `go` in a node to control its activation
        // TODO: remove/undo in editor (how to restore)
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