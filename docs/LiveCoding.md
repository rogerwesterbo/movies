# Movies

Just testing graphql with memory data and perhaps mongodb?!

# Prerequisite
- Angular CLI (newest :tada: )
- .Net Core 6
- Docker-Desktop

# Backend:
- create git repo
- ```dotnet new webapi -o WebApi -n Movies.WebApi```
- ```dotnet add ./WebApi package HotChocolate.AspNetCore```
- create a model foler
    - create a simple model for movies
        - Movie
            - Id
            - Title
            - Type (TVShow / Movie)
            - Director
            - Cast
            - Country
            - Release year
            - Created
            - Rating 
            - Duration
        - Director
            - FirstName
            - SurName
- Add GraphQL in services
```
builder.Services
        .AddGraphQLServer()
        .AddQueryType<MovieQuery>();
```
- Add GraphQL in Endpoints
```
app
    .UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapGraphQL();
    });
```
- Add some data:
```csharp
public List<Movie> _movies => new List<Movie>
    {
        new()
        {
            Id = Random.Shared.Next(1, int.MaxValue),
            ExternalId = Guid.NewGuid().ToString(),
            Title = "Sopranos",
            Type = MovieType.TvShow,
            Created = DateTime.UtcNow,
            Cast = "Ganster stuff",
            Country = "America",
            Director = new Director { FirstName = "Noen", SurName = "Andre" }
        },
        new()
        {
            Id = 2,
            ExternalId = Guid.NewGuid().ToString(),
            Title = "Matrix",
            Type=MovieType.Movie,
            Created = DateTime.UtcNow,
            Cast = "Religion ...",
            Country = "America",
            Director = new Director { FirstName = "Gunnar", SurName = "Hendriksen" }
        },
        new()
        {
            Id = 1,
            ExternalId = Guid.NewGuid().ToString(),
            Title = "Roger tester",
            Type = MovieType.Movie,
            Created = DateTime.UtcNow,
            Cast = "Bare tull og tøys",
            Country = "Norway",
            Director = new Director { FirstName = "Roger", SurName = "Westerbø" }
        }

    };
```

## Mongodb
- create a free mongodb instance https://cloud.mongodb.com/
- Add package to Movies.Webapi
```dotnet add ./WebApi package MongoDb.Driver```
- Add mongodb to services
```
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));
```
- Add settings to appsettings.json / appsettings.developer.json
```
 "ConnectionStrings": {
    "MongoDb": "mongodb://MyUser:MyPassword@mongodb:27017/MyDb"
  }
```
- Add MongoQuery.cs
```csharp
public class MovieQuery
{
    public MongoService MongoService { get; }

    public MovieQuery(MongoService mongoService)
    {
        MongoService = mongoService ?? throw new ArgumentNullException(nameof(mongoService));
    }

    public async Task<IEnumerable<Movie?>> GetMovies(string? title = null, int? limit = null, int? skip = null)
    {
        var movies = await MongoService.Get(title, limit, skip);
        return movies;
    }
}
```
- Add MongoService.cs

# Docker
 - create ```<git root>/.env``` file
 - create docker folder, with ```docker-compose.yaml``` file

# Frontend

- ```ng```angular 
