#region

using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

#endregion

namespace com.damphat.MetaUI
{
    public class WrapGameObject
    {
        public GameObject gameObject { get; }

        public WrapGameObject(GameObject go)
        {
            gameObject = go;
        }


        #region Add

        public WrapGameObject Add(string name = null)
        {
            var item = gameObject.transform.Find("Item").gameObject;
            var go = Object.Instantiate(item, gameObject.transform);
            go.SetActive(true);
            if (name != null) go.name = name;
            return new WrapGameObject(go);
        }

        #endregion

        #region Get

        public WrapGameObject Get(string name)
        {
            var go = gameObject.transform.Find(name)?.gameObject;
            if (go == null) throw new UnityException($"WrapGameObject.Get('{name}') => NOT FOUND");

            return new WrapGameObject(go);
        }

        #endregion

        #region Setter

        public WrapGameObject Show(bool value)
        {
            gameObject.SetActive(value);
            return this;
        }

        public WrapGameObject Show(Func<bool> provider)
        {
            MetaUIManager.Instance.Show(gameObject, provider);
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
            var updater = gameObject.GetComponent<MetaUIUpdater>() ?? gameObject.AddComponent<MetaUIUpdater>();
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
            var updater = gameObject.GetComponent<MetaUIUpdater>() ?? gameObject.AddComponent<MetaUIUpdater>();
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
            var updater = gameObject.GetComponent<MetaUIUpdater>() ?? gameObject.AddComponent<MetaUIUpdater>();
            var text = gameObject.GetComponentInChildren<Text>();
            updater.Set("text", () => { text.text = provider(); });
            return this;
        }

        [Obsolete("Used Clicked")]
        public WrapGameObject Click(UnityAction click)
        {
            gameObject.GetComponent<Button>().onClick.AddListener(click);
            return this;
        }

        public WrapGameObject Clicked(Action click)
        {
            gameObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                try
                {
                    click();
                }
                catch (Exception ex)
                {
                    UI.ToastError(ex.Message);
                    Debug.LogError(ex);
                }
            });
            return this;
        }

        public WrapGameObject Clicked<R>(Func<R> click)
        {
            gameObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                try
                {
                    var r = click();
                    UI.ToastResult(r);
                }
                catch (Exception ex)
                {
                    UI.ToastError(ex.Message);
                    Debug.LogError(ex);
                }
            });
            return this;
        }

        public WrapGameObject Clicked(Func<Task> click)
        {
            gameObject.GetComponent<Button>().onClick.AddListener(async () =>
            {
                try
                {
                    Enable(false);
                    await click();
                }
                catch (Exception ex)
                {
                    UI.ToastError(ex.Message);
                    Debug.LogError(ex);
                }
                finally
                {
                    Enable(true);
                }
            });
            return this;
        }

        public WrapGameObject Clicked<R>(Func<Task<R>> click)
        {
            gameObject.GetComponent<Button>().onClick.AddListener(async () =>
            {
                try
                {
                    Enable(false);
                    var r = await click();
                    UI.ToastResult(r);
                }
                catch (Exception ex)
                {
                    UI.ToastError(ex.Message);
                    Debug.LogError(ex);
                }
                finally
                {
                    Enable(true);
                }
            });
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

        public WrapGameObject Value(Func<string> provider)
        {
            var updater = gameObject.GetComponent<MetaUIUpdater>() ?? gameObject.AddComponent<MetaUIUpdater>();
            var input = gameObject.GetComponent<InputField>();
            updater.Set("Value", () => input.text = provider());
            return this;
        }

        public WrapGameObject Update(string name, Action action)
        {
            var updater = gameObject.GetComponent<MetaUIUpdater>() ?? gameObject.AddComponent<MetaUIUpdater>();
            var input = gameObject.GetComponent<InputField>();
            updater.Set(name, action);
            return this;
        }

        #endregion
    }
}