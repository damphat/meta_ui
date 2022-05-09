using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
// ReSharper disable InconsistentNaming


namespace MetaUI 
{
    public class Binder : MonoBehaviour, IBinder
    {
        public string Kind { get; private set; }
        protected Text _title1;
        protected TMP_Text _title2;
        protected Func<string> _titleProvider;

        protected Image _image;
        protected Func<Sprite> _imageProvider;

        protected InputField _string1;
        protected TMP_InputField _string2;
        protected Func<string> _stringProvider;

        protected Dropdown _int1;
        protected TMP_Dropdown _int2;
        protected Func<int> _intProvider;

        protected Slider _float;
        protected Func<float> _floatProvider;

        protected Toggle _bool;
        protected Func<bool> _boolProvider;

        protected Selectable _interactable;
        protected Func<bool> _interactableProvider;

        protected Button _button;

        #region Interactable

        public virtual bool? GetInteractable()
        {
            if (_interactable) return _interactable.interactable;
            return null;
        }

        public virtual void SetInteractable(bool value)
        {
            if (_interactable) _interactable.interactable = value;
        }

        public virtual void SetInteractable(Func<bool> provider)
        {
            _interactableProvider = provider;

            SetInteractable(provider());
        }

        #endregion


        #region Clicked

        public virtual void AddClickedListener(UnityAction action)
        {
            if (_button)
            {
                _button.onClick.AddListener(action);
            }
        }

        #endregion


        #region Title
        public virtual string GetTitle()
        {
            if (_title1) return _title1.text;
            if (_title2) return _title2.text;
            return null;
        }

        public virtual void SetTitle(string value)
        {
            if (_title1) _title1.text = value;
            if (_title2) _title2.text = value;
        }

        public virtual void SetTitle(Func<string> provider)
        {
            _titleProvider = provider;

            SetTitle(provider());
        }

        #endregion

        #region Image
        public virtual Sprite GetImage()
        {
            if (_image) return _image.sprite;
            return null;
        }

        public virtual void SetImage(Sprite value)
        {
            if (_image) _image.sprite = value;
        }

        public virtual void SetImage(Func<Sprite> provider)
        {
            _imageProvider = provider;

            SetImage(provider());
        }

        #endregion


        #region String

        public virtual string GetString()
        {
            if (_string1) return _string1.text;
            if (_string2) return _string2.text;
            return null;
        }

        public virtual void SetString(string value)
        {
            if (_string1) _string1.text = value;
            if (_string2) _string2.text = value;
        }

        public virtual void SetString(Func<string> provider)
        {
            _stringProvider = provider;
            SetString(provider());
        }

        public virtual void AddStringListener(UnityAction<string> handler)
        {
            if (_string1) _string1.onValueChanged.AddListener(handler);
            if (_string2) _string2.onValueChanged.AddListener(handler);
        }

        #endregion

        #region Int32

        public virtual int? GetInt()
        {
            if (_int1) return _int1.value;
            if (_int2) return _int2.value;
            return null;
        }

        public virtual void SetInt(int value)
        {
            if (_int1) _int1.value = value;
            if (_int2) _int2.value = value;
        }
        public virtual void SetInt(Func<int> provider)
        {
            _intProvider = provider;
            SetInt(provider());
        }

        public virtual void AddIntListener(UnityAction<int> handler)
        {
            if (_int1) _int1.onValueChanged.AddListener(handler);
            if (_int2) _int2.onValueChanged.AddListener(handler);
        }

        #endregion

        #region Float

        public virtual float? GetFloat()
        {
            if(_float) return _float.value;
            return null;
        }

        public virtual void SetFloat(float value)
        {
            if (_float) _float.value = value;
        }
        public virtual void SetFloat(Func<float> provider)
        {
            _floatProvider = provider;
            SetFloat(provider());
        }

        public virtual void AddFloatListener(UnityAction<float> handler)
        {
            if(_float) _float.onValueChanged.AddListener(handler);
        }

        #endregion

        #region Bool

        public virtual bool? GetBool()
        {
            if (_bool) return _bool.isOn;
            return null;
        }

        public virtual void SetBool(bool value)
        {
            if(_bool) _bool.isOn = value;
        }
        public virtual void SetBool(Func<bool> provider)
        {
            _boolProvider = provider;
            SetBool(provider());
        }

        public virtual void AddBoolListener(UnityAction<bool> handler)
        {
            if(_bool) _bool.onValueChanged.AddListener(handler);
        }
        #endregion

        private void Awake()
        {
            Kind = Load();
        }

        public string Load()
        {
            // TEXT
            _title1 = this.GetComponent<Text>();

            if(_title1 != null ) return "Text[]";
    
            _title2 = this.GetComponent<TMP_Text>();
            if (_title2 != null) return "TMP_Text";

            // BUTTON
            _button = GetComponent<Button>();
            if (_button != null)
            {
                _interactable = _button;

                _image = GetComponentInChildren<Image>();
                var icon = _image == null ? "" : "icon";
                _title1 = GetComponentInChildren<Text>();
                if (_title1 != null)
                {
                    return $"Button[interact,{icon},title1]";
                }

                _title2 = GetComponentInChildren<TMP_Text>();
                if (_title2 != null)
                {
                    return $"Button[interact,{icon},title2]";
                }

                return $"Button[{icon}]";
            }

            // INPUT
            _string1 = GetComponent<InputField>();
            if (_string1 != null)
            {
                _interactable = _string1;
                _title1 = _string1.placeholder as Text;
                return "InputField[title]";
            }

            _string2 = GetComponent<TMP_InputField>();
            if (_string2 != null)
            {
                _interactable = _string2;
                _title2 = _string2.placeholder as TMP_Text;
                return "TMP_InputField[interact,title]";
            }

            // DROPDOWN
            _int1 = GetComponent<Dropdown>();
            if (_int1 != null)
            {
                _interactable = _int1;
                return "Dropdown[interact,int]";
            }

            _int2 = GetComponent<TMP_Dropdown>();
            if (_int2 != null)
            {
                _interactable = _int2;
                return "TMP_Dropdown[interact,int]";
            }

            // SLIDER
            _float = GetComponent<Slider>();
            if (_float != null)
            {
                _interactable = _float;
                return "Slider[interact,float]";
            }

            // TOGGLE
            _bool = GetComponent<Toggle>();
            if (_bool != null)
            {
                _interactable = _bool;
                _title1 =  GetComponentInChildren<Text>();
                if(_title1 == null) _title2 = GetComponentInChildren<TMP_Text>();
                return "Toggle[interact,bool,title]";
            }
            
            // IMAGE
            _image = GetComponent<Image>();
            if (_image != null)
            {
                return "Image[sprite]";
            }

            return null;
        }

        public virtual void Update()
        {

            if (_titleProvider != null)
            {
                var value = _titleProvider();
                if(_title1) _title1.text = value;
                if (_title2) _title2.text = value;
            }

            if (_imageProvider != null)
            {
                var value = _imageProvider();
                if (_image) _image.sprite = value;
            }

            if (_stringProvider != null)
            {
                var value = _stringProvider();
                if(_string1) _string1.text = value;
                if (_string2) _string2.text = value;
            }

            if (_intProvider != null)
            {
                var value = _intProvider();
                if (_int1) _int1.value = value;
                if (_int2) _int2.value = value;
            }
            ;

            if (_floatProvider != null)
            {
                var value = _floatProvider();
                if(_float) _float.value = value;
            }
            ;

            if (_boolProvider != null)
            {
                var value = _boolProvider();
                if(_bool) _bool.isOn = value;
            }
            ;

            if (_interactableProvider != null)
            {
                var value = _interactableProvider();
                if (_interactable) _interactable.interactable = value;
            }
            
        }
    }

}
