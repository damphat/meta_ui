#region using

using UnityEngine;

#endregion

namespace MetaUI
{
    public static class MetaUIExtensions
    {
        public static WrapGameObject Get(this MonoBehaviour behavior, string qs)
        {
            return UI.From(behavior).Get(qs);
        }

        public static WrapGameObject Get(this MonoBehaviour behavior)
        {
            return UI.From(behavior);
        }

        public static void Toast(this MonoBehaviour behaviour, object msg, float? seconds = null)
        {
            MetaUI.Toast.Info(msg, seconds);
        }

        public static void ToastError(this MonoBehaviour behaviour, object msg, float? seconds = null)
        {
            MetaUI.Toast.Error(msg, seconds);
        }

        public static void ToastSuccess(this MonoBehaviour behaviour, object msg, float? seconds = null)
        {
            MetaUI.Toast.Success(msg, seconds);
        }
    }
}