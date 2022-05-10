using System;
using UnityEngine;
using UnityEngine.Events;

namespace MetaUI
{
    public interface IBinder
    {
        string Kind { get; }
        Accessor<string> Title { get; }
        Accessor<string> Description { get; }
        Accessor<Sprite> Background { get; }
        Accessor<Sprite> Icon { get; }
        Accessor<string> ValueString { get; }
        Accessor<bool> ValueBool { get; }
        Accessor<int> ValueInt { get; }
        Accessor<float> ValueFloat { get; }
        Accessor<bool> Interactable { get; }

    }
}