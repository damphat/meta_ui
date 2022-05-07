#region

using UnityEngine;

#endregion

namespace MetaUI
{
    // TODO: no canvas found?
    public class UI
    {
        public static float ConfigClickToastErrorSeconds = 3;
        public static float ConfigClickToastSuccessSeconds = 3;

        public static WrapGameObject Canvas(string name = null)
        {
            var go = name == null ? Object.FindObjectOfType<Canvas>().gameObject : GameObject.Find(name);

            return new WrapGameObject(go);
        }

        public static WrapGameObject Get(string qs)
        {
            var tr = Query.Get(null, qs);
            if (tr == null)
            {
                var msg = $"Can not query: '{qs}' from the current scene";
                Toast.Info(msg, 10);
                throw new MetaUIException(msg);
            }
            return new WrapGameObject(tr.gameObject);
        }

        public static WrapGameObject From(GameObject go)
        {
            return new WrapGameObject(go);
        }

        public static WrapGameObject From(Component go)
        {
            return new WrapGameObject(go.gameObject);
        }
    }
}