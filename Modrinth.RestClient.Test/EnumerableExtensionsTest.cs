using Modrinth.RestClient.Extensions;
using NUnit.Framework;

namespace Modrinth.RestClient.Test;

[TestFixture]
public class StringExtensionsTest
{
    [Test]
    public void TestToModrinthQueryString_TwoItems()
    {
        var items = new[] {"AABBCCDD", "EEFFGGHH"};
        const string expected = "[\"AABBCCDD\",\"EEFFGGHH\"]";
        var actual = items.ToModrinthQueryString();
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void TestToModrinthQueryString_OneItem()
    {
        var items = new[] {"AABBCCDD"};
        const string expected = "[\"AABBCCDD\"]";
        var actual = items.ToModrinthQueryString();
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void TestToModrinthQueryString_NoItems()
    {
        var items = Array.Empty<string>();
        const string expected = "[]";
        var actual = items.ToModrinthQueryString();
        Assert.That(actual, Is.EqualTo(expected));
    }
}