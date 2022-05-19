
using System.Collections.Generic;
using MetaUI.Sigos;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Diagnostics;
using Utils = MetaUI.Sigos.Utils;

public class SigoTests 
{
    [Test]
    public void Test1()
    {
        var s = Sigo.Create(
            "name", Sigo.Create(
                "first", "Phat", 
                "last", "Dam"), 
            "friends", "An");
        
        Debug.Log(s.ToString());
        
        Assert.AreEqual("An", s.Get("friends"));
        Assert.AreEqual("Phat", s.Get("name/first"));
        Assert.AreEqual("Dam", s.Get("name/last"));
        Assert.AreEqual(s.Get("name/first"), s.Get("name/first"));
        
        s.Freeze();

        s = s.Set("name/first", "Fat");
        s = s.Set("name/middle", "Ngoc");
        s = s.Set("friends/0", "An");
        s = s.Set("friends/1", "Bao");
        
        Debug.Log(s.ToString());
        
    }

    [Test]
    public void AtTest()
    {
        var store = new Store();
        var a = store.At("name");
        var b = store.At("name");
        
        Assert.AreSame(a, b);
        
        a.Set(1);
        Assert.AreEqual(1, b.Get());
        Assert.AreEqual(1, b.Get());
        
        Assert.AreEqual(a.Get(), b.Get());
    }
    
    [Test]
    public void AddListenerTests_root()
    {
        var store = new Store();
        var ret = new List<object>();
        store.AddListener(v =>
        {
            ret.Add(v);
        });
        
        store.Set("1");
        store.Update();
        store.Update();
        Assert.AreEqual(new [] {"1"}, ret);
        store.Set("1");
        store.Update();
        store.Update();
        Assert.AreEqual(new [] {"1"}, ret);
        store.Set("2");
        store.Update();
        store.Update();
        Assert.AreEqual(new [] {"1", "2"}, ret);
    }
    
    [Test]
    public void AddListenerTests_name()
    {
        var store = new Store();
        var ret = new List<object>();
        store.AddListener(v =>
        {
            ret.Add(v);
        });

        var name = store.At("name");
        
        name.Set("Phat");
        store.Update();
        store.Update();
        Assert.AreEqual(new [] { Sigo.Create("name", "Phat") }, ret);
    }
    
    [Test]
    public void AddListenerTests_name_age()
    {
        var store = new Store();
        var ret = new List<object>();
        store.AddListener(v =>
        {
            ret.Add(v);
        });

        store.At("name").Set("phat");
        store.At("age").Set("40");
        store.Update();
        store.Update();
        Assert.AreEqual(new [] { Sigo.Create("name", "phat", "age", "40") }, ret);
        store.At("age").Set("41");
        store.Update();
        Assert.AreEqual(new [] { Sigo.Create("name", "phat", "age", "40"), Sigo.Create("name", "phat", "age", "41") }, ret);
        
    }
    
    [Test]
    public void AddListenerTests_name_first_last_age()
    {
        var store = new Store();
        var first = new List<object>();
        var last = new List<object>();
        var name = new List<object>();
        var age = new List<object>();
        store.At("name/first").AddListener(v => first.Add(v));
        store.At("name/last").AddListener(v => last.Add(v));
        store.At("name").AddListener(v => name.Add(v));
        store.At("age").AddListener(v => age.Add(v));
        
        store.Set(Sigo.Create("name", Sigo.Create("first", "phat", "last", "dam"), "age", "40"));
        store.Update();
        Assert.AreEqual(new [] { "phat" }, first);
        Assert.AreEqual(new [] { "dam" }, last);
        Assert.AreEqual(new [] { Sigo.Create("first", "phat", "last", "dam") }, name);
        Assert.AreEqual(new [] { "40" }, age);
        
        store.At("name/first").Set("Fat");
        store.Update();

        Assert.AreEqual(new [] { "phat", "Fat" }, first);
        Assert.AreEqual(new [] { "dam" }, last);
        Assert.AreEqual(new [] { Sigo.Create("first", "phat", "last", "dam"), Sigo.Create("first", "Fat", "last", "dam") }, name);
        Assert.AreEqual(new [] { "40" }, age);
    }
    
    [Test]
    public void AddListenerTests_input_input()
    {
        var store = new Store();
        var age = new List<object>();
        store.At("user/age").AddListener(v => age.Add(v));
        store.At("user/age").Set("40");
        store.Update();
        
        Assert.AreEqual(new [] {Sigo.Create("user", Sigo.Create("age", "40"))}, age);

    }
    
    // cay
    // - nut frozen
    // - co listener
    // - 
    // leo cay
    // - khac nhau
    // - co listener
    // da leo chua

    [Test]
    public void StoreTests()
    {
        var store = new Store();
        store.Update();
        store.At("name").AddListener(name =>
        {
            Debug.Log(name);
        });
        store.Update();
        store.Update();
        store.At("name/first").Set("Phat");
        store.Update();
        store.Update();
        store.At("name/last").Set("Dam");
        store.Update();
        store.Update();
        store.At("age").Set(40);
        store.Update();
        store.Update();
        
        Debug.Log(store.ToString());
    }
    
}
