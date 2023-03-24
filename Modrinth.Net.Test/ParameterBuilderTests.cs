using Modrinth.Http;

namespace Modrinth.Net.Test;

[TestFixture]
public class ParameterBuilderTests
{
    [Test]
    public void Add_Parameters_Are_Correctly_Added()
    {
        // Arrange
        var builder = new ParameterBuilder();

        // Act
        builder.Add("key1", "value1");
        builder.Add("key2", 2);

        // Assert
        Assert.That(builder.ToString(), Is.EqualTo("key1=value1&key2=2&"));
    }

    [Test]
    public void Add_IgnoreNull_Is_True_Null_Values_Are_Ignored()
    {
        // Arrange
        var builder = new ParameterBuilder();

        // Act
        builder.Add("key1", null);
        builder.Add("key2", "value2", true);

        // Assert
        Assert.That(builder.ToString(), Is.EqualTo("key2=value2&"));
    }

    [Test]
    public void AddToRequest_RequestUri_Is_Correctly_Built()
    {
        // Arrange
        var builder = new ParameterBuilder();
        builder.Add("key1", "value1");
        builder.Add("key2", 2);

        var request = new HttpRequestMessage(HttpMethod.Get, "https://example.com");

        // Act
        builder.AddToRequest(request);

        // Assert
        Assert.That(request.RequestUri, Is.EqualTo(new Uri("https://example.com?key1=value1&key2=2&")));
    }
}