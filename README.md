# Modrinth.Net
[![GitHub](https://img.shields.io/github/license/Zechiax/Modrinth.RestClient?style=for-the-badge)](https://github.com/Zechiax/Modrinth.RestClient)
[![Nuget](https://img.shields.io/nuget/v/Modrinth.RestClient?style=for-the-badge)](https://www.nuget.org/packages/Modrinth.RestClient) 
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Modrinth.RestClient?label=NuGet%20Pre-release&style=for-the-badge)](https://www.nuget.org/packages/Modrinth.RestClient)


- C# Wrapper for the [Modrinth API](https://docs.modrinth.com/api-spec/)
- Previously known as "Modrinth.RestClient"

## Usage

- Install the [NuGet package](https://www.nuget.org/packages/Modrinth.Net)

```csharp
using Modrinth.Net;

// You must provide a user-agent, and optionally an authentication token if you wish to access authenticated API endpoints
var client = new ModrinthClient(userAgent: "My_Awesome_Project" , token: "Your_Authentication_Token");

var project = await client.Project.GetAsync("sodium");

Console.WriteLine(project.Description);
```

### Upgrade from 2.X.X to 3.0.0

The package has been renamed from "Modrinth.RestClient" to "Modrinth.Net", so you will need to do the following to upgrade:

1. Uninstall the old "Modrinth.RestClient" package
2. Install the new "Modrinth.Net" package
3. Replace the old namespace "Modrinth.RestClient" with the new namespace "Modrinth.Net" in your code

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
using Modrinth.Net;

var client = new ModrinthClient(userAgent: "My_Awesome_Project");
var project = await client.Project.GetAsync("sodium");
```