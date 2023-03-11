﻿using Flurl.Http;
using Flurl.Http.Configuration;
using Modrinth.Client;
using Modrinth.Endpoints.Miscellaneous;
using Modrinth.Endpoints.Project;
using Modrinth.Endpoints.Tag;
using Modrinth.Endpoints.Team;
using Modrinth.Endpoints.User;
using Modrinth.Endpoints.Version;
using Modrinth.Endpoints.VersionFile;
using Modrinth.Exceptions;
using Modrinth.JsonConverters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Modrinth;

/// <summary>
///     Base for creating new clients using RestEase from <see cref="IModrinthClient" /> interface
/// </summary>
public class ModrinthClient : IModrinthClient
{
    /// <summary>
    ///     API Url of the production server
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public const string BaseUrl = "https://api.modrinth.com/v2";

    /// <summary>
    ///     API Url of the staging server
    /// </summary>
    public const string StagingBaseUrl = "https://staging-api.modrinth.com/v2";

    protected readonly FlurlClient Client;

    /// <inheritdoc />
    public ModrinthClient(UserAgent userAgent, string? token = null, string url = BaseUrl)
        : this(userAgent.ToString(), token, url)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ModrinthClient" /> class.
    /// </summary>
    /// <param name="token">Authentication token for Authenticated requests</param>
    /// <param name="userAgent">
    ///     User-Agent header you want to use while communicating with Modrinth API, it's recommended to
    ///     set a uniquely-identifying one (<a href="https://docs.modrinth.com/api-spec/#section/User-Agents">see the docs</a>)
    /// </param>
    /// <param name="url">Custom API url, default is <see cref="BaseUrl" /></param>
    /// <returns></returns>
    public ModrinthClient(string? userAgent = null, string? token = null, string url = BaseUrl)
    {
        if (string.IsNullOrEmpty(userAgent))
            throw new ArgumentException("User-Agent cannot be empty", nameof(userAgent));

        Client = new FlurlClient(url)
            .WithHeader("User-Agent", userAgent)
            .WithHeader("Accept", "application/json")
            .WithHeader("Content-Type", "application/json");

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new ColorConverter(),
                new JsonStringEnumConverter()
            }
        };

        Client.Configure(settings =>
        {
            settings.OnErrorAsync = HandleFlurlErrorAsync;
            settings.JsonSerializer = new DefaultJsonSerializer(jsonSerializerOptions);
        });

        if (!string.IsNullOrEmpty(token)) Client.WithHeader("Authorization", token);

        Project = new ProjectApi(Client);
        Tag = new TagApi(Client);
        Team = new TeamApi(Client);
        User = new UserApi(Client);
        Version = new VersionApi(Client);
        VersionFile = new VersionFileApi(Client);
        Miscellaneous = new MiscellaneousApi(Client);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (IsDisposed || Client.IsDisposed) return;
        Client.Dispose();
        IsDisposed = true;
        GC.SuppressFinalize(this);
    }

    private static async Task HandleFlurlErrorAsync(FlurlCall call)
    {
        call.ExceptionHandled = true;

        throw new ModrinthApiException(
            "An error occurred while communicating with Modrinth API; See the inner exception for more details",
            call.Response.ResponseMessage.StatusCode,
            call.Response.ResponseMessage.Content, call.Exception);
    }

    #region Endpoints

    /// <inheritdoc />
    public bool IsDisposed { get; private set; }

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

    /// <inheritdoc />
    public IMiscellaneousApi Miscellaneous { get; }

    #endregion
}