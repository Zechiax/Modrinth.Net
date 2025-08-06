namespace Modrinth.Net.Test.ModrinthApiTests;

[TestFixture]
public class ProjectColorTests : EndpointTests
{
    [Test]
    public async Task ProjectColor()
    {
        var project = await NoAuthClient.Project.GetAsync(TestProjectSlug);

        // Check that the project color is not null
        Assert.That(project.Color, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(project.Color.HasValue, Is.True);

            // Check that at least one of the color components is not 0, only 1 is enough
            Assert.That(project.Color.Value.R, Is.Not.EqualTo(0));
            Assert.That(project.Color.Value.G, Is.Not.EqualTo(0));
            Assert.That(project.Color.Value.B, Is.Not.EqualTo(0));
        });
    }

    [Test]
    public async Task ProjectColor_WithNoColor()
    {
        // TODO: Don't use a hardcoded project slug
        var project = await Client.Project.GetAsync("nocolorproject");

        // Check that the project color is not null
        Assert.That(project.Color, Is.Null);
        Assert.That(project.Color.HasValue, Is.False);
    }
}