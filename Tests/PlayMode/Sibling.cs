using NUnit.Framework;
using UnityEngine.SceneManagement;

public class Sibling
{
    [Test]
    public void SiblingIndex()
    {
        var scene = SceneManager.GetActiveScene();

        var objects = scene.GetRootGameObjects();

        for (int i = 0; i < objects.Length; i++)
        {
            Assert.AreEqual(i, objects[i].transform.GetSiblingIndex());
        }

    }
}