using Movies.WebApi.Models;

namespace Movies.WebApi.Interfaces;

public interface IMongoService {
    Task<IEnumerable<Movie>> Get(string? title = null, int? limit = null, int? skip = null);
}