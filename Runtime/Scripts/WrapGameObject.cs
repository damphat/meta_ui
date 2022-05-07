#region using

using System;
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
        public GameObject gameObject { get; }


        public string Path => Utils.GetPath(gameObject.transform);

        public WrapGameObject(GameObject go)
        {
            gameObject = go;
        }

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
            // TODO should cached this value?
            var selectable = gameObject.GetComponent<Selectable>();
            selectable.interactable = value;
            return this;
        }

        public WrapGameObject Enable(Func<bool> provider)
        {
            var updater = GetUpdater();
            var selectable = gameObject.GetComponent<Selectable>();
            updater.Set("enable", () => { selectable.interactable = provider(); });
            return this;
        }

        public WrapGameObject Disable(bool value)
        {
            return Enable(!value);
        }

        public WrapGameObject Disable(Func<bool> provider)
        {
            var updater = GetUpdater();

            var selectable = gameObject.GetComponent<Selectable>();
            updater.Set("enable", () => { selectable.interactable = !provider(); });
            return this;
        }

        public WrapGameObject Text(string text)
        {
            gameObject.GetComponentInChildren<Text>().text = text;
            return this;
        }

        public WrapGameObject Text(Func<string> provider)
        {
            var updater = GetUpdater();

            var text = gameObject.GetComponentInChildren<Text>();
            updater.Set("text", () => { text.text = provider(); });
            return this;
        }


        public WrapGameObject Changed(UnityAction<string> changed)
        {
            gameObject.GetComponent<InputField>().onValueChanged.AddListener(changed);
            return this;
        }

        public WrapGameObject Value(string value)
        {
            gameObject.GetComponent<InputField>().text = value;
            return this;
        }

        private Updater GetUpdater()
        {
            var updater = gameObject.GetComponent<Updater>();
            if (updater is null)
            {
                updater = gameObject.AddComponent<Updater>();
            }

            return updater;
        }

        public WrapGameObject Value(Func<string> provider)
        {
            var updater = GetUpdater();

            var input = gameObject.GetComponent<InputField>();
            updater.Set("Value", () => input.text = provider());
            return this;
        }

        public WrapGameObject Update(string name, UnityAction action)
        {
            var updater = GetUpdater();

            updater.Set(name, action);
            return this;
        }

        #endregion
    }
}