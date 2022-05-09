- Issues: options.index always fired the same ints
- [x] SetXXX(value) will remove provider
- SetProvider(null)
- GetXXX((value) => {})
- GetXXX(null)
- GetXXXCurrent((value) => {})
- GetXXXNext((value) => {})

- Clicked: replace instead of addListener

- Bug: More than 1 binder in gameobject

- TODO: Awake is called before AddComponent return;

- ISSUE: ComponentInChildren

- LOOP
  SetInt(() => 2)
  AddIntListener((v))

- TODO:
  - prefabs link to prefab
  - prefabs link to gameobject
  - prefabs link to function
  - editor:
    - change prefab will also change to gameobject?
    - change gameobject will ask for update to prefab?
  - runtime: everything is clone
  - download more assets
    - can assets bundle contains code
