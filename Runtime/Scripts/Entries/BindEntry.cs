using System;
using UnityEngine.Events;

namespace MetaUI.Generic
{
    public class BindEntry<T> : Entry<T>
    {
        private readonly Func<T> _getter;
        private readonly Action<T> _setter;
        private readonly UnityEvent<T> _changed;

        public BindEntry(string name, Func<T> getter, Action<T> setter, UnityEvent<T> changed) : base(name)
        {
            _getter = getter;
            _setter = setter;
            _changed = changed;
        }

        public override T Get()
        {
            return _getter();
        }

        public override void Set(T value)
        {
            _setter(value);
        }

        public override void Add(UnityAction<T> action)
        {
            _changed.AddListener(action);
        }

        public override void Remove(UnityAction<T> action)
        {
            _changed.RemoveListener(action);
        }
    }
}