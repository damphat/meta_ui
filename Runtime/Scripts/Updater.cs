#region using

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#endregion

namespace MetaUI
{
    [DisallowMultipleComponent]
    internal class Updater : MonoBehaviour
    {
        private Text text;
        private Button button;
        private InputField inputField;
        private Toggle toggle;
        private ToggleGroup toggleGroup;
        private Image image;
        private Slider slider;


        private readonly Dictionary<string, UnityAction> _updateDictionary = new Dictionary<string, UnityAction>();

        private void Update()
        {
            foreach (var action in _updateDictionary.Values) action?.Invoke();
        }

        public void Set(string key, UnityAction action)
        {
            _updateDictionary[key] = action;
        }

        public bool? Interactable()
        {
            return null;
        }

        public bool? Checked()
        {
            return null;
        }

        public string Caption()
        {
            return null;
        }

        public bool? BoolValue()
        {
            return null;
        }

        public int? IntValue()
        {
            return null;
        }

        public float? FloatValue()
        {
            return null;
        }


        public string StringValue()
        {
            return null;
        }
    }
}