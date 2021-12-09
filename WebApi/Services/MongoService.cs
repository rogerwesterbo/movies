using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;
using Movies.WebApi.Models;

namespace Movies.WebApi.Services;

public class MongoService
{
    private IMongoClient MongoClient { get; }
    private IMongoDatabase? Database { get; set; }
    private IMongoCollection<Movie>? MovieCollection { get; set; }

    public MongoService(IMongoClient mongoClient)
    {
        MongoClient = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));
        _ = Initialize(MongoClient);
    }

    public async Task<IEnumerable<Movie>> Get(string? title = null, int? limit = null, int? skip = null)
    {
        var movies = new List<Movie>();
        IFindFluent<Movie, Movie>? query;
        if (!string.IsNullOrEmpty(title))
        {
            var filter = Builders<Movie>.Filter.Regex(x => x.Title, BsonRegularExpression.Create(Regex.Escape(title)));
            query = MovieCollection.Find(filter);
        }
        else
            query = MovieCollection.Find(_ => true);

        if (skip != null)
            query.Skip(skip);

        if (limit != null)
            query.Limit(limit);

        movies = await query.ToListAsync();
        return movies;
    }

    private async Task Initialize(IMongoClient mongoClient)
    {
        Database = mongoClient.GetDatabase("test");
        
        MovieCollection = Database.GetCollection<Movie>("movies");
        var count = MovieCollection.CountDocuments(_ => true);
        if (count == 0)
            await SeedCollectionAsync();
    }

    private async Task SeedCollectionAsync()
    {
        var assemblyFolder = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = System.IO.Path.Combine(assemblyFolder, "./Data/", "netflix_titles.csv");

        using var reader = new StreamReader(filePath);
        string? line;

        var lineCount = 0;
        var movies = new List<Movie>();
        while ((line = reader.ReadLine()) != null)
        {
            lineCount++;
            if (lineCount == 1)
            {
                continue;
            }

            var csvRegex = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            var fields = csvRegex.Split(line);
            var lineArray = new string[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                lineArray[i] = fields[i].Replace("\"", "");
            }

            var type = MovieType.Unknow;
            var typeString = lineArray[1];
            if (typeString.Equals("Tv Show", StringComparison.CurrentCultureIgnoreCase))
            {
                type = MovieType.TvShow;
            }
            else if (typeString.Equals("Movie", StringComparison.CurrentCultureIgnoreCase))
            {
                type = MovieType.Movie;
            }

            var directorStringArray = lineArray[3].Split(" ");
            List<Director> directors = new();
            if (directorStringArray.Length == 2)
            {
                directors.Add(new Director
                {
                    FirstName = directorStringArray[0],
                    SurName = directorStringArray[1],
                });
            }

            var castArray = lineArray[4].Split(", ");
            List<Actor> actors = new();
            foreach (var actor in castArray)
            {
                var actorArray = actor.Split(" ");
                if (actorArray.Length >= 2)
                {
                    actors.Add(new Actor
                    {
                        FirstName = String.Join(" ", actorArray.Take(actorArray.Length - 1)),
                        SurName = actorArray.LastOrDefault(),
                    });
                }
            }

            var categoryArray = lineArray[10].Split(", ");
            var categories = new List<string>();
            foreach (var category in categoryArray)
                categories.Add(category);

            DateTime? created = null;
            if (DateTime.TryParse(lineArray[6], out DateTime createdDate))
                created = createdDate;

            var movie = new Movie
            {
                Type = type,
                Title = lineArray[2],
                Directors = directors,
                Description = lineArray[11],
                Cast = actors,
                Country = lineArray[5],
                Created = createdDate,
                ReleaseYear = lineArray[7],
                Rating = lineArray[8],
                Duration = lineArray[9],
                Categories = categories,
                StreamingService = "Netflix",
            };
            movies.Add(movie);
        }

        var count = movies.Count;
        if (MovieCollection != null)
            await MovieCollection.InsertManyAsync(movies).ContinueWith(done =>
            {
                Console.WriteLine($"Done seeding movies ({count})");
            });
        else
            Console.WriteLine("Collection does not exist ...");
    }
}
