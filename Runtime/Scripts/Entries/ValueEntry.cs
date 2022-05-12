using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace MetaUI.Generic
{
 
    [Serializable]
    public class ValueEntry<T> : Entry<T>
    {
        private T value;
        private UnityEvent<T> changed;
        public ValueEntry(string name, T value) : base(name)
        {
            this.value = value;
            changed = new UnityEvent<T>();
        }

        public override T Get()
        {
            return value;
        }

        public override void Set(T value)
        {
            if (!EqualityComparer<T>.Default.Equals(this.value, value))
            {
                this.value = value;
                changed.Invoke(this.value);
            }
            
        }

        public override void Add(UnityAction<T> action)
        {
            changed.AddListener(action);
        }

        public override void Remove(UnityAction<T> action)
        {
            changed.RemoveListener(action);
        }
    }
}