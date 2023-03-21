using Modrinth.Endpoints.Miscellaneous;
using Modrinth.Endpoints.Project;
using Modrinth.Endpoints.Tag;
using Modrinth.Endpoints.Team;
using Modrinth.Endpoints.User;
using Modrinth.Endpoints.Version;

namespace Modrinth;

/// <summary>
///     Entry point for the API
/// </summary>
public interface IModrinthClient : IDisposable
{
    /// <summary>
    ///     Whether the client has been disposed
    /// </summary>
    bool IsDisposed { get; }

    /// <inheritdoc cref="IProjectEndpoint" />
    IProjectEndpoint Project { get; }

    /// <inheritdoc cref="ITeamEndpoint" />
    ITeamEndpoint Team { get; }

    /// <inheritdoc cref="IUserEndpoint" />
    IUserEndpoint User { get; }

    /// <inheritdoc cref="Endpoints.Version.IVersionFileEndpoint" />
    IVersionFileEndpoint Version { get; }

    /// <inheritdoc cref="ITagEndpoint" />
    ITagEndpoint Tag { get; }

    /// <inheritdoc cref="Endpoints.VersionFile.IVersionFileEndpoint" />
    Endpoints.VersionFile.IVersionFileEndpoint VersionFileEndpoint { get; }

    /// <inheritdoc cref="IMiscellaneousEndpoint" />
    IMiscellaneousEndpoint Miscellaneous { get; }
}