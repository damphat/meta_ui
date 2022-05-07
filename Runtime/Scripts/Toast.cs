#region using

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

#endregion

namespace MetaUI
{
    // TODO: singleton
    // TODO: scene that has more than 1 canvas
    // TODO: save preference
    // TODO: use a separated Canvas?
    public static class Toast
    {
        public static float ConfigSeconds = 3;
        public static Color ConfigColor = new Color(1, 1, 0, 0.2f);
        public static Color ConfigSuccessColor = new Color(0.5f, 1, 0.5f, 0.2f);
        public static Color ConfigErrorColor = new Color(1, 0.5f, 0.5f, 0.2f);


        private static GameObject _toastCanvas;
        private static GameObject _toastList;
        private static GameObject _toastItem;

        private static void ToastInit()
        {
            if (_toastCanvas != null)
            {
                return;
            }

            _toastCanvas = GameObject.Find("Toast Canvas");

            if (_toastCanvas == null)
            {
                _toastCanvas = Object.Instantiate(Resources.Load<GameObject>("Toast Canvas"));
                _toastCanvas.name = "Toast Canvas";
            }

            Object.DontDestroyOnLoad(_toastCanvas);

            _toastList = _toastCanvas.transform.Find("toast").gameObject;
            _toastItem = _toastList.transform.Find("Item").gameObject;
        }

        public static Dictionary<Type, Func<object, string>> Formatter => new Dictionary<Type, Func<object, string>>
        {
            {typeof(bool), b => (bool) b ? "true" : "false"}
        };

        public static string Format(object msg)
        {
            if (msg == null)
            {
                return "null";
            }

            if (Formatter.ContainsKey(msg.GetType()))
            {
                return Formatter[msg.GetType()](msg);
            }

            return msg.ToString();
        }

        private static void ToastInternal(object message, float seconds, Color color)
        {
            ToastInit();

            var item = Object.Instantiate(_toastItem, _toastList.transform);
            item.SetActive(true);
            item.GetComponentInChildren<Text>().text = Format(message);
            item.GetComponentInChildren<Image>().color = color;
            Object.Destroy(item, seconds);
        }

        public static void Info(object message, float? seconds = null)
        {
            ToastInternal(message, seconds ?? ConfigSeconds, ConfigColor);
        }

        public static void Success(object message, float? seconds = null)
        {
            ToastInternal(message, seconds ?? ConfigSeconds, ConfigSuccessColor);
        }

        public static void Error(object message, float? seconds = null)
        {
            ToastInternal(message, seconds ?? ConfigSeconds, ConfigErrorColor);
        }
    }
}