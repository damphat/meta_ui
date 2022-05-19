using System;
using UnityEngine;

namespace MetaUI.Sigos
{
    public class StoreProvider : MonoBehaviour
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

        private void Update()
        {
            store.Update();
        }

    }
}