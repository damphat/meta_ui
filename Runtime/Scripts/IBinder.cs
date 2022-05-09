using System;
using UnityEngine;
using UnityEngine.Events;

namespace MetaUI
{
    public interface IBinder
    {
        string Kind { get; }  
        #region Interactable

        bool? GetInteractable();
        void SetInteractable(bool value);

        void SetInteractable(Func<bool> provider);

        #endregion

        #region Clicked

        void AddClickedListener(UnityAction action);

        #endregion


        #region Title

        string GetTitle();

        void SetTitle(string value);

        void SetTitle(Func<string> provider);

        #endregion

        #region Image

        Sprite GetImage();

        void SetImage(Sprite value);

        void SetImage(Func<Sprite> provider);

        #endregion


        #region String

        string GetString();

        void SetString(string value);

        void SetString(Func<string> provider);

        void AddStringListener(UnityAction<string> handler);

        #endregion

        #region Int32

        int? GetInt();

        void SetInt(int value);
        void SetInt(Func<int> provider);
        void AddIntListener(UnityAction<int> handler);

        #endregion

        #region Float

        float? GetFloat();

        void SetFloat(float value);
        void SetFloat(Func<float> provider);

        void AddFloatListener(UnityAction<float> handler);

        #endregion

        #region Bool

        bool? GetBool();

        void SetBool(bool value);
        void SetBool(Func<bool> provider);

        void AddBoolListener(UnityAction<bool> handler);

        #endregion
    }
}