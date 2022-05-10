using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MetaUI
{
    public abstract class Accessor
    {
        public static Binder CurrentBinder;

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
            return Dropdown(transform.GetComponent<Dropdown>(), name) ??
                   Dropdown(transform.GetComponent<TMP_Dropdown>(), name);
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
            return Input(transform.GetComponent<InputField>(), name) ??
                   Input(transform.GetComponent<TMP_InputField>(), name);
        }
    }
}