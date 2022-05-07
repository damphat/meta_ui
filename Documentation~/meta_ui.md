## meta_ui

A tiny library for binding GameObjects with variables.

**Counter example**

```cs
using MetaUI;

public class Counter : MonoBehaviour
{
    private void Start()
    {
        var count = 0;

        this.Get("**/count")
            .Text(() => count.ToString());

        this.Get("**/inc")
            .Clicked(new Action(() => count++))
    }
}

```

**Toast example**

```cs
    // Login example
    private void Start()
    {
        this.Get("**/button1").Clicked(() => this.Toast("message"));
        this.Get("**/button2").Clicked(() => this.ToastError("Error message"));
        this.Get("**/button3").Clicked(() => this.ToastSuccess("Success message"));
    }
```
