using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace MetaUI.Tests.EditMode.Learn_Unity
{
    [Serializable]
    public class MyClass : Object
    {
        public void AddFooToMyEvent()
        {
            MyEvent = new UnityEvent();
            MyEvent.AddListener(Foo);
        }

        public void Foo()
        {
            Debug.Log("Foooooo!");
        }

        public UnityEvent MyEvent;
    }

    [Serializable]
    public class O : MonoBehaviour
    {
        [SerializeField] public string publicSerializeField = "damphat";
        public string publicField = "damphat";

        [SerializeField] private string privateSerializeField = "damphat";
        private string privateField = "damphat";

        public List<int> publicListInt1;
        public IList<int> publicIListInt2;

        public MyClass publicMyClass;

        public UnityEvent<int> clicked1;
        public UnityEvent<int> clicked2;
    }

    public class SerializationTests
    {
        private static string ConEm(SerializedProperty p, bool hasEm)
        {
            p = p.Copy();
            try
            {
                //var n = p.CountInProperty();

                // return $"{n}";

                var con = p.Copy();
                con.NextVisible(true);
                var em = p.Copy();
                if (hasEm) em.NextVisible(false);
                return $"  (-- {con.name} -- {em.name})";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private static StringBuilder Stringify(SerializedProperty p, bool hasEm = false, int indent = 0,
            StringBuilder sb = null)
        {
            if (sb == null) sb = new StringBuilder();
            for (var i = 0; i < indent; i++) sb.Append("|    ");

            sb.AppendLine($"{p.name}: {p.type}   {p.hasVisibleChildren}  {ConEm(p, hasEm)}");
            //Debug.Log($">>{p.name}");

            if (p.hasVisibleChildren)
            {
                var con = p.Copy();
                var hasCon = con.NextVisible(true);

                var em = p.Copy();
                if (hasEm) em.NextVisible(false);

                //Debug.Log($"  con=={con.name}   em=={em.name}");


                while (true)
                {
                    if (!hasCon) break;
                    if (hasEm && SerializedProperty.EqualContents(con, em)) break;

                    Stringify(con, true, indent + 1, sb);

                    hasCon = con.NextVisible(false);
                }
            }

            return sb;
        }

        [Test]
        public void Test1()
        {
            var go = new GameObject("THE GO");
            var oc = go.AddComponent<O>();
            var sp = new SerializedObject(oc).GetIterator();

            Debug.Log(Stringify(sp));
        }
    }
}