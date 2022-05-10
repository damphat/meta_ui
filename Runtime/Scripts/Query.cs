#region using

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#endregion

namespace MetaUI
{
    // TODO: GetAll("x/**/#tag")
    // TODO: interface QueryAccessor { match, text, click, skin}
    // TODO: auto detect and choose accessor: Text | TMP_Text
    public class Query
    {
        private static readonly List<GameObject> RootObjects = new List<GameObject>();

        public static Func<Transform, bool> DotText = transform => transform.GetComponent<Text>() != null;
        public static Func<Transform, bool> DotImage = transform => transform.GetComponent<Image>() != null;
        public static Func<Transform, bool> DotRawImage = transform => transform.GetComponent<RawImage>() != null;
        public static Func<Transform, bool> DotButton = transform => transform.GetComponent<Button>() != null;
        public static Func<Transform, bool> DotToggle = transform => transform.GetComponent<Toggle>() != null;
        public static Func<Transform, bool> DotSlider = transform => transform.GetComponent<Slider>() != null;
        public static Func<Transform, bool> DotScrollbar = transform => transform.GetComponent<Scrollbar>() != null;
        public static Func<Transform, bool> DotDropdown = transform => transform.GetComponent<Dropdown>() != null;
        public static Func<Transform, bool> DotInputField = transform => transform.GetComponent<InputField>() != null;
        public static Func<Transform, bool> DotCanvas = transform => transform.GetComponent<Canvas>() != null;
        public static Func<Transform, bool> DotCamera = transform => transform.GetComponent<Camera>() != null;
        public static Func<Transform, bool> DotLight = transform => transform.GetComponent<Light>() != null;

        public static readonly Dictionary<string, Func<Transform, bool>> MatchFunc =
            new Dictionary<string, Func<Transform, bool>>
            {
                {".text", DotText},
                {".button", DotButton},
                {".input", DotInputField},
                {".canvas", DotCanvas},
                {".camera", DotCamera}
            };


        public static IEnumerable GetRootTransforms()
        {
            SceneManager.GetActiveScene().GetRootGameObjects(RootObjects);

            return RootObjects.Select(go => go.transform);
        }

        public static List<object> Parse(string qs)
        {
            if (qs == null) throw new ArgumentNullException(nameof(qs), "Query string is null");

            var ret = new List<object>();
            var keys = qs.Split('/');
            if (keys.Length >= 2 && keys[0] == "") keys[0] = null;

            foreach (var key in keys)
                if (string.IsNullOrEmpty(key))
                {
                    ret.Add(key);
                }
                else
                {
                    var c = key[0];
                    if (c == '.')
                    {
                        if (MatchFunc.TryGetValue(key, out var func))
                            ret.Add(func);
                        else
                            throw new MetaUIException($"Bad class '{key}' in the query string: '{qs}'");
                    }
                    else if (c >= '0' && c <= '9')
                    {
                        if (int.TryParse(key, out var result))
                            ret.Add(int.Parse(key));
                        else
                            throw new MetaUIException($"Bad number '{key}' in the query string: '{qs}'");
                    }
                    else
                    {
                        ret.Add(key);
                    }
                }

            return ret;
        }

        public static Transform Get(Transform parent, string path)
        {
            var keys = Parse(path);

            if (keys.Count > 1 && keys[0] == null) return Get(null, keys, 1, false);

            return Get(parent, keys, 0, false);
        }

        public static Transform GetChild(Transform parent, int index)
        {
            if (parent == null)
            {
                var scene = SceneManager.GetActiveScene();

                if (index >= scene.rootCount) return null;

                scene.GetRootGameObjects(RootObjects);
                return RootObjects[index].transform;
            }

            if (index >= parent.childCount) return null;

            return parent.GetChild(index);
        }

        public static Transform Get(Transform parent, IReadOnlyList<object> keys, int index, bool deep)
        {
            static bool Match(object key, Transform child)
            {
                return key switch
                {
                    string name => child.name == name,
                    Func<Transform, bool> func => func(child),
                    int i => child.GetSiblingIndex() == i,
                    _ => false
                };
            }

            if (index >= keys.Count) return parent;

            var key = keys[index];

            if (key is int i)
            {
                var child = GetChild(parent, i);

                if (child != null)
                {
                    var ret = Get(child, keys, index + 1, false);
                    if (ret != null) return ret;
                }
                else
                {
                    if (deep)
                        foreach (Transform c in parent ? parent : GetRootTransforms())
                        {
                            var ret = Get(c, keys, index, true);
                            if (ret != null) return ret;
                        }
                }
            }

            if ("**".Equals(key)) return Get(parent, keys, index + 1, true);

            foreach (Transform child in parent ? parent : GetRootTransforms())
                if (Match(key, child))
                {
                    var ret = Get(child, keys, index + 1, false);
                    if (ret != null) return ret;
                }
                else
                {
                    if (deep)
                    {
                        var ret = Get(child, keys, index, true);
                        if (ret != null) return ret;
                    }
                }

            return null;
        }
    }
}