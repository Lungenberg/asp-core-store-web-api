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

        [BsonElement("name")]
        [Required, StringLength(64)]
        public string Name { get; set; } = "";

        [BsonElement("displayOrder")]
        [BsonIgnoreIfDefault]
        [Range(0, int.MaxValue)]
        public int DisplayOrder { get; set; } = 0;

    }
}
