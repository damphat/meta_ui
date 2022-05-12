using System;
using System.Collections.Generic;
using System.Text;
using MetaUI.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ParticleSystemJobs;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace MetaUI.Tests.EditMode.Learn_Unity
{
    [Serializable]
    public class O : MonoBehaviour
    {
        [SerializeField]
        public string username = "damphat";
        public UnityEvent<int> clicked1;
        public UnityEvent<int> clicked2;
    }
    public class SerializationTests
    {
        static StringBuilder Stringify1(SerializedProperty p, int indent = 0, StringBuilder sb = null)
        {
            if (sb == null) sb = new StringBuilder();
            for (int i = 0; i < indent; i++)
            {
                sb.Append("|    ");
            }

            
            sb.AppendLine($"{p.name}: {p.type}");

            if (p.hasChildren)
            {
                var end = p.GetEndProperty(true);

                var c = p.Copy();
                var hasNext = c.NextVisible(true);
                while (hasNext && !SerializedProperty.EqualContents(c, end))
                {
                    Stringify1(c, indent + 1, sb);
                    hasNext = c.NextVisible(false);
                }
            }

            return sb;

        }

        [Test]
        public void Test1()
        {
            var go = new GameObject("THE GO");
            var com = go.AddComponent<O>();

            Debug.Log(Stringify1(new SerializedObject(com).GetIterator()));
        }
    }
}