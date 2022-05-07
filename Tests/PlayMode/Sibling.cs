#region using

using NUnit.Framework;
using UnityEngine.SceneManagement;

#endregion

public class Sibling
{
    [Test]
    public void SiblingIndex()
    {
        var scene = SceneManager.GetActiveScene();

        var objects = scene.GetRootGameObjects();

        for (var i = 0; i < objects.Length; i++)
        {
            Assert.AreEqual(i, objects[i].transform.GetSiblingIndex());
        }
    }
}