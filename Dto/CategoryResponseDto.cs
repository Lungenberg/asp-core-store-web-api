using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ASPCoreWebApplication.Dto
{
    public class CategoryResponseDto
    {
        public string Id { get; set; } = null!;
        public string AlbumName { get; set; } = "";

        public decimal Price { get; set; }

        public string Genre { get; set; } = "";

        public string Author { get; set; } = "";
    }
}
