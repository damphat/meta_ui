#region using

using MetaUI;
using UnityEngine;

#endregion

public class Counter : MonoBehaviour
{
    private void Start()
    {
        var count = this.Value(0);

        this.Get("**/count")
            .Text(count.Convert(v => v.ToString()));

        this.Get("**/inc")
            .Clicked(() => count.Set(count.Get() + 1))
            .Enable(count.Convert(v => v < 10));

        this.Get("**/dec")
            .Clicked(() => count.Set(count.Get() - 1))
            .Enable(count.Convert(v => v > 0));
    }
}