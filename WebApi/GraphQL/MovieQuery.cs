using Movies.WebApi.Interfaces;
using Movies.WebApi.Models;

namespace Movies.WebApi.GraphQL;

public class MovieQuery
{
    private IMongoService MongoService { get; }

    public MovieQuery(IMongoService mongoService)
    {
        MongoService = mongoService ?? throw new ArgumentNullException(nameof(mongoService));
    }

    public async Task<IEnumerable<Movie?>> GetMovies(string? title = null, int? limit = null, int? skip = null)
    {
        var movies = await MongoService.Get(title, limit, skip);
        return movies;
    }
}