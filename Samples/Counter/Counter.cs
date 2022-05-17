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
            .Title.SetSrc(count.Convert(v => v.ToString()));

        var inc = this.Get("**/inc");
        inc.Clicked(() => count.Set(count.Get() + 1));
        inc.Interactable.SetSrc(count.Convert(v => v < 10));

        var dec = this.Get("**/dec");
        dec.Clicked(() => count.Set(count.Get() - 1));
        dec.Interactable.SetSrc(count.Convert(v => v > 0));
    }
}