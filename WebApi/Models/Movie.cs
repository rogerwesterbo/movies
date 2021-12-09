using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Movies.WebApi.Models;

public class Movie
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? ExternalId { get; set; }
    public string? Title { get; set; }
    public MovieType Type { get; set; }
    public List<Director>? Directors { get; set; }
    public string? Description { get; set; }
    public List<Actor>? Cast { get; set; }
    public string? Country { get; set; }
    public string? Duration { get; set; }
    public string? Rating { get; set; }
    public List<string>? Categories { get; set; }
    public DateTime? Created { get; set; }
    public string? ReleaseYear { get; set; }
    public string? StreamingService { get; set; }
}
