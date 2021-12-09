using Movies.WebApi.Models;
using Movies.WebApi.Services;

namespace Movies.WebApi.GraphQL;

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