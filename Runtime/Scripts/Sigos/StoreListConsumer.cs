using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MetaUI.Sigos
{
    public class StoreListConsumer : StoreConsumer
    {
        private static Dictionary<string, object> Empty = new Dictionary<string, object>();

        static IEnumerable<(string, string, object, object)> DumpActions(IReadOnlyDictionary<string, object> a, IReadOnlyDictionary<string, object> b)
        {
            static object Get(IReadOnlyDictionary<string, object> dict, string key) => dict.TryGetValue(key, out var value) ? value : null;
            static string GetAction(object a, object b) => a != b ? a == null ? "add" : b == null ? "remove" : "change" : "keep";

            if (a == null) a = Empty;
            if (b == null) b = Empty;

            foreach (var key in a.Keys.Union(b.Keys))
            {
                var ta = Get(a, key);
                var tb = Get(b, key);

                yield return (key, GetAction(ta, tb), ta, tb);
            }
        }
        
        private object current = null;

        public override void Changed(object obj)
        { 
            if (current != obj)
            {
                foreach (var e in DumpActions(current as IReadOnlyDictionary<string, object>,
                             obj as IReadOnlyDictionary<string, object>))
                {
                    Debug.Log($"{e.Item1}: {e.Item2}");
                }
                current = obj;
            }
        }

    }
}