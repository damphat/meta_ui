using UnityEngine;

namespace MetaUI
{
    partial class WrapGameObject
    {
        public WrapGameObject From(Component c)
        {
            return new WrapGameObject(c.gameObject);
        }
        public WrapGameObject Get(string qs)
        {
            var go = Query.Get(gameObject.transform, qs);
            if (go == null)
            {
                var error = $"Can not Get('{qs}') from {Path}";
                Toast.Error(error, 10);
                throw new MetaUIException(error);
            }

            return new WrapGameObject(go.gameObject);
        }
    }
}