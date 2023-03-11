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
var client = new ModrinthClient(userAgent: "My_Awesome_Project", token: "Your_Authentication_Token");

var project = await client.Project.GetAsync("sodium");

Console.WriteLine(project.Description);
```

### User-Agent

- You can also use the UserAgent class to help you create a valid user-agent
- User-Agent current cannot be changed after the client has been created
- More info about the User-Agent can be found [here](https://docs.modrinth.com/api-spec/#section/User-Agents)

```csharp
using Modrinth;
using Modrinth.Client;

// Note: All properties are optional, and will be ignored if they are null or empty
var userAgent = new UserAgent
{
    ProjectName = "ProjectName",
    ProjectVersion = "1.0.0",
    GitHubUsername = "Username",
    Contact = "contact@contact.com"
};

var client = new ModrinthClient(userAgent: userAgent, token: "Your_Authentication_Token");
```

### Unsuccesful API calls

- If the API call was unsuccessful, the client will throw an `ModrinthApiException` exception
- This will be thrown if the API call return non-200 status code, or if the response body is not valid JSON
- This approach will be revisited in future versions

```csharp
using Modrinth;
using Modrinth.Exceptions;
using System.Net;

var client = new ModrinthClient(userAgent: "My_Awesome_Project");

try 
{
    var project = await _client.Project.GetAsync("non-existent-project");
    
    Console.WriteLine(project.Title);
}
// You can catch the exception and only handle the 404 status code
catch (ModrinthApiException e) when (e.StatusCode == HttpStatusCode.NotFound) 
{
    Console.WriteLine("Project not found");
}
// Or you can catch the exception and handle all non-200 status codes
catch (ModrinthApiException e)
{
    Console.WriteLine($"API call failed with status code {e.StatusCode}");
}
```

## Info

- This package was previously called 'Modrinth.RestClient', while completely rewriting it, I've decided to rename it
  to 'Modrinth.Net', continuing the versioning from the previous package
    - The old package can be found [here](https://www.nuget.org/packages/Modrinth.RestClient/)