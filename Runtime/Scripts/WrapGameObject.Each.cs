#region using

using System;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace MetaUI
{
    public partial class WrapGameObject
    {
        public WrapGameObject EachChild(Action<WrapGameObject> action)
        {
            foreach(Transform transform in gameObject.transform)
            {
                action(this.From(transform));
            }

            return this;
        }
    }
}