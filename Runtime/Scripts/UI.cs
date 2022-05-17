#region using

using UnityEngine;

#endregion

namespace MetaUI
{
    // TODO: no canvas found?
    public class UI
    {
        public static float ConfigClickToastErrorSeconds = 3;
        public static float ConfigClickToastSuccessSeconds = 3;

        public static MetaObject Get(string qs)
        {
            var tr = Query.Get(null, qs);
            if (tr == null)
            {
                var msg = $"Can not query: '{qs}' from the current scene";
                Toast.Info(msg, 10);
                throw new MetaException(msg);
            }

            return new MetaObject(tr.gameObject);
        }

        public static MetaObject From(GameObject go)
        {
            return new MetaObject(go);
        }

        public static MetaObject From(Component go)
        {
            return new MetaObject(go.gameObject);
        }
    }
}