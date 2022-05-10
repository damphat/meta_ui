#region using

using System;
using UnityEngine;

#endregion

namespace MetaUI
{
    public partial class WrapGameObject
    {
        public WrapGameObject EachChild(Action<WrapGameObject> action)
        {
            foreach (Transform transform in gameObject.transform) action(From(transform));

            return this;
        }
    }
}