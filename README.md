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
- [ ] Click
  - [ ] Click(Action)
  - [ ] Click(Func<Task>)
  - [ ] Click(Func<R>)
  - [ ] Click(Func<Task<R>>)
  - [ ] Disable the button while performing
  - [ ] Toast Error if an Exception occurs or the Task failes
  - [ ] Toast the result in case the input is Func<R> or Func<Task<R>>
- [x] Binding (active, enable, value, changed)

  - [x] Text, Button, Input
  - [ ] Checkbox, Slider
  - [ ] TMP
  - [ ] Custom

- [ ] layout
- [ ] Custom Widgets

  - [ ] messagebox, modal
  - [ ] menu
  - [ ] navigator, push, pop, replace, state-management
  - [ ] list<string>

- [ ] useEffect
