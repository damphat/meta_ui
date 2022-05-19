using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaUI.Sigos
{
    public static class Utils
    {
        public static object Get1(object obj, string key)
        {
            if (obj is Sigo tree) return tree.Get1(key);
            return null;
        }

        public static object Set1(object obj, string key, object value)
        {
            if (obj is Sigo tree)
            {
                return tree.Set1(key, value);
            }

            return new Sigo(new Dictionary<string, object>
            {
                {key, value}
            }, false);
        }
        
        public static object Set(object obj, string[] keys, int from, object value)
        {
            if (from >= keys.Length) return value;
            
            var key = keys[from];
            var child = Get1(obj, key);
            return Set1(obj, key, Set(child, keys, from + 1, value));
        }

        private static void AppendIndent(StringBuilder sb, int indent)
        {
            for (var i = 0; i < indent; i++) sb.Append("    ");
        }

        public static void Freeze(object obj)
        {
            if (obj is Sigo tree)
            {
                tree.Freeze();
            }
        }
        
        public static bool IsFrozen(object obj)
        {
            if (obj is Sigo tree)
            {
                return tree.IsFrozen();
            }

            return true;
        }
        
        public static void ToBuilder(object obj, StringBuilder sb, int indent)
        {
            
            if (obj is Sigo tree)
            {
                ToBuilder(tree, sb, indent);
                
            }
            else if (obj is string s)
            {
                sb.Append($"'{s}'");
            }
            else if (obj is null)
            {
                sb.Append("null");
            } else if (obj is bool b)
            {
                sb.Append(b ? "true" : "false");
            }
            else
            {
                sb.Append(obj);
            }

        }
        public static void ToBuilder(Sigo obj, StringBuilder sb, int indent)
        {
            if (!IsFrozen(obj))
            {
                sb.Append('*');
            }
            sb.AppendLine("{");
            foreach (var e in obj)
            {
                AppendIndent(sb, indent + 1);
                sb.Append(e.Key);
                sb.Append(": ");
                ToBuilder(e.Value, sb, indent + 1);
                sb.AppendLine();
            }

            AppendIndent(sb, indent);
            sb.Append('}');
        }

        public static IEnumerable<KeyValuePair<string, object>> Merge(Store store, object state)
        {
            var r = store.children;
            var s = state as IReadOnlyDictionary<string, object>;

            if (r == null && s == null) yield break;
            
            if (r == null)
            {
                foreach (var e in s)
                {
                    yield return e;
                }
                yield break;
            } 
            
            else if (s == null)
            {
                foreach (var e in r)
                {
                    yield return new KeyValuePair<string, object>(e.Key, e.Value);
                }
                yield break;
            }
            else
            {

                foreach (var e in s)
                {
                    yield return r.ContainsKey(e.Key) ? new KeyValuePair<string, object>(e.Key, r[e.Key]) : e;
                }

                foreach (var e in r.Where(e => !s.ContainsKey(e.Key)))
                {
                    yield return new KeyValuePair<string, object>(e.Key, r[e.Key]);
                }
            }
        }
        
        public static void ToBuilder(Store store, StringBuilder sb, int indent)
        {
            var state = store.state;
            if (!IsFrozen(state))
            {
                sb.Append('*');
            }
            
            if (state is null)
            {
                sb.Append("null");
            } else if (state is bool b)
            {
                sb.Append(b ? "true" : "false");
            } else if (state is string s)
            {
                sb.Append($"'{s}'");
            } else if (state is Sigo tree)
            {
            }
            else
            {
                sb.Append(state);
            }
            
            sb.AppendLine("{{");
            foreach (var e in Merge(store, state))
            {
                AppendIndent(sb, indent + 1);
                sb.Append(e.Key);
                sb.Append(": ");
                if (e.Value is Store r)
                {
                    ToBuilder(r, sb, indent + 1);    
                }
                else
                {
                    ToBuilder(e.Value, sb, indent + 1);
                }
                
                sb.AppendLine();
            }

            AppendIndent(sb, indent);
            sb.Append("}}");
        }
    }
    
    public class Sigo : IReadOnlyDictionary<string, object>
    {
        private Dictionary<string, object> data;
        private bool frozen;

        internal Sigo(Dictionary<string, object> data, bool frozen)
        {
            this.data = data;
            this.frozen = frozen;
        }

        public static Sigo Create(params object[] kvs)
        {
            var data = new Dictionary<string, object>();
            var i = 0;
            while (i < kvs.Length - 1)
            {
                data.Add(kvs[i++].ToString(), kvs[i++]);
            }

            return new Sigo(data, false);
        }

        public Sigo Set1(string key, object value)
        {
            if (data.TryGetValue(key, out var old))
            {
                if (Equals(old, value))
                {
                    return this;
                }

                if (frozen)
                {
                    var ret = new Sigo(new Dictionary<string, object>(data), false);
                    ret.data[key] = value;
                    return ret;
                }
                else
                {
                    data[key] = value;
                    return this;
                }
            }
            else
            {
                if (frozen)
                {
                    var ret = new Sigo(new Dictionary<string, object>(data), false);
                    ret.data[key] = value;
                    return ret;
                }
                else
                {
                    data[key] = value;
                    return this;
                }
            }
        }

        public Sigo Set(string path, object value)
        {
            return (Sigo)Utils.Set(this, path.Split('/'), 0, value);
        }

        public object Get1(string key)
        {
            return data.TryGetValue(key, out var value) ? value : null;
        }

        public object Get(string path) => Get(path.Split('/'), 0);
        public object Get(string[] keys, int from)
        {
            object ret = this;
            while (true)
            {
                if (from >= keys.Length) return ret;
                var key = keys[from++];
                if (ret is Sigo tree)
                {
                    ret = tree.Get1(key);
                }
                else
                {
                    return null;
                }
            }
        }

        public void Freeze()
        {
            if (frozen) return;
            foreach (var value in Values)
            {
                if (value is Sigo tree) tree.Freeze();
            }

            frozen = true;
        }

        public bool IsFrozen()
        {
            return frozen;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) data).GetEnumerator();
        }

        public int Count => data.Count;

        public bool ContainsKey(string key)
        {
            return data.ContainsKey(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return data.TryGetValue(key, out value);
        }

        public object this[string key] => data[key];

        public IEnumerable<string> Keys => data.Keys;

        public IEnumerable<object> Values => data.Values;
        
        public override string ToString()
        {
            var sb = new StringBuilder();
            Utils.ToBuilder(this, sb, 0);
            return sb.ToString();
        }
    }
}