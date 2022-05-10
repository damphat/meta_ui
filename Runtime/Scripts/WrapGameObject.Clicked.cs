#region using

using System;
using System.Threading.Tasks;
using UnityEngine;

#endregion

namespace MetaUI
{
    partial class WrapGameObject
    {
        private void ToastError(object message)
        {
            if (UI.ConfigClickToastErrorSeconds > 0) Toast.Error(message, UI.ConfigClickToastErrorSeconds);
        }

        private void ToastSuccess(object message)
        {
            if (UI.ConfigClickToastSuccessSeconds > 0) Toast.Success(message, UI.ConfigClickToastSuccessSeconds);
        }

        public WrapGameObject Clicked(Action click)
        {
            AddClickedListener(() =>
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
            AddClickedListener(() =>
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
            AddClickedListener(async () =>
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
            AddClickedListener(async () =>
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