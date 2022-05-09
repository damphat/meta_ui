using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
// ReSharper disable InconsistentNaming


namespace MetaUI
{

    // TODO more laze
    [DisallowMultipleComponent]
    public class Binder : MonoBehaviour, IBinder
    {
        public string Kind { get; private set; }

        public Accessor<string> Title { get; private set; } = Accessor<string>.Null();
        public Accessor<string> Description { get; private set; } = Accessor<string>.Null();
        public Accessor<Sprite> Background { get; private set; } = Accessor<Sprite>.Null();
        public Accessor<Sprite> Icon { get; private set; } = Accessor<Sprite>.Null();
        public Accessor<string> ValueString { get; private set; } = Accessor<string>.Null();
        public Accessor<bool> ValueBool { get; private set; } = Accessor<bool>.Null();
        public Accessor<int> ValueInt { get; private set; } = Accessor<int>.Null();
        public Accessor<float> ValueFloat { get; private set; } = Accessor<float>.Null();
        public Accessor<bool> Interactable { get; private set; } = Accessor<bool>.Null();

        protected Button _button;

        #region Clicked

        public virtual void AddClickedListener(UnityAction action)
        {
            if (_button)
            {
                _button.onClick.AddListener(action);
            }
        }

        #endregion



        private void Awake()
        {
            Kind = Load();
        }


        public string Load()
        {
            // TEXT
            Title = Accessor<string>.Text(this.transform);
            if (Title != null)
            {
                return "Title";
            }

            // BUTTON
            _button = GetComponent<Button>();
            if (_button != null)
            {
                Interactable = Accessor<bool>.Interactable(_button);
                Title = Accessor<string>.Text(this.transform.Find("Text"));

                return "Button";
            }

            // INPUT
            ValueString = Accessor<string>.Input(this.transform);

            if (ValueString != null)
            {
                // TODO can be optimize a title bit
                Interactable = Accessor<bool>.Interactable(this.GetComponent<Selectable>());
                Title = Accessor<string>.Text(this.transform.Find("Placeholder"));
                return "Input";
            }

            // DROPDOWN
            ValueInt = Accessor<int>.Dropdown(this.transform);
            if (ValueInt != null)
            {
                Interactable = Accessor<bool>.Interactable(this.GetComponent<Selectable>());
                return "Dropdown";
            }


            // SLIDER
            ValueFloat = Accessor<float>.From(this.GetComponent<Slider>());
            if (ValueFloat != null)
            {
                Interactable = Accessor<bool>.Interactable(this.GetComponent<Selectable>());
                return "Slider";
            }


            // TOGGLE
            ValueBool = Accessor<float>.From(this.GetComponent<Toggle>());
            if (ValueBool != null)
            {
                Interactable = Accessor<bool>.Interactable(this.GetComponent<Selectable>());
                Title = Accessor<string>.Text(this.transform.Find("Label"));
                return "Toggle";
            }

            // IMAGE
            Background = Accessor<Sprite>.From(this.GetComponent<Image>());
            if (Background != null)
            {
                return "Toggle";
            }

            return null;
        }

        public virtual void Update()
        {
            Title.Update();
            Description.Update();
            Background.Update();
            Icon.Update();
            ValueString.Update();
            ValueBool.Update();
            ValueInt.Update();
            ValueFloat.Update();
            Interactable.Update();
        }
    }

    public class Accessor<T>
    {
        public string Name { get; private set; } = "";
        public bool IsNull { get; private set; }
        private Func<T> getter;
        private Action<T> setter;
        private UnityEvent<T> ev;
        private Func<T> provider;
        private UnityAction<T> handler;

        public override string ToString()
        {
            var value = IsNull ? "null" : $"{getter()}";
            return $"{this.Name}={value}";
        }

        public static Accessor<T> Null(string name = null)
        {
            return new Accessor<T>(() => default, value => { }, null)
            {
                Name = name,
                IsNull = true
            };
        }

        public static Accessor<string> Text(Transform go)
        {
            if (go == null) return null;
            return Text(go.GetComponent<Text>()) ?? Text(go.GetComponent<TMP_Text>());
        }

        public static Accessor<string> Text(Text c)
        {
            if (c == null) return null;
            return new Accessor<string>(() => c.text, value => c.text = value, null);
        }

        public static Accessor<string> Text(TMP_Text c)
        {
            if (c == null) return null;
            return new Accessor<string>(() => c.text, value => c.text = value, null);
        }

        public static Accessor<Sprite> From(Image c)
        {
            if (c == null) return null;
            return new Accessor<Sprite>(() => c.sprite, value => c.sprite = value, null);
        }

        public static Accessor<bool> Interactable(Selectable c)
        {
            if (c == null) return null;
            return new Accessor<bool>(() => c.interactable, value => c.interactable = value, null);
        }

        public static Accessor<bool> From(Toggle c)
        {
            if (c == null) return null;
            return new Accessor<bool>(() => c.isOn, value => c.isOn = value, c.onValueChanged);
        }

        public static Accessor<int> Dropdown(Dropdown c)
        {
            if (c == null) return null;
            return new Accessor<int>(() => c.value, value => c.value = value, c.onValueChanged);
        }

        public static Accessor<int> Dropdown(TMP_Dropdown c)
        {
            if (c == null) return null;
            return new Accessor<int>(() => c.value, value => c.value = value, c.onValueChanged);
        }

        public static Accessor<int> Dropdown(Transform transform)
        {
            if (transform == null) return null;
            return Dropdown(transform.GetComponent<Dropdown>()) ?? Dropdown(transform.GetComponent<TMP_Dropdown>());
        }

        public static Accessor<float> From(Slider c)
        {
            if (c == null) return null;
            return new Accessor<float>(() => c.value, value => c.value = value, c.onValueChanged);
        }

        public static Accessor<string> Input(InputField c)
        {
            if (c == null) return null;
            return new Accessor<string>(() => c.text, value => c.text = value, c.onValueChanged);
        }

        public static Accessor<string> Input(TMP_InputField c)
        {
            if (c == null) return null;
            return new Accessor<string>(() => c.text, value => c.text = value, c.onValueChanged);
        }

        public static Accessor<string> Input(Transform transform)
        {
            if (transform == null) return null;
            return Input(transform.GetComponent<InputField>()) ?? Input(transform.GetComponent<TMP_InputField>());
        }

        public Accessor(Func<T> getter, Action<T> setter, UnityEvent<T> ev)
        {
            if (setter == null || getter == null)
            {
                Debug.Break();
            }
            this.getter = getter;
            this.setter = setter;
            this.ev = ev;
        }

        public T Get() => getter();
        public void Get(UnityAction<T> handler, bool replace = true)
        {
            if (this.handler != handler)
            {
                if (replace && this.handler != null) ev.RemoveListener(this.handler);
                this.handler = handler;
                if (this.handler != null) ev.AddListener(this.handler);

                // TODO fire current so that users can receive the first value without waiting
            }
        }

        public void Set(T value)
        {
            provider = null;
            // TODO don't set the same value?
            setter(value);
        }

        // TODO what if call multiple times Set(() => 1);
        public void Set(Func<T> provider)
        {
            if (this.provider != provider)
            {
                this.provider = provider;

                if (this.provider != null) setter(this.provider());
            }
        }

        public void Update()
        {
            // TODO bounce
            if (provider == null) return;

            if (getter == null)
            {
                Debug.Break();
            }

            var p = provider();
            if (!EqualityComparer<T>.Default.Equals(p, getter()))
            {
                setter(p);
            }

        }

    }

}
