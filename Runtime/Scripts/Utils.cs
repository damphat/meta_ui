#region using

using UnityEngine;

#endregion

namespace MetaUI
{
    internal class Utils
    {
        public static string GetPath(Transform transform)
        {
            var path = "";
            while (true)
            {
                if (transform == null)
                {
                    return "/" + path;
                }

                path = transform.name + "/" + path;
                transform = transform.parent;
            }
        }
    }
}