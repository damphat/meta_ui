#region using

using System;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace MetaUI
{
    // TODO: Wrap multiple gameObject
    public partial class WrapGameObject : IBinder
    {
        private Binder _binder; 
        public Binder Binder
        {
            get
            {
                if (_binder == null)
                {
                    _binder = gameObject.GetComponent<Binder>();
                    if (_binder == null)
                    {
                        _binder = gameObject.AddComponent<Binder>();
                    }
                }

                return _binder;
            }
        }

        public string Kind => Binder.Kind;
        public Accessor<string> Title => _binder.Title;

        public Accessor<string> Description => _binder.Description;

        public Accessor<Sprite> Background => _binder.Background;

        public Accessor<Sprite> Icon => _binder.Icon;

        public Accessor<string> ValueString => _binder.ValueString;

        public Accessor<bool> ValueBool => _binder.ValueBool;

        public Accessor<int> ValueInt => _binder.ValueInt;

        public Accessor<float> ValueFloat => _binder.ValueFloat;

        public Accessor<bool> Interactable => _binder.Interactable;


        public void AddClickedListener(UnityAction action)
        {
            Binder.AddClickedListener(action);
        }
    }
}