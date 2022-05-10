#region using

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
                    if (_binder == null) _binder = gameObject.AddComponent<Binder>();
                }

                return _binder;
            }
        }

        public string Kind => Binder.Kind;
        public Accessor<string> Title => Binder.Title;

        public Accessor<string> Description => Binder.Description;

        public Accessor<Sprite> Background => Binder.Background;

        public Accessor<Sprite> Icon => Binder.Icon;

        public Accessor<string> ValueString => Binder.ValueString;

        public Accessor<bool> ValueBool => Binder.ValueBool;

        public Accessor<int> ValueInt => Binder.ValueInt;

        public Accessor<float> ValueFloat => Binder.ValueFloat;

        public Accessor<bool> Interactable => Binder.Interactable;


        public void AddClickedListener(UnityAction action)
        {
            Binder.AddClickedListener(action);
        }
    }
}