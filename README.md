# Modrinth.Net

[![GitHub](https://img.shields.io/github/license/Zechiax/Modrinth.Net?style=for-the-badge)](https://github.com/Zechiax/Modrinth.Net)
[![Nuget](https://img.shields.io/nuget/v/Modrinth.Net?style=for-the-badge)](https://www.nuget.org/packages/Modrinth.Net)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Modrinth.Net?label=NuGet%20Pre-release&style=for-the-badge)](https://www.nuget.org/packages/Modrinth.Net)
[![Modrinth API](https://img.shields.io/badge/Modrinth%20API-v2.7.0-449C59?style=for-the-badge)](https://docs.modrinth.com/api-spec/)

- C# Wrapper for the [Modrinth API](https://docs.modrinth.com/api-spec/)

## Usage

- Install the [NuGet package](https://www.nuget.org/packages/Modrinth.Net)

```csharp
using Modrinth;

// You must provide a user-agent, and optionally an authentication token if you wish to access authenticated API endpoints
var client = new ModrinthClient(userAgent: "My_Awesome_Project" , token: "Your_Authentication_Token");

var project = await client.Project.GetAsync("sodium");

Console.WriteLine(project.Description);
```

### Unsuccesful API calls

- If the API call was unsuccessful, the client will throw an `ModrinthApiException` exception
- This will be thrown if the API call return non-200 status code, or if the response body is not valid JSON
- This approach will be revisited in future versions

## Info

- This package was previously called 'Modrinth.RestClient', while completely rewriting it, I've decided to rename it
  to 'Modrinth.Net', continuing the versioning from the previous package
    - The old package can be found [here](https://www.nuget.org/packages/Modrinth.RestClient/)