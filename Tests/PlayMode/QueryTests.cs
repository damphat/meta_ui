#region using

using MetaUI;
using NUnit.Framework;
using UnityEngine.SceneManagement;

#endregion

public class QueryTests
{
    [Test]
    public void GetTests()
    {
        SceneManager.LoadScene("Test", LoadSceneMode.Single);
        Assert.AreEqual(null, Query.Get(null, "Canvas"));
    }
}