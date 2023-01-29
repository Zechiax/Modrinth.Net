﻿using Flurl.Http;
using Modrinth.Net.Endpoints.Project;
using Modrinth.Net.Endpoints.Tag;
using Modrinth.Net.Endpoints.Team;
using Modrinth.Net.Endpoints.User;
using Modrinth.Net.Endpoints.VersionFile;
using Modrinth.Net.Exceptions;

namespace Modrinth.Net;


/// <summary>
/// Base for creating new clients using RestEase from <see cref="IModrinthClient"/> interface
/// </summary>
public class ModrinthClient : IModrinthClient
{
    /// <summary>
    /// API Url of the production server
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public const string BaseUrl = "https://api.modrinth.com/v2";

    /// <summary>
    /// API Url of the staging server
    /// </summary>
    public const string StagingBaseUrl = "https://staging-api.modrinth.com/v2";
    
    protected readonly FlurlClient Client;

    #region Endpoints

    /// <inheritdoc />
    public bool IsDisposed { get; private set; } = false;

    /// <inheritdoc />
    public IProjectApi Project { get; }
    
    /// <inheritdoc />
    public ITeamApi Team { get; }
    
    /// <inheritdoc />
    public IUserApi User { get; }
    
    /// <inheritdoc />
    public IVersionApi Version { get; }
    
    /// <inheritdoc />
    public ITagApi Tag { get; }
    
    /// <inheritdoc />
    public IVersionFile VersionFile { get; }

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="ModrinthClient"/> class.
    /// </summary>
    /// <param name="token">Authentication token for Authenticated requests</param>
    /// <param name="userAgent">User-Agent header you want to use while communicating with Modrinth API, it's recommended to set a uniquely-identifying one (<a href="https://docs.modrinth.com/api-spec/#section/User-Agents">see the docs</a>)</param>
    /// <param name="url">Custom API url, default is <see cref="BaseUrl"/></param>
    /// <returns></returns>
    public ModrinthClient(string? userAgent = null, string? token = null, string url = BaseUrl)
    {
        if (string.IsNullOrEmpty(userAgent))
        {
            throw new ArgumentException("User-Agent cannot be empty", nameof(userAgent));
        }

        Client = new FlurlClient(url)
            .WithHeader("User-Agent", userAgent)
            .WithHeader("Accept", "application/json")
            .WithHeader("Content-Type", "application/json");

        Client.Configure(settings =>
        {
            settings.OnErrorAsync = HandleFlurlErrorAsync;
        });
        
        if (!string.IsNullOrEmpty(token))
        {
            Client.WithHeader("Authorization", token);
        }

        Project = new ProjectApi(Client);
        Tag = new TagApi(Client);
        Team = new TeamApi(Client);
        User = new UserApi(Client);
        Version = new VersionApi(Client);
        VersionFile = new VersionFileApi(Client);
    }
    
    private static async Task HandleFlurlErrorAsync(FlurlCall call) {
        call.ExceptionHandled = true;

        throw new ModrinthApiException(call.Exception.Message, call.Response.ResponseMessage.StatusCode, call.Response.ResponseMessage.Content);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (IsDisposed || Client.IsDisposed) return;
        Client.Dispose();
        IsDisposed = true;
        GC.SuppressFinalize(this);
    }

}