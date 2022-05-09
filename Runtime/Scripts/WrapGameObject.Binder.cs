#region using

using System;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace MetaUI
{
    // TODO: Wrap multiple gameObject
    public partial class WrapGameObject : IBinder
    {
        private IBinder _binder; 
        public IBinder Binder
        {
            get
            {
                if (_binder == null)
                {
                    _binder = gameObject.GetComponent<Binder>();
                    _binder = gameObject.AddComponent<Binder>();
                }

                return _binder;
            }
        }

        public string Kind => Binder.Kind;
        
        public bool? GetInteractable()
        {
            return Binder.GetInteractable();
        }

        public void SetInteractable(bool value)
        {
            Binder.SetInteractable(value);
        }

        public void SetInteractable(Func<bool> provider)
        {
            Binder.SetInteractable(provider);
        }

        public void AddClickedListener(UnityAction action)
        {
            Binder.AddClickedListener(action);
        }

        public string GetTitle()
        {
            return Binder.GetTitle();
        }

        public void SetTitle(string value)
        {
            Binder.SetTitle(value);
        }

        public void SetTitle(Func<string> provider)
        {
            Binder.SetTitle(provider);
        }

        public Sprite GetImage()
        {
            return Binder.GetImage();
        }

        public void SetImage(Sprite value)
        {
            Binder.SetImage(value);
        }

        public void SetImage(Func<Sprite> provider)
        {
            Binder.SetImage(provider);
        }

        public string GetString()
        {
            return Binder.GetString();
        }

        public void SetString(string value)
        {
            Binder.SetString(value);
        }

        public void SetString(Func<string> provider)
        {
            Binder.SetString(provider);
        }

        public void AddStringListener(UnityAction<string> handler)
        {
            Binder.AddStringListener(handler);
        }

        public int? GetInt()
        {
            return Binder.GetInt();
        }

        public void SetInt(int value)
        {
            Binder.SetInt(value);
        }

        public void SetInt(Func<int> provider)
        {
            Binder.SetInt(provider);
        }

        public void AddIntListener(UnityAction<int> handler)
        {
            Binder.AddIntListener(handler);
        }

        public float? GetFloat()
        {
            return Binder.GetFloat();
        }

        public void SetFloat(float value)
        {
            Binder.SetFloat(value);
        }

        public void SetFloat(Func<float> provider)
        {
            Binder.SetFloat(provider);
        }

        public void AddFloatListener(UnityAction<float> handler)
        {
            Binder.AddFloatListener(handler);
        }

        public bool? GetBool()
        {
            return Binder.GetBool();
        }

        public void SetBool(bool value)
        {
            Binder.SetBool(value);
        }

        public void SetBool(Func<bool> provider)
        {
            Binder.SetBool(provider);
        }

        public void AddBoolListener(UnityAction<bool> handler)
        {
            Binder.AddBoolListener(handler);
        }
    }
}