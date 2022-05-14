using System.Collections.Generic;
using System.Linq;
using MetaUI.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MetaUI.Tests.EditMode
{
    public class EntryTests
    {
        private Entry<int> Count;

        [SetUp]
        public void Setup()
        {
            Count = new ValueEntry<int>("Count", 1);
        }

        [Test]
        public void SetGet()
        {
            Assert.AreEqual(1, Count.Get());
            Count.Set(2);
            Assert.AreEqual(2, Count.Get());
        }

        [Test]
        public void AddRemove()
        {
            var list = new List<object>();

            void Handle(int value)
            {
                list.Add(value);
            }

            Count.Add(Handle);
            Assert.AreEqual(0, list.Count);

            Count.Set(1);
            Assert.AreEqual(0, list.Count);

            Count.Set(2);
            Assert.AreEqual(new[] {2}, list);

            Count.Set(2);
            Assert.AreEqual(new[] {2}, list);

            Count.Remove(Handle);

            Count.Set(3);
            Assert.AreEqual(new object[] {2}, list);
        }

        [Test]
        public void Src()
        {
            var count2 = new ValueEntry<int>("Count2", -1000);

            count2.SetSrc(Count);

            Assert.AreEqual(Count, count2.Src);
            Assert.AreEqual(1, count2.Get());

            Count.Set(1000);
            Assert.AreEqual(1000, count2.Get());
        }

        [Test]
        public void Entry()
        {
            var entry = Count as Entry;

            entry.Set(1000);

            Assert.AreEqual(1000, Count.Get());
            Assert.AreEqual(1000, entry.Get());
        }

        [Test]
        public void DictTests()
        {
            // EditorSceneManager.OpenScene("Assets/MetaUI/Samples/Binding2/Binding2");
            EditorSceneManager.OpenScene("Assets/MetaUI/Samples/Binding2/Binding2.unity");
            var scene = SceneManager.GetActiveScene();

            var canvas = scene.GetRootGameObjects().FirstOrDefault(go => go.name == "Canvas");


            var objects = canvas.transform.Find("objects");

            var output = new Dictionary<string, Dictionary<string, Entry>>();

            foreach (Transform o in objects)
            {
                var com = o.gameObject.AddComponent<AutoBinder>();
                com.Detect();

                output[o.name] = com.dict;
            }

            Debug.Log(output);
        }
    }
}