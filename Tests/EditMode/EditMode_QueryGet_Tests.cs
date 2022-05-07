#region using

using System.Linq;
using MetaUI;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;

#endregion

public class EditMode_QueryGet_Tests
{
    [SetUp]
    public void Setup()
    {
        EditorSceneManager.OpenScene("Assets/MetaUI/Tests/EditMode/Test.unity");
    }

    [TearDown]
    public void TearDown()
    {
    }

    [Test]
    public void GetRootTransforms()
    {
        var vietnam = Query.GetRootTransforms().Cast<Transform>().Where(t => t.name == "vietnam").First();
        Assert.IsNotNull(vietnam);
    }

    [Test]
    public void Get_Shallow_return_transform()
    {
        Assert.AreEqual("vietnam", Query.Get(null, "vietnam").name);
        Assert.AreEqual("saigon", Query.Get(null, "vietnam/saigon").name);
        Assert.AreEqual("thuduc", Query.Get(null, "vietnam/saigon/thuduc").name);
    }

    [Test]
    public void Get_Deep_return_transform()
    {
        Assert.AreEqual("vietnam", Query.Get(null, "**/vietnam").name);
        Assert.AreEqual("saigon", Query.Get(null, "**/saigon").name);
        Assert.AreEqual("thuduc", Query.Get(null, "**/thuduc").name);
        Assert.AreEqual("thuduc", Query.Get(null, "**/vietnam/**/thuduc").name);
        Assert.AreEqual("thuduc", Query.Get(null, "**/saigon/**/thuduc").name);
        Assert.AreEqual("thuduc", Query.Get(null, "**/saigon/thuduc").name);
    }

    [Test]
    public void Get_return_null()
    {
        Assert.IsNull(Query.Get(null, "saigon"));
        Assert.IsNull(Query.Get(null, "vietnam/thuduc"));
        Assert.IsNull(Query.Get(null, "vietnam/vietnam"));
        Assert.IsNull(Query.Get(null, "vietnam/saigon/hanoi"));

        Assert.IsNull(Query.Get(null, "saigon/**"));
        Assert.IsNull(Query.Get(null, "**/vietnam/thuduc"));
        Assert.IsNull(Query.Get(null, "vietnam/**/vietnam"));
        Assert.IsNull(Query.Get(null, "vietnam/saigon/**/hanoi"));
    }

    [Test]
    public void Get_Deep_NameMiddle()
    {
        Assert.AreEqual("middle", Query.Get(null, "**/name/middle").name);
    }

    [Test]
    public void Get_Vietnam_index()
    {
        Assert.AreEqual("hanoi", Query.Get(null, "vietnam/0").name);
        Assert.AreEqual("saigon", Query.Get(null, "vietnam/1").name);
        Assert.AreEqual("thuduc", Query.Get(null, "vietnam/1/0").name);

        Assert.AreEqual("Main Camera", Query.Get(null, "0").name);
        Assert.AreEqual("Directional Light", Query.Get(null, "1").name);
        Assert.IsNull(Query.Get(null, "100"));

        Assert.AreEqual("hanoi", Query.Get(null, "vietnam/**/0").name);
        Assert.AreEqual("saigon", Query.Get(null, "vietnam/**/1").name);
        Assert.IsNull(Query.Get(null, "vietnam/**/2"));
    }

    [Test]
    public void Get_GetChild()
    {
        Assert.AreEqual("vietnam", GameObject.Find("vietnam").name);
        Assert.AreEqual("vietnam", GameObject.Find("/vietnam").name);

        Assert.IsNull(Query.GetChild(null, 100));
        Assert.IsNull(Query.GetChild(GameObject.Find("vietnam").transform, 100));
    }
}