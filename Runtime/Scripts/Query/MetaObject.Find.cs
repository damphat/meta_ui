using UnityEngine;

namespace MetaUI
{
    partial class MetaObject
    {
        public MetaObject From(Component c)
        {
            return new MetaObject(c.gameObject);
        }

        public MetaObject Get(string qs)
        {
            var go = Query.Get(gameObject.transform, qs);
            if (go == null)
            {
                var error = $"Can not Get('{qs}') from {Path}";
                Toast.Error(error, 10);
                throw new MetaException(error);
            }

            return new MetaObject(go.gameObject);
        }
    }
}