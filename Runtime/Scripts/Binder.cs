﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
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

        public Accessor<string> Title { get; private set; }
        public Accessor<string> Description { get; private set; }
        public Accessor<Sprite> Background { get; private set; } 
        public Accessor<Sprite> Icon { get; private set; } 
        public Accessor<string> ValueString { get; private set; } 
        public Accessor<bool> ValueBool { get; private set; } 
        public Accessor<int> ValueInt { get; private set; }
        public Accessor<float> ValueFloat { get; private set; } 
        public Accessor<bool> Interactable { get; private set; }

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
            LoadNull();
        }

        public void LoadNull()
        {
            Title ??= Accessor<string>.Null("Title");
            Description ??= Accessor<string>.Null("Description");
            Background ??= Accessor<Sprite>.Null("Background");
            Icon ??= Accessor<Sprite>.Null("Icon");
            ValueString ??= Accessor<string>.Null("ValueString");
            ValueBool ??= Accessor<bool>.Null("ValueBool");
            ValueInt ??= Accessor<int>.Null("ValueInt");
            ValueFloat ??= Accessor<float>.Null("ValueFloat");
            Interactable ??= Accessor<bool>.Null("Interactable");
        }

        public string Load()
        {
            // TEXT
            Title = Accessor<string>.Text(this.transform, "Title");
            if (Title != null)
            {
                return "Title";
            }

            // BUTTON
            _button = GetComponent<Button>();
            if (_button != null)
            {
                Interactable = Accessor<bool>.Interactable(_button, "Interactable");
                // TODO GetComponentInChildren performance?
                Title = Accessor<string>.Text(GetComponentInChildren<Text>(), "Title") ?? Accessor<string>.Text(GetComponent<TMP_Text>(), "Title");

                // TODO image button
                return "Button";
            }

            // INPUT
            {
                var inputField = this.GetComponent<InputField>();
                if (inputField != null)
                {
                    ValueString = Accessor<string>.Input(inputField, "ValueString");
                    Interactable = Accessor<bool>.Interactable(inputField, "Interactable");
                    Title = Accessor<string>.Text(inputField.placeholder as Text, "Title");
                    return "Input";
                }
            }
            {
                var tmp_InputField = this.GetComponent<TMP_InputField>();
                if (tmp_InputField != null)
                {
                    ValueString = Accessor<string>.Input(tmp_InputField, "ValueString");
                    Interactable = Accessor<bool>.Interactable(tmp_InputField, "Interactable");
                    Title = Accessor<string>.Text(tmp_InputField.placeholder as TMP_Text, "Title"); // TODO TEST with Text
                    return "Input";
                }

            }

            // DROPDOWN
            ValueInt = Accessor<int>.Dropdown(this.transform, "ValueInt");
            if (ValueInt != null)
            {
                Interactable = Accessor<bool>.Interactable(this.GetComponent<Selectable>(), "Interactable");
                return "Dropdown";
            }


            // SLIDER
            ValueFloat = Accessor<float>.From(this.GetComponent<Slider>(), "ValueFloat");
            if (ValueFloat != null)
            {
                Interactable = Accessor<bool>.Interactable(this.GetComponent<Selectable>(), "Interactable");
                return "Slider";
            }


            // TOGGLE
            ValueBool = Accessor<float>.From(this.GetComponent<Toggle>(), "ValueBool");
            if (ValueBool != null)
            {
                Interactable = Accessor<bool>.Interactable(this.GetComponent<Selectable>(), "Interactable");
                Title = Accessor<string>.Text(this.transform.Find("Label"), "Title");
                return "Toggle";
            }

            // IMAGE
            Background = Accessor<Sprite>.From(this.GetComponent<Image>(), "Background");
            if (Background != null)
            {
                return "Background";
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
        public Binder Binder { get; private set; }
        public Component Component { get; private set; }
        public string Name { get; private set; }
        public bool IsNull { get; private set; }
        private Func<T> getter;
        private Action<T> setter;
        private UnityEvent<T> ev;
        private Func<T> provider;
        private UnityAction<T> handler;

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Binder != null) sb.Append(Binder.name);
            sb.Append('<');
            if (Component != null) sb.Append(Component.GetType().Name);
            sb.Append('>');
            sb.Append('.');
            sb.Append(Name);
            sb.Append('=');
            sb.Append(getter());
            return sb.ToString();
        }

        public static Accessor<T> Null(string name)
        {
            return new Accessor<T>(() => default, value => { }, null, null, name);
        }

        public static Accessor<string> Text(Transform go, string name)
        {
            if (go == null) return null;
            return Text(go.GetComponent<Text>(), name) ?? Text(go.GetComponent<TMP_Text>(), name);
        }

        public static Accessor<string> Text(Text c, string name)
        {
            if (c == null) return null;
            return new Accessor<string>(() => c.text, value => c.text = value, null, c, name);
        }

        public static Accessor<string> Text(TMP_Text c, string name)
        {
            if (c == null) return null;
            return new Accessor<string>(() => c.text, value => c.text = value, null, c, name);
        }

        public static Accessor<Sprite> From(Image c, string name)
        {
            if (c == null) return null;
            return new Accessor<Sprite>(() => c.sprite, value => c.sprite = value, null, c, name);
        }

        public static Accessor<bool> Interactable(Selectable c, string name)
        {
            if (c == null) return null;
            return new Accessor<bool>(() => c.interactable, value => c.interactable = value, null, c, name);
        }

        public static Accessor<bool> From(Toggle c, string name)
        {
            if (c == null) return null;
            return new Accessor<bool>(() => c.isOn, value => c.isOn = value, c.onValueChanged, c, name);
        }

        public static Accessor<int> Dropdown(Dropdown c, string name)
        {
            if (c == null) return null;
            return new Accessor<int>(() => c.value, value => c.value = value, c.onValueChanged, c, name);
        }

        public static Accessor<int> Dropdown(TMP_Dropdown c, string name)
        {
            if (c == null) return null;
            return new Accessor<int>(() => c.value, value => c.value = value, c.onValueChanged, c, name);
        }

        public static Accessor<int> Dropdown(Transform transform, string name)
        {
            if (transform == null) return null;
            return Dropdown(transform.GetComponent<Dropdown>(), name) ?? Dropdown(transform.GetComponent<TMP_Dropdown>(), name);
        }

        public static Accessor<float> From(Slider c, string name)
        {
            if (c == null) return null;
            return new Accessor<float>(() => c.value, value => c.value = value, c.onValueChanged, c, name);
        }

        public static Accessor<string> Input(InputField c, string name)
        {
            if (c == null) return null;
            return new Accessor<string>(() => c.text, value => c.text = value, c.onValueChanged, c, name);
        }

        public static Accessor<string> Input(TMP_InputField c, string name)
        {
            if (c == null) return null;
            return new Accessor<string>(() => c.text, value => c.text = value, c.onValueChanged, c, name);
        }

        public static Accessor<string> Input(Transform transform, string name)
        {
            if (transform == null) return null;
            return Input(transform.GetComponent<InputField>(), name) ?? Input(transform.GetComponent<TMP_InputField>(), name);
        }

        public Accessor(Func<T> getter, Action<T> setter, UnityEvent<T> ev, Component c, string name)
        {
            this.Name = name;
            this.Component = c;
            this.getter = getter;
            this.setter = setter;
            this.ev = ev;
        }

        public T Get() => getter();
        public void Get(UnityAction<T> handler, bool replace = true)
        {
            if (ev == null) return;

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

            

            var p = provider();
            if (!EqualityComparer<T>.Default.Equals(p, getter()))
            {
                setter(p);
            }

        }

    }

}
