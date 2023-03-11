using Modrinth.Helpers;
using NUnit.Framework;

namespace Modrinth.Net.Test;

[TestFixture]
public class UrlParserTests
{
    [Test]
    // Valid URLs
    [TestCase("https://modrinth.com/mod/12345678", true, "12345678")]
    [TestCase("https://modrinth.com/plugin/plasmo-voice", true, "plasmo-voice")]
    [TestCase("https://modrinth.com/modpack/12345678", true, "12345678")]
    [TestCase("https://modrinth.com/datapack/datapack-slug", true, "datapack-slug")]
    [TestCase("https://modrinth.com/resourcepack/resourcepack-slug", true, "resourcepack-slug")]
    [TestCase("https://modrinth.com/shader/shader-slug", true, "shader-slug")]
    [TestCase("https://modrinth.com/mod/12345678/", true, "12345678")]
    // Invalid URLs
    [TestCase("https://not-modrinth.com/mod/12345678", false)]
    [TestCase("https://modrinth.us/mod/12345678/invalid/12345678", false)]
    [TestCase("http://modrinth.net/mod/12345678/invalid/12345678/invalid", false)]
    [TestCase("just a normal string because why not", false)]
    public void TestParse(string url, bool expectedResult, string? exceptedId = null)
    {
        var result = Helpers.UrlParser.TryParseModrinthUrl(url, out var id);
        Assert.That(result, Is.EqualTo(expectedResult));
        if (exceptedId != null)
        {
            Assert.That(id, Is.EqualTo(exceptedId));
        }
    }
    
    [Test]
    // Valid slugs
    [TestCase("valid-slug", true)]
    [TestCase("plasmo-voice", true)]
    [TestCase("12345678", true)]
    [TestCase("12345678-12345678", true)]
    [TestCase("12345678-12345678-12345678", true)]
    [TestCase("12345678-12345678-12345678-12345678", true)]
    [TestCase("12345678-12345678-12345678-12345678-12345678", true)]
    [TestCase("12345678-12345678-12345678-12345678-12345678-12345678", true)]
    [TestCase("12345678-12345678-12345678-12345678-12345678-12345678-12345678", true)]
    // Invalid slugs
    [TestCase("12345678-12345678-12345678-12345678-12345678-12345678-12345678-12345678", false)]
    [TestCase("not valid slug", false)]
    public void TestSlug(string slug, bool expectedResult)
    {
        var result = slug.ValidateModrinthSlug();
        Assert.That(result, Is.EqualTo(expectedResult));
    }
    
    [Test]
    // Valid ids
    [TestCase("P7dR8mSH", true)]
    [TestCase("12345678", true)]
    [TestCase("AANobbMI", true)]
    [TestCase("gvQqBUqZ", true)]
    [TestCase("YL57xq9U", true)]
    // Invalid ids
    [TestCase("12345 6789", false)]
    [TestCase("12345-6789", false)]
    public void TestId(string id, bool expectedResult)
    {
        var result = id.ValidateModrinthId();
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}