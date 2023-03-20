﻿using System.Net;
using Flurl.Http;
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
using Modrinth.Models.Errors;
using Newtonsoft.Json;

namespace Modrinth;

/// <summary>
///     A client for the Modrinth API
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

    private readonly FlurlClient _client;

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

        _client = new FlurlClient(url)
            .WithHeader("User-Agent", userAgent)
            .WithHeader("Accept", "application/json")
            .WithHeader("Content-Type", "application/json");

        var serializerSettings = new JsonSerializerSettings
        {
            Converters = {new ColorConverter()}
        };

        _client.Configure(settings =>
        {
            settings.OnErrorAsync = HandleFlurlErrorAsync;
            settings.JsonSerializer = new NewtonsoftJsonSerializer(serializerSettings);
        });

        if (!string.IsNullOrEmpty(token)) _client.WithHeader("Authorization", token);

        Project = new ProjectApi(_client);
        Tag = new TagApi(_client);
        Team = new TeamApi(_client);
        User = new UserApi(_client);
        Version = new VersionApi(_client);
        VersionFile = new VersionFileApi(_client);
        Miscellaneous = new MiscellaneousApi(_client);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (IsDisposed || _client.IsDisposed) return;
        _client.Dispose();
        IsDisposed = true;
        GC.SuppressFinalize(this);
    }

    private static async Task HandleFlurlErrorAsync(FlurlCall call)
    {
        call.ExceptionHandled = true;

        // Try to parse Response error
        ResponseError? error = null!;
        try
        {
            error = await call.Response.GetJsonAsync<ResponseError>();
        }
        catch (FlurlHttpException)
        {
        }
        
        var message =
                $"An error occurred while communicating with Modrinth API: {call.Response.ResponseMessage.ReasonPhrase}";
        message += $"\n{error?.Error}: {error?.Description}";

        throw new ModrinthApiException(
            message,
            call.Response.ResponseMessage.StatusCode,
            call.Response.ResponseMessage.Content, call.Exception,
            error);
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