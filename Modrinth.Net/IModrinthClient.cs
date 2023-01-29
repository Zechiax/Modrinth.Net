using Modrinth.Net.Endpoints.Project;
using Modrinth.Net.Endpoints.Tag;
using Modrinth.Net.Endpoints.Team;
using Modrinth.Net.Endpoints.User;
using Modrinth.Net.Endpoints.VersionFile;

namespace Modrinth.Net;

/// <summary>
/// Entry point for the API
/// </summary>
public interface IModrinthClient : IDisposable
{
    /// <summary>
    /// If the client has been disposed 
    /// </summary>
    public bool IsDisposed { get; }
    
    /// <inheritdoc cref="IProjectApi" />
    IProjectApi Project { get; }
    
    /// <inheritdoc cref="ITeamApi" />
    ITeamApi Team { get; }
    
    /// <inheritdoc cref="IUserApi" />
    IUserApi User { get; }
    
    /// <inheritdoc cref="IVersionApi" />
    IVersionApi Version { get; }
    
    /// <inheritdoc cref="ITagApi" />
    ITagApi Tag { get; }
    
    /// <inheritdoc cref="IVersionFile" />
    IVersionFile VersionFile { get; }
}