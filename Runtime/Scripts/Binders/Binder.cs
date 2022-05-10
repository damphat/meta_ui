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

        // TODO imporove this
        public virtual void AddClickedListener(UnityAction action)
        {
            if (_button) _button.onClick.AddListener(action);
        }

        #endregion


        private void Awake()
        {
            // TODO Awake method will be executed before AddComponent returns?
            Kind = HeuristicBinding();
            ConvertNullToNullObjects();
        }

        private void ConvertNullToNullObjects()
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

        private string HeuristicBinding()
        {
            // TEXT
            Title = Accessor.Text(transform, "Title");
            if (Title != null) return "Title";

            // BUTTON
            _button = GetComponent<Button>();
            if (_button != null)
            {
                Interactable = Accessor.Interactable(_button, "Interactable");
                // TODO GetComponentInChildren performance?
                Title = Accessor.Text(GetComponentInChildren<Text>(), "Title") ??
                        Accessor.Text(GetComponentInChildren<TMP_Text>(), "Title");

                // TODO image button
                return "Button";
            }

            // INPUT
            {
                var inputField = GetComponent<InputField>();
                if (inputField != null)
                {
                    ValueString = Accessor.Input(inputField, "ValueString");
                    Interactable = Accessor.Interactable(inputField, "Interactable");
                    Title = Accessor.Text(inputField.placeholder as Text, "Title");
                    return "Input";
                }
            }
            {
                var tmp_InputField = GetComponent<TMP_InputField>();
                if (tmp_InputField != null)
                {
                    ValueString = Accessor.Input(tmp_InputField, "ValueString");
                    Interactable = Accessor.Interactable(tmp_InputField, "Interactable");
                    Title = Accessor.Text(tmp_InputField.placeholder as TMP_Text, "Title"); // TODO TEST with Text
                    return "Input";
                }
            }

            // DROPDOWN
            ValueInt = Accessor.Dropdown(transform, "ValueInt");
            if (ValueInt != null)
            {
                Interactable = Accessor.Interactable(GetComponent<Selectable>(), "Interactable");
                return "Dropdown";
            }


            // SLIDER
            ValueFloat = Accessor.From(GetComponent<Slider>(), "ValueFloat");
            if (ValueFloat != null)
            {
                Interactable = Accessor.Interactable(GetComponent<Selectable>(), "Interactable");
                return "Slider";
            }


            // TOGGLE
            ValueBool = Accessor.From(GetComponent<Toggle>(), "ValueBool");
            if (ValueBool != null)
            {
                Interactable = Accessor.Interactable(GetComponent<Selectable>(), "Interactable");
                Title = Accessor.Text(transform.Find("Label"), "Title");
                return "Toggle";
            }

            // IMAGE
            Background = Accessor.From(GetComponent<Image>(), "Background");
            if (Background != null) return "Background";

            return null;
        }

        public virtual void Update()
        {
            // TODO assembly reload will clear all state
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
}