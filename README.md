## meta_ui

A tiny library for binding GameObjects with variables.

```cs
    // Counter example

    var canvas = UI.Canvas(); // get Canvas from the current scene
    var count = 0;

    canvas.Get('myPanel/countButton')
        .Text(() => count.ToString())
        .Clicked( () => count++)
```

```cs
    // Login example
    var currentUser = (string)null;

    canvas.Get('current')
        .Text(() => currentUser)
        .Show(() => currentUser != null)

    canvas.Get('login')
        .Clicked(() => currentUser = 'aaa@gmail.com')
        .Show(() => currentUser == null)

    canvas.Get('logout')
        .Clicked(() => currentUser = null)
        .Show(() => currentUser != null)
```

## TODO

- [x] Query
- [ ] Instanciate
- [x] Binding (active, enable, value, changed)

  - [x] Text, Button, Input
  - [ ] Checkbox, Slider
  - [ ] TMP
  - [ ] Custom

- [ ] layout
- [ ] Custom Widgets

  - [ ] toast, clickthrough
  - [ ] messagebox, modal
  - [ ] menu
  - [ ] navigator, push, pop, replace, state-management
  - [ ] list<string>

- [ ] useEffect
