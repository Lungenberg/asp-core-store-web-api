using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ASPCoreWebApplication.Models
{
    [BsonIgnoreExtraElements]
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("albumName")]
        [JsonPropertyName("albumName")]
        [Required, StringLength(64)]
        public string AlbumName { get; set; } = "";

        [BsonElement("price")]
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [BsonElement("genre")]
        [JsonPropertyName("genre")]
        public string Genre { get; set; } = null!;

        [BsonElement("author")]
        [JsonPropertyName("author")]
        public string Author { get; set; } = null!;
    }
}
