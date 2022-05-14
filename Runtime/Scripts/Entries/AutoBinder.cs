using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MetaUI.Generic
{
    [DisallowMultipleComponent]
    public class AutoBinder : MonoBehaviour
    {
        private Dictionary<string, Entry> _dict;

        public Dictionary<string, Entry> dict
        {
            get
            {
                Detect();
                return _dict;
            }
        }

        public Entry<T> AddEntry<T>(string name, Func<T> getter, Action<T> setter, UnityEvent<T> changed)
        {
            if (dict.ContainsKey(name)) throw new Exception($"{name} already exists");

            var entry = new BindEntry<T>(name, getter, setter, changed);
            dict.Add(name, entry);
            return entry;
        }

        public Entry<string> AddEntry(string name, Text c)
        {
            if (c == null) return null;
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
            if (dict.TryGetValue(name, out var entry)) return (Entry<T>) entry;

            return Entry<T>.Null;
        }

        public Entry<string> Title => GetEntry<string>("Title");
        public Entry<string> String => GetEntry<string>("String");
        public Entry<bool> Bool => GetEntry<bool>("Bool");
        public Entry<int> Int => GetEntry<int>("Int");
        public Entry<float> Float => GetEntry<float>("Float");
        public Entry<bool> Interactable => GetEntry<bool>("Interactable");
        public Entry<Sprite> Background => GetEntry<Sprite>("Background");

        private UnityEvent clicked;

        public UnityEvent Clicked
        {
            get
            {
                Detect();
                return clicked ?? new UnityEvent();  
            }
        }

        public string Detect()
        {
            if (_dict != null) return null;
            
            _dict = new Dictionary<string, Entry>();
            
            // Text TMP_text
            {
                var Title = AddEntry("Title", GetComponent<Text>()) ?? AddEntry("Title", GetComponent<TMP_Text>());
                if (Title != null) return "Text";
            }

            // Button
            {
                var button = GetComponent<Button>();
                if (button != null)
                {
                    clicked = button.onClick;
                    var Interactable = AddEntry("Interactable", button);
                    var Title = AddEntry("Title", GetComponentInChildren<Text>()) ??
                                AddEntry("Title", GetComponentInChildren<TMP_Text>());
                    return "Button";
                }
            }

            // Toggle
            {
                var toggle = GetComponent<Toggle>();
                if (toggle != null)
                {
                    var Interactable = AddEntry("Interactable", (Selectable) toggle);
                    var Bool = AddEntry("Bool", toggle);
                    var Title = AddEntry("Title", GetComponentInChildren<Text>()) ??
                                AddEntry("Title", GetComponentInChildren<TMP_Text>());

                    return "Toggle";
                }
            }

            // Slider
            {
                var slider = GetComponent<Slider>();
                if (slider != null)
                {
                    var Interactable = AddEntry("Interactable", (Selectable) slider);
                    var Float = AddEntry("Float", slider);
                    return "Slider";
                }
            }

            // Scrollbar
            {
                var scrollbar = GetComponent<Scrollbar>();
                if (scrollbar != null)
                {
                    var Interactable = AddEntry("Interactable", (Selectable) scrollbar);
                    var Float = AddEntry("Float", scrollbar);
                    return "Scrollbar";
                }
            }

            // Dropdown 
            {
                var dropdown = GetComponent<Dropdown>();
                if (dropdown != null)
                {
                    var Interactable = AddEntry("Interactable", (Selectable) dropdown);
                    var Int = AddEntry("Int", dropdown);
                    return "Dropdown";
                }
            }

            // TMP_Dropdown
            {
                var dropdown = GetComponent<TMP_Dropdown>();
                if (dropdown != null)
                {
                    var Interactable = AddEntry("Interactable", (Selectable) dropdown);
                    var Int = AddEntry("Int", dropdown);
                    return "Dropdown";
                }
            }

            // InputField 
            {
                var c = GetComponent<InputField>();
                if (c != null)
                {
                    var Interactable = AddEntry("Interactable", (Selectable) c);
                    var String = AddEntry("String", c);
                    var Title = AddEntry("Title", c.placeholder as Text);
                    return "InputField";
                }
            }

            // TMP_InputField
            {
                var c = GetComponent<TMP_InputField>();
                if (c != null)
                {
                    var Interactable = AddEntry("Interactable", (Selectable) c);
                    var String = AddEntry("String", c);
                    var Title = AddEntry("Title", c.placeholder as TMP_Text);
                    return "InputField";
                }
            }

            // Image
            {
                var c = GetComponent<Image>();
                if (c != null)
                {
                    var Background = AddEntry("Background", c);
                    return "Image";
                }
            }

            return "Unknown";
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(name);
            sb.AppendLine(": {");
            foreach (var e in dict)
            {
                sb.Append(' ', 8);
                sb.Append(e.Key).Append(": ").Append(e.Value.Get());
                sb.AppendLine();
            }

            sb.Append('}');
            return sb.ToString();
        }
    }
}