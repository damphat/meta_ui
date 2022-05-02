#region

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

#endregion

namespace com.damphat.MetaUI
{
    public class WrapGameObject
    {
        private readonly GameObject _gameObject;

        public WrapGameObject(GameObject go)
        {
            _gameObject = go;
        }


        #region Add

        public WrapGameObject Add(string name = null)
        {
            var item = _gameObject.transform.Find("Item").gameObject;
            var go = Object.Instantiate(item, _gameObject.transform);
            go.SetActive(true);
            if (name != null) go.name = name;
            return new WrapGameObject(go);
        }

        #endregion

        #region Get

        public WrapGameObject Get(string name)
        {
            var go = _gameObject.transform.Find(name)?.gameObject;
            if (go == null) throw new UnityException($"WrapGameObject.Get('{name}') => NOT FOUND");

            return new WrapGameObject(go);
        }

        #endregion

        #region Setter

        public WrapGameObject Show(bool value)
        {
            _gameObject.SetActive(value);
            return this;
        }

        public WrapGameObject Show(Func<bool> provider)
        {
            MetaUIManager.Instance.Show(_gameObject, provider);
            return this;
        }

        public WrapGameObject Enable(bool value)
        {
            // TODO should cached this value?
            var selectable = _gameObject.GetComponent<Selectable>();
            selectable.interactable = value;
            return this;
        }

        public WrapGameObject Enable(Func<bool> provider)
        {
            var updater = _gameObject.GetComponent<MetaUIUpdater>() ?? _gameObject.AddComponent<MetaUIUpdater>();
            var selectable = _gameObject.GetComponent<Selectable>();
            updater.Set("enable", () => { selectable.interactable = provider(); });
            return this;
        }

        public WrapGameObject Disable(bool value)
        {
            return Enable(!value);
        }

        public WrapGameObject Disable(Func<bool> provider)
        {
            var updater = _gameObject.GetComponent<MetaUIUpdater>() ?? _gameObject.AddComponent<MetaUIUpdater>();
            var selectable = _gameObject.GetComponent<Selectable>();
            updater.Set("enable", () => { selectable.interactable = !provider(); });
            return this;
        }

        public WrapGameObject Text(string text)
        {
            _gameObject.GetComponentInChildren<Text>().text = text;
            return this;
        }

        public WrapGameObject Text(Func<string> provider)
        {
            var updater = _gameObject.GetComponent<MetaUIUpdater>() ?? _gameObject.AddComponent<MetaUIUpdater>();
            var text = _gameObject.GetComponentInChildren<Text>();
            updater.Set("text", () => { text.text = provider(); });
            return this;
        }

        public WrapGameObject Click(UnityAction click)
        {
            _gameObject.GetComponent<Button>().onClick.AddListener(click);
            return this;
        }

        public WrapGameObject Changed(UnityAction<string> changed)
        {
            _gameObject.GetComponent<InputField>().onValueChanged.AddListener(changed);
            return this;
        }

        public WrapGameObject Value(string value)
        {
            _gameObject.GetComponent<InputField>().text = value;
            return this;
        }

        public WrapGameObject Value(Func<string> provider)
        {
            var updater = _gameObject.GetComponent<MetaUIUpdater>() ?? _gameObject.AddComponent<MetaUIUpdater>();
            var input = _gameObject.GetComponent<InputField>();
            updater.Set("Value", () => input.text = provider());
            return this;
        }

        #endregion
    }
}