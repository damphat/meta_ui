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

```cs
  // toast a message at bottom of screen for 3 seconds

  UI.Toast("Hello !", 3)

```

## TODO

- [x] Query
- [ ] Instanciate
- [x] `UI.Toast(msg, seconds)`, clickthrough, customizable

  - [ ] Info, Warning, Error

- [x] Click

  - [x] Click(Action | Func<Task> | Func<R> | Func<Task<R>>)
  - [x] Disable the button while performing
  - [x] Toast the result and error

- [x] Binding (active, enable, value, changed)

  - [x] Text, Button, Input
  - [ ] Checkbox, Slider
  - [ ] TMP

- [ ] layout
- [ ] Custom Widgets

  - [ ] network image
  - [ ] messagebox, modal
  - [ ] menu
  - [ ] navigator, push, pop, replace, state-management
  - [ ] list<string>

- [ ] useEffect
