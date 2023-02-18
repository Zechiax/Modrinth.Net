using NUnit.Framework;

namespace Modrinth.Net.Test.ModrinthApiTests;

[TestFixture]
public class ProjectColorTests : EndpointTests
{
    // Check that the project color is not null
    [Test]
    public async Task ProjectColor()
    {
        var project = await _client.Project.GetAsync("gravestones");
        Assert.That(project.Color, Is.Not.Null);
    }
    
}