using UnityEngine;

namespace com.damphat.MetaUI
{
    public class UI
    {
        public static WrapGameObject Canvas(string name = null)
        {
            var go = name == null ? Object.FindObjectOfType<Canvas>().gameObject : GameObject.Find(name);

            return new WrapGameObject(go);
        }
    }
}