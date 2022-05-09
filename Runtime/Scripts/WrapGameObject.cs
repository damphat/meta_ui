#region using

using System;
using Codice.CM.Common.Tree.Partial;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

#endregion

namespace MetaUI
{
    // TODO: Wrap multiple gameObject
    public partial class WrapGameObject
    {
        public WrapGameObject(GameObject go)
        {
            gameObject = go;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public GameObject gameObject { get; }

        public string Path => Utils.GetPath(gameObject.transform);

        public WrapGameObject Add(string template = null, string name = null)
        {
            var item = gameObject.transform.Find(template ?? "Item").gameObject;
            var go = Object.Instantiate(item, gameObject.transform);
            go.SetActive(true);
            if (name != null)
            {
                go.name = name;
            }

            return new WrapGameObject(go);
        }

        #region Binding

        public WrapGameObject Show(bool value)
        {
            gameObject.SetActive(value);
            return this;
        }

        public WrapGameObject Show(Func<bool> provider)
        {
            Manager.Instance.Show(gameObject, provider);
            return this;
        }

        public WrapGameObject Enable(bool value)
        {
            this.Binder.Interactable.Set(value);
            return this;
        }

        public WrapGameObject Enable(Func<bool> provider)
        {
            this.Binder.Interactable.Set(provider);
            return this;
        }

        public WrapGameObject Disable(bool value)
        {
            return Enable(!value);
        }

        public WrapGameObject Disable(Func<bool> provider)
        {
            this.Binder.Interactable.Set(() => !provider());
            return this;
        }

        public virtual WrapGameObject Text(string text)
        {
            this.Binder.Title.Set(text);
            return this;
        }

        public virtual WrapGameObject Text(Func<string> provider)
        {
            this.Binder.Title.Set(provider);
            return this;
        }


        public virtual WrapGameObject Changed(UnityAction<string> changed)
        {
            Binder.ValueString.Get(changed);
            return this;
        }

        public virtual WrapGameObject Value(string value)
        {
            Binder.ValueString.Set(value);
            return this;
        }

        public WrapGameObject Value(Func<string> provider)
        {
            Binder.ValueString.Set(provider);
            return this;
        }

        #endregion
    }
}