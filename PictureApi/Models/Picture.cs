using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PictureApi.Models
{
    public class Picture
    {
        public ObjectId? FileId { get; set; }

        public string? FileName { get; set; }

        public string? CategoryId { get; set; }

        public byte[]? File { get; set; }
    }
}
