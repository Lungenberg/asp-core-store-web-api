using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ASPCoreWebApplication.Dto
{
    public class CategoryUpdateDto
    {
        [Required, StringLength(128)]
        public string AlbumName { get; set; } = "";

        [Range(typeof(decimal), "0", "1000000")]
        public decimal Price { get; set; }

        [Required, StringLength(64)]
        public string Genre { get; set; } = "";

        [Required, StringLength(64)]
        public string Author { get; set; } = "";
    }
}

