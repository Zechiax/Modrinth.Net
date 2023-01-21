﻿using System.Reflection;

namespace Modrinth.RestClient.Test;

[SetUpFixture]
public class EndpointTests
{
    protected IModrinthApi _client = null!;
    
    [OneTimeSetUp]
    public void SetUp()
    {
        var token = Environment.GetEnvironmentVariable("MODRINTH_TOKEN");
        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("MODRINTH_TOKEN environment variable is not set.");
        }
        var userAgent = $"Zechiax/Modrinth.RestClient.Test/{Assembly.GetExecutingAssembly().GetName().Version}";
        _client = new ModrinthApi(url: ModrinthApi.StagingBaseUrl, userAgent: userAgent , token: token);
    }
}