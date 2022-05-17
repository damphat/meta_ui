#region using

using System;
using MetaUI.Generic;
using UnityEngine;

#endregion

namespace MetaUI
{
    public static class Extensions
    {
        public static MetaObject Get(this MonoBehaviour behavior, string qs)
        {
            return UI.From(behavior).Get(qs);
        }

        public static MetaObject Get(this MonoBehaviour behavior)
        {
            return UI.From(behavior);
        }


        public static Entry<T> Value<T>(this MonoBehaviour behavior, T value, string name = "<none>")
        {
            return new ValueEntry<T>(name, value);
        }

        public static Entry<T> Value<T>(this MonoBehaviour behavior, Func<T> update, string name = "<none>")
        {
            return new UpdateEntry<T>(name, update);
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