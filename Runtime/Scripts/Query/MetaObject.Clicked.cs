#region using

using System;
using System.Threading.Tasks;
using UnityEngine;

#endregion

namespace MetaUI
{
    partial class MetaObject
    {
        private void ToastError(object message)
        {
            if (UI.ConfigClickToastErrorSeconds > 0) Toast.Error(message, UI.ConfigClickToastErrorSeconds);
        }

        private void ToastSuccess(object message)
        {
            if (UI.ConfigClickToastSuccessSeconds > 0) Toast.Success(message, UI.ConfigClickToastSuccessSeconds);
        }

        public MetaObject Clicked(Action click)
        {
            Binder.Clicked.AddListener(() =>
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

        public MetaObject Clicked<R>(Func<R> click)
        {
            Binder.Clicked.AddListener(() =>
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

        public MetaObject Clicked(Func<Task> click)
        {
            Binder.Clicked.AddListener(async () =>
            {
                try
                {
                    Interactable.Set(false);
                    await click();
                }
                catch (Exception ex)
                {
                    ToastError(ex.Message);
                    Debug.LogError(ex);
                }
                finally
                {
                    Interactable.Set(true);
                }
            });
            return this;
        }

        public MetaObject Clicked<R>(Func<Task<R>> click)
        {
            Binder.Clicked.AddListener(async () =>
            {
                try
                {
                    Interactable.Set(false);
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
                    Interactable.Set(true);
                }
            });
            return this;
        }
    }
}