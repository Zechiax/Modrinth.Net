# Modrinth.RestClient
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Modrinth.RestClient?style=for-the-badge)](https://www.nuget.org/packages/Modrinth.RestClient)

## Usage
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
  - So it can only get information, not upload (this will probably be implemented)
- For API specification [see here](https://docs.modrinth.com/api-spec/)
