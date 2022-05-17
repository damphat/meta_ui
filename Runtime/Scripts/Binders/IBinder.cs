using System;
using UnityEngine;

namespace MetaUI
{
    [Obsolete]
    // TODO: Accessor<bool> Active
    public interface IBinder1
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