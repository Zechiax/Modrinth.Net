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

## List of endpoints and their support in this library

| Name                                 | Method | Implemented |
|--------------------------------------|--------|-------------|
| Search projects                      | GET    | ✅           |
| Get a project                        | GET    |  ✅           |
| Modify a project                     | PATCH  |             |
| Delete a project                     | DELETE |   ✅          |
| Get multiple projects                | GET    |   ✅          |
| Edit multiple projects               | PATCH  |             |
| Get a list of random projects        | GET    |   ✅          |
| Create a project                     | POST   |             |
| Change project's icon                | PATCH  |   ✅          |
| Delete project's icon                | DELETE |    ✅         |
| Check project slug/ID validity       | GET    |    ✅         |
| Add a gallery image                  | POST   |             |
| Modify a gallery image               | PATCH  |             |
| Delete a gallery image               | DELETE |             |
| Get all of a project's dependencies  | GET    |    ✅         |
| Follow a project                     | POST   |     ✅        |
| Unfollow a project                   | DELETE |      ✅       |
| Schedule a project                   | POST   |             |
| Get list of project's versions       | GET    |      ✅       |
| Get a version                        | GET    |      ✅       |
| Modify a version                     | PATCH  |             |
| Delete a version                     | DELETE |        ✅     |
| Create a version                     | POST   |             |
| Schedule a version                   | POST   |             |
| Get multiple versions                | GET    |         ✅    |
| Add files to version                 | POST   |             |
| Get version from hash                | GET    |           ✅  |
| Delete a file from its hash          | DELETE |             |
| Latest version of a project from a hash, loader(s), and game version(s) | POST |             |
| Get versions from hashes             | POST   |             |
| Latest versions of multiple projects from hashes, loader(s), and game version(s) | POST |             |
| Get a user                           | GET    |✅             |
| Modify a user                        | PATCH  |             |
| Delete a user                        | DELETE |✅             |
| Get user from authorization header   | GET    |✅             |
| Get multiple users                   | GET    |✅             |
| Change user's avatar                 | PATCH  |             |
| Get user's projects                  | GET    |✅             |
| Get user's notifications             | GET    |✅             |
| Get user's followed projects         | GET    |             |
| Get user's payout history            | GET    |✅             |
| Withdraw payout balance to PayPal or Venmo | POST |             |
| Get a project's team members         | GET    |✅             |
| Get a team's members                 | GET    |✅             |
| Add a user to a team                 | POST   |             |
| Get the members of multiple teams    | GET    |✅             |
| Join a team                          | POST   |             |
| Modify a team member's information   | PATCH  |             |
| Remove a member from a team          | DELETE |             |
| Transfer team's ownership to another user | PATCH |             |
| Get a list of categories             | GET    |✅             |
| Get a list of loaders                | GET    |✅             |
| Get a list of game versions          | GET    |✅             |
| Get a list of licenses               | GET    |✅             |
| Get a list of donation platforms     | GET    |✅             |
| Get a list of report types           | GET    |✅             |
| Report a project, user, or version   | POST   |✅             |
| Various statistics about this Modrinth instance | GET    |✅             |


