using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace MetaUI.Generic
{
    public interface Entry
    {
        string Name { get; }
        object Get();
        T GetAs<T>();
        void Set(object value);
        void SetSrc(Entry src);
        void Add(Delegate action);
        void Remove(Delegate action);
    }

    // of component
    // target to child/property
    // listen to parent properties
    public abstract class Entry<T> : Entry
    {
        protected Entry(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public string Error { get; protected set; }

        public Entry<T> Src { get; protected set; }

        public abstract T Get();

        public abstract void Set(T value);

        public virtual void SetSrc(Entry<T> src)
        {
            void Handler(T value)
            {
                Set(value);
            }

            if (Src != src)
            {
                if (Src != null) Src.Remove(Handler);
                Src = src;
                if (Src != null)
                {
                    Set(Src.Get());
                    Src.Add(Handler);
                }
            }
        }

        public abstract void Add(UnityAction<T> action);

        public abstract void Remove(UnityAction<T> action);

        public override bool Equals(object obj)
        {
            return obj is Entry<T> other && Equals(other);
        }

        protected bool Equals(Entry<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Get(), other.Get());
        }

        public override int GetHashCode()
        {
            return Get().GetHashCode();
        }

        public override string ToString()
        {
            if (Error != null)
                return $"{Name}: Error({Error})";
            return $"{Name}: {Get()}";
        }

        object Entry.Get()
        {
            return Get();
        }

        T1 Entry.GetAs<T1>()
        {
            return (T1) Convert.ChangeType(Get(), typeof(T1));
        }

        void Entry.Set(object value)
        {
            Set((T) Convert.ChangeType(value, typeof(T)));
        }

        void Entry.SetSrc(Entry entry)
        {
            SetSrc((Entry<T>) entry);
        }

        void Entry.Add(Delegate entry)
        {
            Add(entry as UnityAction<T>);
        }

        void Entry.Remove(Delegate entry)
        {
            Remove(entry as UnityAction<T>);
        }
    }
}