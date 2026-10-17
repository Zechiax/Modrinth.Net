using System.Reflection;
using System.Text.Json;
using Modrinth.Http;
using Modrinth.Models;

namespace Modrinth.Net.Test;

[TestFixture]
public class ModrinthColorConverterTests
{
    private const int ApiColor = 8703084;

    private static T RoundTrip<T>(T value, JsonSerializerOptions? options = null)
    {
        var json = JsonSerializer.Serialize(value, options);
        return JsonSerializer.Deserialize<T>(json, options)!;
    }

    private static JsonSerializerOptions GetApiSerializerOptions()
    {
        var field = typeof(Requester).GetField("_jsonSerializerOptions",
            BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.That(field, Is.Not.Null, "Requester._jsonSerializerOptions not found");

        var requester = new Requester(new ModrinthClientConfig());
        return (JsonSerializerOptions)field!.GetValue(requester)!;
    }

    [Test]
    public void Project_Color_RoundTrips()
    {
        var project = JsonSerializer.Deserialize<Project>($$"""{"Color": {{ApiColor}}}""")!;
        Assert.That(project.Color, Is.Not.Null);

        var roundTripped = RoundTrip(project);

        Assert.That(roundTripped.Color, Is.EqualTo(project.Color));
        Assert.That(roundTripped.Color!.Value.ToArgb(), Is.EqualTo(ApiColor));
    }

    [Test]
    public void SearchResult_Color_RoundTrips()
    {
        var searchResult = JsonSerializer.Deserialize<SearchResult>($$"""{"Color": {{ApiColor}}}""")!;
        Assert.That(searchResult.Color, Is.Not.Null);

        var roundTripped = RoundTrip(searchResult);

        Assert.That(roundTripped.Color, Is.EqualTo(searchResult.Color));
        Assert.That(roundTripped.Color!.Value.ToArgb(), Is.EqualTo(ApiColor));
    }

    [Test]
    public void Project_Color_IsSerializedAsApiInteger()
    {
        var project = JsonSerializer.Deserialize<Project>($$"""{"Color": {{ApiColor}}}""")!;

        var json = JsonSerializer.Serialize(project);
        using var document = JsonDocument.Parse(json);

        var color = document.RootElement.GetProperty("Color");
        Assert.That(color.ValueKind, Is.EqualTo(JsonValueKind.Number));
        Assert.That(color.GetInt32(), Is.EqualTo(ApiColor));
    }

    [Test]
    public void Project_NullColor_RoundTrips()
    {
        var project = JsonSerializer.Deserialize<Project>("""{"Color": null}""")!;
        Assert.That(project.Color, Is.Null);

        var json = JsonSerializer.Serialize(project);
        using var document = JsonDocument.Parse(json);
        Assert.That(document.RootElement.GetProperty("Color").ValueKind, Is.EqualTo(JsonValueKind.Null));

        Assert.That(JsonSerializer.Deserialize<Project>(json)!.Color, Is.Null);
    }

    [Test]
    public void Project_Color_RoundTrips_WithApiSerializerOptions()
    {
        var options = GetApiSerializerOptions();

        var project = JsonSerializer.Deserialize<Project>(
            $$"""{"id": "AABBCCDD", "slug": "test-project", "color": {{ApiColor}}}""", options)!;
        Assert.That(project.Color, Is.Not.Null);

        var json = JsonSerializer.Serialize(project, options);
        using (var document = JsonDocument.Parse(json))
        {
            Assert.That(document.RootElement.GetProperty("color").GetInt32(), Is.EqualTo(ApiColor));
        }

        var roundTripped = JsonSerializer.Deserialize<Project>(json, options)!;
        Assert.Multiple(() =>
        {
            Assert.That(roundTripped.Color, Is.EqualTo(project.Color));
            Assert.That(roundTripped.Id, Is.EqualTo("AABBCCDD"));
            Assert.That(roundTripped.Slug, Is.EqualTo("test-project"));
        });
    }
}
