using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.Events;

public class EventTests
{
    private readonly List<string> foo = new List<string>();

    private void Foo(string a)
    {
        foo.Add($"Foo({a})");
    }

    private void Bar(string a)
    {
        foo.Add($"Bar({a})");
    }

    [Test]
    public void Test1()
    {
        var f1 = new UnityAction<string>(Foo);
        var f2 = new UnityAction<string>(Foo);
        var b = new UnityAction<string>(Bar);


        Assert.AreEqual(f1, f2);
        Assert.AreNotEqual(f2, b);
    }

    [Test]
    public void Test2()
    {
        var e = new UnityEvent<string>();
        e.AddListener(Foo);
        e.AddListener(Foo);
        e.Invoke("1");

        Assert.AreEqual(new[] {"Foo(1)", "Foo(1)"}, foo);

        foo.Clear();

        e.RemoveListener(Foo);
        e.Invoke("2");
        Assert.AreEqual(Array.Empty<string>(), foo);
    }
}