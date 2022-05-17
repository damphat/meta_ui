#region using

using MetaUI.Generic;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace MetaUI
{
    // TODO: Wrap multiple gameObject
    public partial class MetaObject
    {
        private AutoBinder _binder;

        public AutoBinder Binder
        {
            get
            {
                if (_binder == null)
                {
                    _binder = gameObject.GetComponent<AutoBinder>();
                    if (_binder == null) _binder = gameObject.AddComponent<AutoBinder>();
                }

                return _binder;
            }
        }

        public Entry<string> Title => Binder.Title;

        public Entry<Sprite> Background => Binder.Background;

        public Entry<string> ValueString => Binder.String;

        public Entry<bool> ValueBool => Binder.Bool;

        public Entry<int> ValueInt => Binder.Int;

        public Entry<float> ValueFloat => Binder.Float;

        public Entry<bool> Interactable => Binder.Interactable;

    }
}