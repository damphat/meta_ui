#region using

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace MetaUI
{
    [DisallowMultipleComponent]
    internal class Updater : MonoBehaviour
    {
        private readonly Dictionary<string, UnityAction> _updateDictionary = new Dictionary<string, UnityAction>();


        public void Set(string key, UnityAction action)
        {
            _updateDictionary[key] = action;
        }

        private void Update()
        {
            foreach (var action in _updateDictionary.Values)
            {
                action?.Invoke();
            }
        }
    }
}