#region

using UnityEngine;
using UnityEngine.UI;

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

        public static string ToDebugString(object msg)
        {
            if (msg == null) return "null";
            if (msg is bool b) return b? "true": "false";
            return msg.ToString();
        }

        public static void Toast(object message, float seconds = 3)
        {
            ToastInit();

            var item = Object.Instantiate(_toastItem, _toastList.transform);
            item.SetActive(true);
            item.GetComponentInChildren<Text>().text = ToDebugString(message);
            Object.Destroy(item, seconds);
        }
    }
}