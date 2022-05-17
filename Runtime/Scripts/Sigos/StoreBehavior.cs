using System;
using UnityEngine;

namespace MetaUI.Sigos
{
    public class StoreBehavior : MonoBehaviour
    {
        private Store _store;
        public Store store => _store ??= new Store();

        public void Set(Sigo state)
        {
            store.Set(state);
        }
        public Store At(string path)
        {
            return store.At(path);
        }

        public void Awake()
        {
            this.Toast("StoreBehavior.Awake");
        }

        public void Start()
        {
            this.Toast("StoreBehavior.Start");
        }
    }
}