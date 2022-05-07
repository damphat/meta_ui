#region using

using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace MetaUI
{
    partial class WrapGameObject
    {
        private void ToastError(object message)
        {
            if (UI.ConfigClickToastErrorSeconds > 0)
            {
                Toast.Error(message, UI.ConfigClickToastErrorSeconds);
            }
        }

        private void ToastSuccess(object message)
        {
            if (UI.ConfigClickToastSuccessSeconds > 0)
            {
                Toast.Success(message, UI.ConfigClickToastSuccessSeconds);
            }
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
                    ToastError(ex.Message);
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
                    ToastSuccess(r);
                }
                catch (Exception ex)
                {
                    ToastError(ex.Message);
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
                    ToastError(ex.Message);
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
                    ToastSuccess(r);
                }
                catch (Exception ex)
                {
                    ToastError(ex.Message);
                    Debug.LogError(ex);
                }
                finally
                {
                    Enable(true);
                }
            });
            return this;
        }
    }
}