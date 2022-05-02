    using System;
    using System.Collections.Generic;
    using UnityEngine;

    namespace com.damphat.MetaUI
    {

        [DisallowMultipleComponent]
        internal class MetaUIUpdater : MonoBehaviour
        {
            private readonly Dictionary<string, Action> _dict = new Dictionary<string, Action>();

            public void Set(string key, Action action)
            {
                _dict[key] = action;
            }

            private void Update()
            {
                foreach (var action in _dict.Values)
                {
                    action?.Invoke();
                }
            }
        }
    }