using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ArtechApi.Models;

public class User
{
    [BsonId] // Tells .NET this is an auto-generated ID
    [BsonRepresentation(BsonType.ObjectId)] // ID type
    public string? Id { get; set; }

    [BsonElement("FullName")] // Maps to MongoDB element (use if names are different)
    public string Name { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string UserName { get; set; } = String.Empty;
    public int JerseyNumber { get; set; }
}