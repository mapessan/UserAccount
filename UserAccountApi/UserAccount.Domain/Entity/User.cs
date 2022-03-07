using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserAccount.Domain.Entity;

public class User
{
    [BsonElement("_id")]
    [JsonPropertyName("_id")]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }

    public string Email { get; set; }

    public IList<Role> Roles { get; set; }
}
