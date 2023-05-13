# Modrinth.Net

[![GitHub](https://img.shields.io/github/license/Zechiax/Modrinth.Net?style=for-the-badge)](https://github.com/Zechiax/Modrinth.Net)
[![Nuget](https://img.shields.io/nuget/v/Modrinth.Net?style=for-the-badge)](https://www.nuget.org/packages/Modrinth.Net)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Modrinth.Net?label=NuGet%20Pre-release&style=for-the-badge)](https://www.nuget.org/packages/Modrinth.Net)
[![Modrinth API](https://img.shields.io/badge/Modrinth%20API-v2.7.0-449C59?style=for-the-badge)](https://docs.modrinth.com/api-spec/)

- C# Wrapper for the [Modrinth API](https://docs.modrinth.com/api-spec/)
- For list of supported endpoints,
  see [List of endpoints and their support in this library](#list-of-endpoints-and-their-support-in-this-library)
    - The plan is to eventually cover all endpoints

## Usage

- Install the [NuGet package](https://www.nuget.org/packages/Modrinth.Net)

```csharp
using Modrinth;

var options = new ModrinthClientConfig 
{
    // Optional, if you want to access authenticated API endpoints
    ModrinthToken = "Your_Authentication_Token",
    // For Modrinth API, you must specify a user-agent
    // There is a default library user-agent, but it is recommended to specify your own
    UserAgent = "MyAwesomeProject"
};

var client = new ModrinthClient(options);

var project = await client.Project.GetAsync("sodium");

Console.WriteLine(project.Description);
```

But you don't have to provide options at all, you can just create a client with the default options:

```csharp
var client = new ModrinthClient();
```

### User-Agent

- You can also use the UserAgent class to help you create a valid user-agent
- User-Agent currently cannot be changed after the client has been created
- More info about the User-Agent can be found [here](https://docs.modrinth.com/api-spec/#section/User-Agents)

```csharp
using Modrinth;

// Note: All properties are optional, and will be ignored if they are null or empty
var userAgent = new UserAgent
{
    ProjectName = "ProjectName",
    ProjectVersion = "1.0.0",
    GitHubUsername = "Username",
    Contact = "contact@contact.com"
};

var options = new ModrinthClientOptions
{
    UserAgent = userAgent.ToString()
};

var client = new ModrinthClient(options);
```

### Search

- You can search for projects using the `SearchAsync` method, in the `Project` endpoint

```csharp
using Modrinth;

var client = new ModrinthClient(userAgent: "My_Awesome_Project");

var search = await client.Project.SearchAsync("sodium");

foreach (var project in search.Projects)
{
    Console.WriteLine(project.Title);
}
```

#### Search with facets/filtering

- You can filter the search results by using facets, which are a way to filter the results by certain criteria
- You can read more about facets [here](https://docs.modrinth.com/docs/tutorials/api_search/#facets)
- You can create a `FacetCollection` and add facets to it, and then pass it to the `SearchAsync` method

Example:

```csharp
// For search with facets, you first need to create a FacetCollection
var facets = new FacetCollection();

// Then you can add facets to it
// You add a facet by calling the Add method on the FacetCollection
// In one call, you can add multiple facets, they will be combined in an OR statement
// If you call Add again, the new facets will be combined in an AND statement with the previous ones

// Example:
facets.Add(Facet.Category("adventure"), Facet.Category("magic"));
facets.Add(Facet.Version("1.19.4"));

// This will create a query that looks like this:
// (category:adventure OR category:magic) AND version:1.19.4
// Basically it will search for projects that have the category "adventure" or "magic" on Minecraft version 1.19.4

// Then you can pass the FacetCollection to the SearchAsync method
var search = await _client.Project.SearchAsync("", facets: facets);
```

As `FacetCollection` implements `ICollection<T>`, you can use collection initializers to add facets to it, like this:

```csharp
var facets = new FacetCollection
{
    { Facet.Category("adventure"), Facet.Category("magic") },
    { Facet.Version("1.19.4") }
};
```

Which will create the same query as the previous example.

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
catch (ModrinthApiException e) when (e.Response.StatusCode == HttpStatusCode.NotFound) 
{
    Console.WriteLine("Project not found");
}
// Or you can catch the exception and handle all non-200 status codes
catch (ModrinthApiException e)
{
    Console.WriteLine($"API call failed with status code {e.Response.StatusCode}");
}
```

## List of endpoints and their support in this library

| Icon | Meaning         | Comment                                                                      |
|------|-----------------|------------------------------------------------------------------------------|
| ✅    | Implemented     |                                                                              |
| ❌    | Not implemented |                                                                              |
| ⚠️   | Untested        | The endpoint has been implemented, but no tests have been written for it yet |

### Project endpoints

| Name                                | Method | Implemented |
|-------------------------------------|--------|-------------|
| Search projects                     | GET    | ✅           |
| Get a project                       | GET    | ✅           |
| Modify a project                    | PATCH  | ❌           |
| Delete a project                    | DELETE | ⚠️          |
| Get multiple projects               | GET    | ✅           |
| Edit multiple projects              | PATCH  | ❌           |
| Get a list of random projects       | GET    | ✅           |
| Create a project                    | POST   | ❌           |
| Change project's icon               | PATCH  | ✅           |
| Delete project's icon               | DELETE | ✅           |
| Check project slug/ID validity      | GET    | ✅           |
| Add a gallery image                 | POST   | ✅           |
| Modify a gallery image              | PATCH  | ✅           |
| Delete a gallery image              | DELETE | ✅           |
| Get all of a project's dependencies | GET    | ✅           |
| Follow a project                    | POST   | ✅           |
| Unfollow a project                  | DELETE | ✅           |
| Schedule a project                  | POST   | ❌           |

### Version endpoints

| Name                           | Method | Implemented |
|--------------------------------|--------|-------------|
| Get list of project's versions | GET    | ✅           |
| Get a version                  | GET    | ✅           |
| Modify a version               | PATCH  | ❌           |
| Delete a version               | DELETE | ⚠️          |
| Create a version               | POST   | ❌           |
| Schedule a version             | POST   | ⚠️          |
| Get multiple versions          | GET    | ✅           |
| Add files to version           | POST   | ❌           |

### Version file endpoints

| Name                                                                             | Method | Implemented |
|----------------------------------------------------------------------------------|--------|------------|
| Get version from hash                                                            | GET    | ✅️         |
| Delete a file from its hash                                                      | DELETE | ⚠️         |
| Latest version of a project from a hash, loader(s), and game version(s)          | POST   | ✅️         |
| Get versions from hashes                                                         | POST   | ✅️         |
| Latest versions of multiple projects from hashes, loader(s), and game version(s) | POST   | ❌          |

### User endpoints

| Name                                       | Method | Implemented |
|--------------------------------------------|--------|-------------|
| Get a user                                 | GET    | ✅           |
| Modify a user                              | PATCH  | ❌           |
| Delete a user                              | DELETE | ❌           |
| Get user from authorization header         | GET    | ✅           |
| Get multiple users                         | GET    | ✅           |
| Change user's avatar                       | PATCH  | ✅           |
| Get user's projects                        | GET    | ✅           |
| Get user's notifications                   | GET    | ✅           |
| Get user's followed projects               | GET    | ✅           |
| Get user's payout history                  | GET    | ❌           |
| Withdraw payout balance to PayPal or Venmo | POST   | ❌           |

### Team endpoints

| Name                                      | Method | Implemented |
|-------------------------------------------|--------|-------------|
| Get a project's team members              | GET    | ✅           |
| Get a team's members                      | GET    | ✅           |
| Add a user to a team                      | POST   | ❌           |
| Get the members of multiple teams         | GET    | ✅           |
| Join a team                               | POST   | ❌           |
| Modify a team member's information        | PATCH  | ❌           |
| Remove a member from a team               | DELETE | ❌           |
| Transfer team's ownership to another user | PATCH  | ❌           |

### Tag endpoints

| Name                             | Method | Implemented |
|----------------------------------|--------|-------------|
| Get a list of categories         | GET    | ✅           |
| Get a list of loaders            | GET    | ✅           |
| Get a list of game versions      | GET    | ✅           |
| Get a list of licenses           | GET    | ✅           |
| Get a list of donation platforms | GET    | ✅           |
| Get a list of report types       | GET    | ✅           |

### Miscellaneous endpoints

| Name                                            | Method | Implemented |
|-------------------------------------------------|--------|-------------|
| Report a project, user, or version              | POST   | ❌           |
| Various statistics about this Modrinth instance | GET    | ✅           |

## Disclaimer

This is not an official Modrinth project. This is a third-party project that is not affiliated with Modrinth in any way.
