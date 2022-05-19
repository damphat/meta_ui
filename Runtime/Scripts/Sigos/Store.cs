using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaUI.Sigos
{
    public partial class Store
    {
        private Store parent;
        private string key;
        internal Dictionary<string, Store> children;
        internal object state; // internal because of ToBuilder()
        private object state2;

        private HashSet<Action<object>> changed = new HashSet<Action<object>>();
        public void AddListener(Action<object> a)
        {
            changed.Add(a);
        }

        public void RemoveListener(Action<object> a)
        {
            changed.Remove(a);
        }
        
        public void Update()
        {
            if (!ReferenceEquals(state, state2) || !Utils.IsFrozen(state))
            {
                state2 = state;
                if (changed.Count > 0)
                {
                    Utils.Freeze(state2);
                    foreach (var action in changed)
                    {
                        action.Invoke(state2);
                    }
                }
                foreach (var e in children)
                {
                    e.Value.Update();
                }
            }
        }

        public Store(object state = null)
        {
            children = new Dictionary<string, Store>();
            this.state = state;
        }

        private Store At1(string k)
        {
            if (children.ContainsKey(k))
            {
                return children[k];
            }
            else
            {
                return children[k] = new Store(Utils.Get1(state, k))
                {
                    parent = this,
                    key = k,
                };
            }
        }

        public Store At(string path)
        {
            return path.Split('/').Aggregate(this, (current, k) => current.At1(k));
        }

        public Store Root()
        {
            var ret = this;
            while (ret.parent != null) ret = ret.parent;
            return ret;
        }

        public string Path()
        {
            var store = this;
            var path = store.key;

            while (true)
            {
                store = store.parent;
            }
        }

        private void Up(object s)
        {
            this.state = s;
            foreach (var e in children)
            {
                e.Value.Up(Utils.Get1(s, e.Key));
            }
        }

        public void Set(object s)
        {
            Up(s);

            var p = this.parent;
            var k = this.key;
            while (p != null)
            {
                p.state = s = Utils.Set1(p.state, k, s);
                k = p.key;
                p = p.parent;
            }
        }

        public void Set(string path, object state)
        {
            this.At(path).Set(state);
        }

        public object Get()
        {
            Utils.Freeze(state);
            return state;
        }
        
        public override string ToString()
        {
            var sb = new StringBuilder();
            Utils.ToBuilder(this, sb, 0);
            return sb.ToString();
        }
    }

    partial class Store
    {
        private HashSet<Action<string, string, object, object>> childChanged = new HashSet<Action<string, string, object, object>>();
        public void AddChildListener(Action<string, string, object, object> action)
        {
            childChanged.Add(action);
        }
        
        public void RemoveChildListener(Action<string, string, object, object> action)
        {
            childChanged.Remove(action);
        }
        
    }
}
