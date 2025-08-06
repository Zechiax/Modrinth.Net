using Modrinth.Models.Enums.File;

namespace Modrinth.Net.Test.ModrinthApiTests;

[TestFixture]
public class SpecificTests : UnauthenticatedTestBase
{
    [Test]
    public async Task FileTypeFieldNotDeserializable()
    {
        // Regarding this issue: https://github.com/Zechiax/Modrinth.Net/issues/73
        // This version couldn't be deserialized, as the FileType field wouldn't deserialize to the enum
        // https://api.modrinth.com/v2/version/YwlGTeST

        var version = await NoAuthClient.Version.GetAsync("YwlGTeST");
        Assert.Multiple(() =>
        {
            Assert.That(version.Files[0].FileType, Is.Null);
            Assert.That(version.Files[1].FileType, Is.EqualTo(FileType.RequiredResourcePack));
        });
    }
}