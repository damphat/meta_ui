#region using

using System;
using System.Collections.Generic;
using MetaUI;
using NUnit.Framework;

#endregion

public class EditMode_QueryParse_Tests
{
    // A Test behaves as an ordinary method
    [Test]
    public void ParseSucess()
    {
        var keys = Query.Parse("/abc/**/123/.button/.text/.input");
        Assert.AreEqual(keys,
            new List<object> {null, "abc", "**", 123, Query.DotButton, Query.DotText, Query.DotInputField});


        Assert.AreEqual(new List<object> {""}, Query.Parse(""));
        Assert.AreEqual(new List<object> {"a"}, Query.Parse("a"));
        Assert.AreEqual(new List<object> {"a", ""}, Query.Parse("a/"));
        Assert.AreEqual(new List<object> {"a", "b"}, Query.Parse("a/b"));

        Assert.AreEqual(new List<object> {""}, Query.Parse(""));
        Assert.AreEqual(new List<object> {null, ""}, Query.Parse("/"));
        Assert.AreEqual(new List<object> {null, "a"}, Query.Parse("/a"));
        Assert.AreEqual(new List<object> {null, "a", ""}, Query.Parse("/a/"));
        Assert.AreEqual(new List<object> {null, "a", "b"}, Query.Parse("/a/b"));
    }

    [Test]
    public void ParseError()
    {
        Assert.Throws<ArgumentNullException>(() => Query.Parse(null));
        Assert.Throws<MetaUIException>(() => Query.Parse(".unknown"));
        Assert.Throws<MetaUIException>(() => Query.Parse("0xxx"));
    }
}