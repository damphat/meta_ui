using MetaUI;
using NUnit.Framework;
using UnityEngine.Events;

public class EditMode_Accessor_Tests
{
    private int _count;
    public Accessor<int> Count { get; private set; }

    [SetUp]
    public void SetUp()
    {
        var ev = new UnityEvent<int>();


        _count = 1;

        Count = new Accessor<int>(() => _count, v => _count = v, null, null, "Count");
    }

    [Test]
    public void GetSet()
    {
        Assert.AreEqual(_count, Count.Get());

        Count.Set(10);
        Assert.AreEqual(10, _count);
        Assert.AreEqual(10, Count.Get());
    }

    [Test]
    public void DebugInfo()
    {
        Assert.AreEqual("Count", Count.Name);
        var debug = Count.ToString();
        Assert.That(debug, Does.Match("Count=1"));
    }

    [Test]
    public void Null()
    {
        var parent = Accessor<string>.Null("Parent");
        Assert.AreEqual("Parent", parent.Name);
        var debug = parent.ToString();
        Assert.That(debug, Does.Contain("Parent=null"));

        parent.Set(() => "xxx");
        parent.Set("xxx");
        parent.Get();
        parent.Get(v => { });
        parent.Update();

        Assert.AreEqual(default, parent.Get());
    }

    [Test]
    public void Get_only()
    {
    }

    [Test]
    public void Set_provider()
    {
        Assert.AreEqual("Count", Count.Name);
        Assert.AreEqual(_count, Count.Get());

        Count.Set(10);
        Assert.AreEqual(10, _count);
        Assert.AreEqual(10, Count.Get());
    }
}