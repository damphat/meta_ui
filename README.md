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

## TODO

- [x] Query

  - [x] from current: Get("path/to")
  - [x] from root: Get("/path/to")
  - [x] deep seach: Get("/Canvas/\*\*/button")
  - [x] type search: Get("\*\*/.button")
  - [x] index: Get("user/0")

- [x] `UI.Toast(msg, seconds)`,

  - [x] this.Toast(message)
  - [x] this.ToastError(msg)
  - [x] this.ToastResult(msg)
  - [x] can click through
  - [x] use separated Canvas

- [x] Click

  - [x] Click(`Action` | `Func<Task>` | `Func<R>` | `Func<Task<R>>`)
  - [x] Disable the button while performing
  - [x] Toast the result and error

- [x] Binding (active, enable, value, changed)

  - [ ] ICustomBinder
  - [x] Text, Button, Input
  - [ ] Checkbox, Slider
  - [ ] TMP

- [ ] Custom Widgets

  - [ ] network image
  - [ ] messagebox, modal

- [ ] JSON
  - [ ] immutable JSON
  - [ ] component to bind a JSON with a GameObject
