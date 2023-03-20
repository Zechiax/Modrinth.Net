namespace Modrinth.Net.Test.ModrinthApiTests;

[TestFixture]
public class ProjectGalleryTests : EndpointTests
{
    [Test]
    public async Task UploadModifyAndDeleteGalleryImage_WithValidId_ShouldSuccessfullyUploadModifyAndDelete()
    {
        // Not a unit test, but I can't think of a better way to test this now
        var guid = Guid.NewGuid().ToString();
        var imageTitle = $"TestImage{guid}";
        
        // Will throw exception if not authorized / some other error
        await Client.Project.AddGalleryImageAsync(ModrinthNetTestProjectId, Icon.FullName, true, imageTitle, "TestImage");
        
        var project = await Client.Project.GetAsync(ModrinthNetTestProjectId);
        Assert.That(project.Gallery, Is.Not.Null);
        
        var image = project.Gallery!.FirstOrDefault(i => i.Title == imageTitle);
        Assert.That(image, Is.Not.Null);
        
        // Let's modify the image
        await Client.Project.ModifyGalleryImageAsync(ModrinthNetTestProjectId, image!.Url, false, imageTitle, "TestImageModified");
        
        // Check that the image was modified
        project = await Client.Project.GetAsync(ModrinthNetTestProjectId);
        Assert.That(project.Gallery, Is.Not.Null);
        image = project.Gallery!.FirstOrDefault(i => i.Title == imageTitle);
        Assert.That(image, Is.Not.Null);
        Assert.That(image!.Description, Is.EqualTo("TestImageModified"));
        
        await Client.Project.DeleteGalleryImageAsync(ModrinthNetTestProjectId, image!.Url);
        
        project = await Client.Project.GetAsync(ModrinthNetTestProjectId);
        if (project.Gallery == null)
            return;
        
        // Check that the gallery, if not null, does not contain the image
        Assert.That(project.Gallery.Any(i => i.Title == imageTitle), Is.False);
    }
}