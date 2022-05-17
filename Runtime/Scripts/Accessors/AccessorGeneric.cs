using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace MetaUI
{
    [Obsolete]
    public class Accessor<T> : Accessor
    {
        private readonly Func<T> getter;
        private readonly Action<T> setter;
        private readonly UnityEvent<T> ev;
        private Func<T> provider;
        private UnityAction<T> handler;

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Binder != null) sb.Append(Binder.name);
            sb.Append('<');
            if (Component != null) sb.Append(Component.GetType().Name);
            sb.Append('>');
            sb.Append('.');
            sb.Append(Name);
            sb.Append('=');
            sb.Append(getter());
            return sb.ToString();
        }

        public static Accessor<T> Null(string name)
        {
            return new Accessor<T>(() => default, value => { }, null, null, name);
        }


        public Accessor(Func<T> getter, Action<T> setter, UnityEvent<T> ev, Component c, string name)
            : base(c, name)
        {
            this.getter = getter;
            this.setter = setter;
            this.ev = ev;
        }

        public T Get()
        {
            return getter();
        }

        public void Get(UnityAction<T> handler, bool replace = true)
        {
            if (ev == null) return;

            if (this.handler != handler)
            {
                if (replace && this.handler != null) ev.RemoveListener(this.handler);
                this.handler = handler;
                if (this.handler != null) ev.AddListener(this.handler);

                // TODO fire current so that users can receive the first value without waiting
            }
        }

        public void Set(T value)
        {
            provider = null;

            // TODO add an option that allow to set the same values?
            if (!EqualityComparer<T>.Default.Equals(value, getter())) setter(value);
        }

        // TODO what if call multiple times Set(() => 1);
        public void Set(Func<T> provider)
        {
            if (this.provider != provider)
            {
                this.provider = provider;

                if (this.provider != null)
                {
                    var p = provider();
                    if (!EqualityComparer<T>.Default.Equals(p, getter())) setter(p);
                }
            }
        }

        public override void Update()
        {
            // TODO bounce
            if (provider != null)
            {
                var p = provider();
                if (!EqualityComparer<T>.Default.Equals(p, getter())) setter(p);
            }
        }
    }
}