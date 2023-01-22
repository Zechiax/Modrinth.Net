# Modrinth.RestClient
[![Nuget](https://img.shields.io/nuget/v/Modrinth.RestClient?style=for-the-badge)](https://www.nuget.org/packages/Modrinth.RestClient) [![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Modrinth.RestClient?style=for-the-badge)](https://www.nuget.org/packages/Modrinth.RestClient)
- Modrinth API wrapper using [RestEase](https://github.com/canton7/RestEase) library

# ⚠️ Project is currently undergoing major rewrite to version 3.0.0 ⚠️
- Versions <3.0.0 are not supported anymore as there will be breaking changes in the future, but they work for now.
- I don't recommend using alpha versions, but if you want to, you can find them on NuGet - but be aware that they will change a lot.

## Usage

### Version 3.0.0
```csharp
using Modrinth.RestClient;

// You must provide a user-agent, and optionally an authentication token if you wish to access authenticated API endpoints
var client = new ModrinthClient(userAgent: "My_Awesome_Project" , token: "Your_Authentication_Token");

var project = await client.Project.GetAsync("sodium");

Console.WriteLine(project.Description);
```

#### How does version 3.0.0 differ from 2.X.X?
- Dropped use of `RestEase` library
  - It's a great library, but it was not really suited for this project
  - Currently using [Flurl](https://flurl.dev/) library to create and send requests
- Not using 1 big API class anymore, but rather multiple smaller ones - for each endpoint
  - To provide more unified experience, there is a `ModrinthClient` class, which contains all the smaller API classes
  - It's easier to use, as it will be more similar to the API specification
    - Instead of `client.GetProjectAsync("sodium")` you will do `client.Project.GetAsync("sodium")`
- It's set up to be easily extendable
- All methods will be unit tested
- It has the foundation for authenticated API endpoints, some of them are already implemented

### Version 2.X.X
- All methods are asynchronous
```csharp
using Modrinth.RestClient;

// Modrinth recommends to set a uniquely-identifying user-agent
var api = ModrinthApi.NewClient(userAgent: "My_Awesome_Project");

var project = await api.GetProjectAsync("sodium");
        
Console.WriteLine(project.Description);
```

## Info
- As of right now, this does not cover every endpoint, but rather most of the common `GET` endpoints
  - So it can only get information, not upload
- For API specification [see here](https://docs.modrinth.com/api-spec/)
- For getting response status code, which do not indicate success, [see here](https://github.com/canton7/RestEase#response-status-codes)
