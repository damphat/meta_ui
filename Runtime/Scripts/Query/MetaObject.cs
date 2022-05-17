#region using

using System;
using MetaUI.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

#endregion

namespace MetaUI
{
    // TODO: Wrap multiple gameObject
    public partial class MetaObject
    {
        public MetaObject(GameObject go)
        {
            gameObject = go;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public GameObject gameObject { get; }

        public string Path => Utils.GetPath(gameObject.transform);

        public MetaObject Add(string template = null, string name = null)
        {
            var item = gameObject.transform.Find(template ?? "Item").gameObject;
            var go = Object.Instantiate(item, gameObject.transform);
            go.SetActive(true);
            if (name != null) go.name = name;

            return new MetaObject(go);
        }

        #region Binding

        public MetaObject Show(bool value)
        {
            gameObject.SetActive(value);
            return this;
        }

        public MetaObject Show(Func<bool> provider)
        {
            Manager.Instance.Show(gameObject, provider);
            return this;
        }

        public MetaObject SetTitle(string title)
        {
            Title.Set(title);
            return this;
        }
        public override string ToString()
        {
            return $"{Path}, {Binder}";
        }

        #endregion
    }
}