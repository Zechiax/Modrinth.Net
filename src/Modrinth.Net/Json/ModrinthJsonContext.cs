using System.Text.Json.Serialization;
using Modrinth.Endpoints.Miscellaneous;
using Modrinth.Endpoints.Project;
using Modrinth.Models;
using Modrinth.Models.Enums;
using Modrinth.Models.Enums.File;
using Modrinth.Models.Enums.Project;
using Modrinth.Models.Enums.Version;
using Modrinth.Models.Errors;
using Modrinth.Models.Tags;
using Version = Modrinth.Models.Version;

namespace Modrinth.Json;

using File = Models.File;
using Index = Models.Enums.Index;

/// <inheritdoc />
[JsonSerializable(typeof(FileType))]
[JsonSerializable(typeof(ProjectStatus))]
[JsonSerializable(typeof(ProjectType))]
[JsonSerializable(typeof(ProjectVersionType))]
[JsonSerializable(typeof(DependencyType))]
[JsonSerializable(typeof(VersionRequestedStatus))]
[JsonSerializable(typeof(VersionStatus))]
[JsonSerializable(typeof(GameVersionType))]
[JsonSerializable(typeof(HashAlgorithm))]
[JsonSerializable(typeof(Index))]
[JsonSerializable(typeof(NotificationType))]
[JsonSerializable(typeof(PayoutWallet))]
[JsonSerializable(typeof(PayoutWalletType))]
[JsonSerializable(typeof(Permissions))]
[JsonSerializable(typeof(Role))]
[JsonSerializable(typeof(Side))]
[JsonSerializable(typeof(ResponseError))]
[JsonSerializable(typeof(Category))]
[JsonSerializable(typeof(DonationPlatform))]
[JsonSerializable(typeof(GameVersion))]
[JsonSerializable(typeof(LicenseTag))]
[JsonSerializable(typeof(Loader))]
[JsonSerializable(typeof(Dependency))]
[JsonSerializable(typeof(DonationUrl))]
[JsonSerializable(typeof(File))]
[JsonSerializable(typeof(Gallery))]
[JsonSerializable(typeof(Hashes))]
[JsonSerializable(typeof(License))]
[JsonSerializable(typeof(ModeratorMessage))]
[JsonSerializable(typeof(Notification))]
[JsonSerializable(typeof(PayoutData))]
[JsonSerializable(typeof(PayoutHistory))]
[JsonSerializable(typeof(Project))]
[JsonSerializable(typeof(SearchResponse))]
[JsonSerializable(typeof(SearchResult))]
[JsonSerializable(typeof(TeamMember))]
[JsonSerializable(typeof(User))]
[JsonSerializable(typeof(User[]))]
[JsonSerializable(typeof(UserPayoutHistoryEntry))]
[JsonSerializable(typeof(Version))]
[JsonSerializable(typeof(ModrinthStatistics))]
[JsonSerializable(typeof(SlugIdValidity))]
internal partial class ModrinthJsonContext : JsonSerializerContext;