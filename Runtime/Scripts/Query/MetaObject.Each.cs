#region using

using System;
using UnityEngine;

#endregion

namespace MetaUI
{
    public partial class MetaObject
    {
        public MetaObject EachChild(Action<MetaObject> action)
        {
            foreach (Transform transform in gameObject.transform) action(From(transform));

            return this;
        }
    }
}