﻿using Modrinth.Models.Tags;

namespace Modrinth.Endpoints.Tag;

/// <inheritdoc />
public class TagApi : ITagApi
{
    private const string TagPathSegment = "tag";
    private readonly IRequester _client;

    public TagApi(IRequester client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<Category[]> GetCategoriesAsync()
    {
        // return await _client.Request(TagPathSegment, "category")
        //     .GetJsonAsync<Category[]>();
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment+ '/' + "category", UriKind.Relative);
        
        return await _client.GetJsonAsync<Category[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Loader[]> GetLoadersAsync()
    {
        // return await _client.Request(TagPathSegment, "loader")
        //     .GetJsonAsync<Loader[]>();
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment+ '/' + "loader", UriKind.Relative);
        
        return await _client.GetJsonAsync<Loader[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<GameVersion[]> GetGameVersionsAsync()
    {
        // return await _client.Request(TagPathSegment, "game_version")
        //     .GetJsonAsync<GameVersion[]>();
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/'+ "game_version", UriKind.Relative);
        
        return await _client.GetJsonAsync<GameVersion[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<License[]> GetLicensesAsync()
    {
        // return await _client.Request(TagPathSegment, "license")
        //     .GetJsonAsync<License[]>();
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment+ '/' + "license", UriKind.Relative);
        
        return await _client.GetJsonAsync<License[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<DonationPlatform[]> GetDonationPlatformsAsync()
    {
        // return await _client.Request(TagPathSegment, "donation_platform")
        //     .GetJsonAsync<DonationPlatform[]>();
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment + '/' + "donation_platform", UriKind.Relative);
        
        return await _client.GetJsonAsync<DonationPlatform[]>(reqMsg).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<string[]> GetReportTypesAsync()
    {
        // return await _client.Request(TagPathSegment, "report_type")
        //     .GetJsonAsync<string[]>();
        
        var reqMsg = new HttpRequestMessage();
        reqMsg.Method = HttpMethod.Get;
        reqMsg.RequestUri = new Uri(TagPathSegment+ '/' + "report_type", UriKind.Relative);
        
        return await _client.GetJsonAsync<string[]>(reqMsg).ConfigureAwait(false);
    }
}