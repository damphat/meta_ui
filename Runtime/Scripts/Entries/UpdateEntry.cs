using System;
using System.Collections.Generic;
using MetaUI.Sigos;
using UnityEngine.Events;

namespace MetaUI.Generic
{
    public class UpdateEntry1<T> : Entry<T>, IUpdate1
    {
        private T latest;
        private Func<T> update;
        private readonly UnityEvent<T> uevent;

        public UpdateEntry1(string name, Func<T> update) : base(name)
        {
            latest = update();
            uevent = new UnityEvent<T>();
        }

        public override T Get()
        {
            return latest;
        }

        public override void OnEnable()
        {
            
        }

        public override void OnDisable()
        {
            
        }

        public override void Bind(Store store)
        {
            
        }

        public override void Set(T value)
        {
            latest = value;
        }

        public override void Add(UnityAction<T> action)
        {
            uevent.AddListener(action);
        }

        public override void Remove(UnityAction<T> action)
        {
            uevent.RemoveListener(action);
        }

        public void Update()
        {
            var next = update();
            if (!EqualityComparer<T>.Default.Equals(next, latest))
            {
                latest = next;
                uevent.Invoke(latest);
            }
        }
    }
}