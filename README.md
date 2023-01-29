# Modrinth.Net
[![GitHub](https://img.shields.io/github/license/Zechiax/Modrinth.Net?style=for-the-badge)](https://github.com/Zechiax/Modrinth.Net)
[![Nuget](https://img.shields.io/nuget/v/Modrinth.Net?style=for-the-badge)](https://www.nuget.org/packages/Modrinth.Net) 
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Modrinth.Net?label=NuGet%20Pre-release&style=for-the-badge)](https://www.nuget.org/packages/Modrinth.Net)


- C# Wrapper for the [Modrinth API](https://docs.modrinth.com/api-spec/)
- Previously known as "Modrinth.RestClient"

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

### Upgrade from 2.X.X to 3.0.0

The package has been renamed from "Modrinth.RestClient" to "Modrinth.Net", so you will need to do the following to upgrade:

1. Uninstall the old "Modrinth.RestClient" package
2. Install the new "Modrinth.Net" package
3. Replace the old namespace "Modrinth.RestClient" with the new namespace "Modrinth" in your code

- Old package: [Modrinth.RestClient](https://www.nuget.org/packages/Modrinth.RestClient)
- New package: [Modrinth.Net](https://www.nuget.org/packages/Modrinth.Net)

#### API Changes from 2.X.X to 3.0.0
- New client class
  - `ModrinthClient` is the new client class, which contains all the smaller API classes
  - It's similar to the API specification:
    - Instead of `client.GetProjectAsync("sodium")` you will do `client.Project.GetAsync("sodium")`
    - `client.GetProjectTeamMembersByProjectAsync("sodium")` will become `client.Team.GetProjectTeamAsync("sodium")`
    - And so on

```csharp
// Old
using Modrinth.RestClient;

var client = ModrinthApi.NewClient(userAgent: "My_Awesome_Project");
var project = await client.GetProjectAsync("sodium");

// New
using Modrinth;

var client = new ModrinthClient(userAgent: "My_Awesome_Project");
var project = await client.Project.GetAsync("sodium");
```