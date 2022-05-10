using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// ReSharper disable InconsistentNaming


namespace MetaUI
{
    // TODO more lazy
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

        // TODO improve this
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
            Title ??= Accessor<string>.Null(nameof(Title));
            Description ??= Accessor<string>.Null(nameof(Description));
            Background ??= Accessor<Sprite>.Null(nameof(Background));
            Icon ??= Accessor<Sprite>.Null(nameof(Icon));
            ValueString ??= Accessor<string>.Null(nameof(ValueString));
            ValueBool ??= Accessor<bool>.Null(nameof(ValueBool));
            ValueInt ??= Accessor<int>.Null(nameof(ValueInt));
            ValueFloat ??= Accessor<float>.Null(nameof(ValueFloat));
            Interactable ??= Accessor<bool>.Null(nameof(Interactable));
        }

        private string HeuristicBinding()
        {
            // TEXT
            Title = Accessor.Text(transform, nameof(Title));
            if (Title != null) return nameof(Title);

            // BUTTON
            _button = GetComponent<Button>();
            if (_button != null)
            {
                Interactable = Accessor.Interactable(_button, nameof(Interactable));
                // TODO GetComponentInChildren performance?
                Title = Accessor.Text(GetComponentInChildren<Text>(), nameof(Title)) ??
                        Accessor.Text(GetComponentInChildren<TMP_Text>(), nameof(Title));

                // TODO image button
                return nameof(Button);
            }

            // INPUT
            {
                var inputField = GetComponent<InputField>();
                if (inputField != null)
                {
                    ValueString = Accessor.Input(inputField, nameof(ValueString));
                    Interactable = Accessor.Interactable(inputField, nameof(Interactable));
                    Title = Accessor.Text(inputField.placeholder as Text, nameof(Title));
                    return nameof(Input);
                }
            }
            {
                var tmp_InputField = GetComponent<TMP_InputField>();
                if (tmp_InputField != null)
                {
                    ValueString = Accessor.Input(tmp_InputField, nameof(ValueString));
                    Interactable = Accessor.Interactable(tmp_InputField, nameof(Interactable));
                    Title = Accessor.Text(tmp_InputField.placeholder as TMP_Text, nameof(Title)); // TODO TEST with Text
                    return nameof(Input);
                }
            }

            // DROPDOWN
            ValueInt = Accessor.Dropdown(transform, nameof(ValueInt));
            if (ValueInt != null)
            {
                Interactable = Accessor.Interactable(GetComponent<Selectable>(), nameof(Interactable));
                return nameof(Dropdown);
            }


            // SLIDER
            ValueFloat = Accessor.From(GetComponent<Slider>(), nameof(ValueFloat));
            if (ValueFloat != null)
            {
                Interactable = Accessor.Interactable(GetComponent<Selectable>(), nameof(Interactable));
                return nameof(Slider);
            }


            // TOGGLE
            ValueBool = Accessor.From(GetComponent<Toggle>(), nameof(ValueBool));
            if (ValueBool != null)
            {
                Interactable = Accessor.Interactable(GetComponent<Selectable>(), nameof(Interactable));
                Title = Accessor.Text(transform.Find("Label"), nameof(Title));
                return nameof(Toggle);
            }

            // IMAGE
            Background = Accessor.From(GetComponent<Image>(), nameof(Background));
            if (Background != null) return nameof(Background);

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

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(name);
            sb.AppendLine(Title.ToString());
            sb.AppendLine(Description.ToString());
            sb.AppendLine(Background.ToString());
            sb.AppendLine(Icon.ToString());
            sb.AppendLine(ValueString.ToString());
            sb.AppendLine(ValueBool.ToString());
            sb.AppendLine(ValueInt.ToString());
            sb.AppendLine(ValueFloat.ToString());
            sb.AppendLine(Interactable.ToString());

            return sb.ToString();
        }
    }
}