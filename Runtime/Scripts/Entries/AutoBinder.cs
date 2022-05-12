using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MetaUI.Generic
{

    [Serializable]
    public class AutoBinder: MonoBehaviour
    {
        public UDictionary<string, Entry> dict = new UDictionary<string, Entry>();

        public Entry<T> AddEntry<T>(string name, Func<T> getter, Action<T> setter, UnityEvent<T> changed)
        {
            if (dict.ContainsKey(name))
            {
                throw new Exception($"{name} already exists");
            }

            var entry = new BindEntry<T>(name, getter, setter, changed);
            dict.Add(name, entry);
            return entry;
        }

        public Entry<string> AddEntry(string name, Text c)
        {
            if(c == null) return null;
            return AddEntry(name, () => c.text, value => c.text = value, null);
        }

        public Entry<string> AddEntry(string name, TMP_Text c)
        {
            if (c == null) return null;
            return AddEntry(name, () => c.text, value => c.text = value, null);
        }

        public Entry<string> AddEntry(string name, InputField c)
        {
            if (c == null) return null;
            return AddEntry(name, () => c.text, value => c.text = value, c.onValueChanged);
        }
        public Entry<string> AddEntry(string name, TMP_InputField c)
        {
            if (c == null) return null;
            return AddEntry(name, () => c.text, value => c.text = value, c.onValueChanged);
        }

        public Entry<bool> AddEntry(string name, Toggle c)
        {
            if (c == null) return null;
            return AddEntry(name, () => c.isOn, value => c.isOn = value, c.onValueChanged);
        }
        public Entry<bool> AddEntry(string name, Selectable c)
        {
            if (c == null) return null;
            return AddEntry(name, () => c.interactable, value => c.interactable = value, null);
        }
        public Entry<int> AddEntry(string name, Dropdown c)
        {
            if (c == null) return null;
            return AddEntry(name, () => c.value, value => c.value = value, c.onValueChanged);
        }

        public Entry<int> AddEntry(string name, TMP_Dropdown c)
        {
            if (c == null) return null;
            return AddEntry(name, () => c.value, value => c.value = value, c.onValueChanged);
        }

        public Entry<float> AddEntry(string name, Slider c)
        {
            if (c == null) return null;
            return AddEntry(name, () => c.value, value => c.value = value, c.onValueChanged);
        }

        public Entry<float> AddEntry(string name, Scrollbar c)
        {
            if (c == null) return null;
            return AddEntry(name, () => c.value, value => c.value = value, c.onValueChanged);
        }

        
        public Entry<Sprite> AddEntry(string name, Image c)
        {
            if (c == null) return null;
            return AddEntry(name, () => c.sprite, value => c.sprite = value, null);
        }


        public Entry<T> GetEntry<T>(string name)
        {
            if (dict.TryGetValue(name, out var entry))
            {
                return (Entry<T>) entry;
            }

            return null;
        }

        public Entry<string> Title { get; private set; }
        public Entry<string> String { get; private set; }
        public Entry<bool> Bool { get; private set; }
        public Entry<int> Int { get; private set; }
        public Entry<float> Float { get; private set; }
        public Entry<bool> Interactable { get; private set; }
        public Entry<Sprite> Background { get; private set; }


        public UnityEvent Clicked;

        public string Detect()
        {
            // Text TMP_text
            {
                Title = AddEntry("Title", GetComponent<Text>()) ?? AddEntry("Title", GetComponent<TMP_Text>());
                if (Title != null)
                {
                    return "Text";
                }
            }

            // Button
            {
                var button = GetComponent<Button>();
                if (button != null)
                {
                    Clicked = button.onClick;
                    Interactable = AddEntry("Interactable", (Selectable)button);
                    Title = AddEntry("Title", GetComponentInChildren<Text>()) ?? AddEntry("Title", GetComponentInChildren<TMP_Text>());
                    return "Button";
                }
            }

            // Toggle
            {
                var toggle = GetComponent<Toggle>();
                if (toggle != null)
                {
                    Interactable = AddEntry("Interactable", (Selectable)toggle);
                    Bool = AddEntry("Bool", toggle);
                    Title = AddEntry("Title", GetComponentInChildren<Text>()) ??
                           AddEntry("Title", GetComponentInChildren<TMP_Text>());

                    return "Toggle";
                }
            }

            // Slider
            {
                var slider = GetComponent<Slider>();
                if (slider != null)
                {
                    Interactable = AddEntry("Interactable", (Selectable) slider);
                    Float = AddEntry("Float", slider);
                    return "Slider";
                }
            }

            // Scrollbar
            {
                var scrollbar = GetComponent<Scrollbar>();
                if (scrollbar != null)
                {
                    Interactable = AddEntry("Interactable", (Selectable) scrollbar);
                    Float = AddEntry("Float", scrollbar);
                    return "Scrollbar";
                }
            }

            // Dropdown 
            {
                var dropdown = GetComponent<Dropdown>();
                if (dropdown != null)
                {
                    Interactable = AddEntry("Interactable", (Selectable) dropdown);
                    Int = AddEntry("Int", dropdown);
                    return "Dropdown";
                }
            }

            // TMP_Dropdown
            {
                var dropdown = GetComponent<TMP_Dropdown>();
                if (dropdown != null)
                {
                    Interactable = AddEntry("Interactable", (Selectable)dropdown);
                    Int = AddEntry("Int", dropdown);
                    return "Dropdown";
                }
            }
        
            // InputField 
            {
                var c = GetComponent<InputField>();
                if (c != null)
                {
                    Interactable = AddEntry("Interactable", (Selectable)c);
                    String = AddEntry("String", c);
                    Title = AddEntry("Title", c.placeholder as Text);
                    return "InputField";
                }

            }

            // TMP_InputField
            {
                var c = GetComponent<TMP_InputField>();
                if (c != null)
                {
                    Interactable = AddEntry("Interactable", (Selectable)c);
                    String = AddEntry("String", c);
                    Title = AddEntry("Title", c.placeholder as TMP_Text);
                    return "InputField";
                }
            }

            // Image
            {
                var c = GetComponent<Image>();
                if (c != null)
                {
                    Background = AddEntry("Background", c);
                    return "Image";
                }

            }

            return "Unknown";

        }

        private void Awake()
        {
            Detect();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(name);
            foreach (var e in dict)
            {
                sb.Append(e.Value).Append(',');
            }
            return sb.ToString();
        }
    }
}