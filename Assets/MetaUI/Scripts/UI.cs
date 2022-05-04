#region

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

#endregion

namespace com.damphat.MetaUI
{
    public class UI
    {
        public static WrapGameObject Canvas(string name = null)
        {
            var go = name == null ? Object.FindObjectOfType<Canvas>().gameObject : GameObject.Find(name);

            return new WrapGameObject(go);
        }

        public static WrapGameObject Get(string name)
        {            
            return new WrapGameObject(GameObject.Find(name)); 
        }


        public static WrapGameObject Get(GameObject from, string name = null)
        {
            if (name == null)
            {
                return new WrapGameObject(from);
            }
            else {
                return new WrapGameObject(from.transform.Find(name).gameObject);
            }            
        }

        public static WrapGameObject Get(Component from, string name = null)
        {
            if (name == null)
            {
                return new WrapGameObject(from.gameObject);
            }
            else {
                return new WrapGameObject(from.transform.Find(name).gameObject);
            }            
        }

        private static GameObject _toastList;
        private static GameObject _toastItem;

        private static void ToastInit()
        {

            if (_toastList == null)
            {
                var canvas = UI.Canvas().gameObject;
                _toastList = canvas.transform.Find("toast")?.gameObject;
            }

            if (_toastList == null)
            {
                var canvas = UI.Canvas().gameObject;

                _toastList = Object.Instantiate(Resources.Load<GameObject>("toast"), canvas.transform);
            }

            if (_toastItem == null)
            {
                _toastItem = _toastList.transform.Find("Item").gameObject;
            }
        }

        public static Dictionary<Type, Func<object, string>> Debug = new Dictionary<Type, Func<object, string>>
        {
            {typeof(bool), b => (bool)b ? "true" : "false"},
        };
        public static string ToDebugString(object msg)
        {
            if (msg == null) return "null";
            if(Debug.ContainsKey(msg.GetType()))
            {
                return Debug[msg.GetType()](msg);
            }
            return msg.ToString();
        }

        private static readonly Color ColorInfo = new Color(1,1,0,0.2f);
        private static readonly Color ColorResult = new Color(0.5f, 1, 0.5f, 0.2f);
        private static readonly Color ColorError = new Color(1, 0.5f, 0.5f, 0.2f);


        public static void ToastInternal(object message, float seconds, Color color)
        {
            ToastInit();

            var item = Object.Instantiate(_toastItem, _toastList.transform);
            item.SetActive(true);
            item.GetComponentInChildren<Text>().text = ToDebugString(message);
            item.GetComponentInChildren<Image>().color = color;
            Object.Destroy(item, seconds);
        }

        public static void Toast(object message, float seconds = 3)
        {
            ToastInternal(message, seconds, ColorInfo);
        }

        public static void ToastResult(object message, float seconds = 3)
        {
            ToastInternal(message, seconds, ColorResult);
        }

        public static void ToastError(object message, float seconds = 3)
        {
            ToastInternal(message, seconds, ColorError);
        }
    }
}