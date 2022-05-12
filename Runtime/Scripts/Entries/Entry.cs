using UnityEngine.Events;

namespace MetaUI.Generic
{
    public interface Entry
    {
        string Name { get; }
        object Get();
        void Set(object value);

    }
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

            if (this.Src != src)
            {
                if (this.Src != null)
                {
                    this.Src.Remove(Handler);
                }
                this.Src = src;
                if (this.Src != null)
                {
                    this.Set(this.Src.Get());
                    this.Src.Add(Handler);
                }
            }
        }

        public abstract void Add(UnityAction<T> action);

        public abstract void Remove(UnityAction<T> action);

        public override string ToString()
        {
            if (Error != null)
            {
                return $"{Name}: Error({Error})";
            }
            else
            {
                return $"{Name}: {Get()}";
            }
        }

        object Entry.Get()
        {
            return Get();
        }

        void Entry.Set(object value)
        {
            Set((T)value);
        }
    }
}