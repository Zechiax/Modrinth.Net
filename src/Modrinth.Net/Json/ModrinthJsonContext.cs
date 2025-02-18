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
internal partial class ModrinthJsonContext : JsonSerializerContext
{
    public static readonly Dictionary<Type, object> TypeInfoMap;

    static ModrinthJsonContext()
    {
        TypeInfoMap = new Dictionary<Type, object>
        {
            {
                typeof(FileType), Default.FileType
            },
            {
                typeof(ProjectStatus), Default.ProjectStatus
            },
            {
                typeof(ProjectType), Default.ProjectType
            },
            {
                typeof(ProjectVersionType), Default.ProjectVersionType
            },
            {
                typeof(DependencyType), Default.DependencyType
            },
            {
                typeof(VersionRequestedStatus), Default.VersionRequestedStatus
            },
            {
                typeof(VersionStatus), Default.VersionStatus
            },
            {
                typeof(GameVersionType), Default.GameVersionType
            },
            {
                typeof(HashAlgorithm), Default.HashAlgorithm
            },
            {
                typeof(Index), Default.Index
            },
            {
                typeof(NotificationType), Default.NotificationType
            },
            {
                typeof(PayoutWallet), Default.PayoutWallet
            },
            {
                typeof(PayoutWalletType), Default.PayoutWalletType
            },
            {
                typeof(Permissions), Default.Permissions
            },
            {
                typeof(Role), Default.Role
            },
            {
                typeof(Side), Default.Side
            },
            {
                typeof(ResponseError), Default.ResponseError
            },
            {
                typeof(Category), Default.Category
            },
            {
                typeof(DonationPlatform), Default.DonationPlatform
            },
            {
                typeof(GameVersion), Default.GameVersion
            },
            {
                typeof(LicenseTag), Default.LicenseTag
            },
            {
                typeof(Loader), Default.Loader
            },
            {
                typeof(Dependency), Default.Dependency
            },
            {
                typeof(DonationUrl), Default.DonationUrl
            },
            {
                typeof(File), Default.File
            },
            {
                typeof(Gallery), Default.Gallery
            },
            {
                typeof(Hashes), Default.Hashes
            },
            {
                typeof(License), Default.License
            },
            {
                typeof(ModeratorMessage), Default.ModeratorMessage
            },
            {
                typeof(Notification), Default.Notification
            },
            {
                typeof(PayoutData), Default.PayoutData
            },
            {
                typeof(PayoutHistory), Default.PayoutHistory
            },
            {
                typeof(Project), Default.Project
            },
            {
                typeof(SearchResponse), Default.SearchResponse
            },
            {
                typeof(SearchResult), Default.SearchResult
            },
            {
                typeof(TeamMember), Default.TeamMember
            },
            {
                typeof(User), Default.User
            },
            {
                typeof(User[]), Default.UserArray
            },
            {
                typeof(UserPayoutHistoryEntry), Default.UserPayoutHistoryEntry
            },
            {
                typeof(Version), Default.Version
            },
            {
                typeof(ModrinthStatistics), Default.ModrinthStatistics
            },
            {
                typeof(SlugIdValidity), Default.SlugIdValidity
            }
        };
    }
}