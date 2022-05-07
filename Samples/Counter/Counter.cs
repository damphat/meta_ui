#region

using System;
using MetaUI;
using UnityEngine;

#endregion

public class Counter : MonoBehaviour
{
    private void Start()
    {
        var count = 0;

        this.Get("**/count")
            .Text(() => count.ToString());

        this.Get("**/inc")
            .Clicked(new Action(() => count++))
            .Enable(() => count < 10);

        this.Get("**/dec")
            .Clicked(new Action(() => count--))
            .Enable(() => count > 0);
    }
}