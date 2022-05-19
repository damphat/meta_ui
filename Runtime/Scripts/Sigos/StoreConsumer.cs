using System;
using UnityEngine;

namespace MetaUI.Sigos
{
    public class StoreConsumer : MonoBehaviour
    {
        public string path;
        private Store _store = null;
        public Store store
        {
            get => _store;
            set
            {
                if (_store != value)
                {
                    if (_store != null && enabled)
                    {
                        _store.RemoveListener(Changed);
                    }
                    _store = value;
                    if (_store != null && enabled)
                    {
                        _store.AddListener(Changed);
                    }
                }
            }
        }

        public virtual void Changed(object value)
        {
            Debug.Log(value);
        }

        public void Awake()
        {
            if (string.IsNullOrEmpty(path)) return;

            _store = GetComponentInParent<StoreProvider>()?.store?.At(path);
            
        }

        public virtual void OnEnable()
        {
            store?.AddListener(Changed);
        }

        public virtual void OnDisable()
        {
            store?.RemoveListener(Changed);
        }
    }
}
